using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using JsonException = Newtonsoft.Json.JsonException;

public class TranslationMiddleware(ILogger<TranslationMiddleware> logger, IConfiguration configuration) : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		logger.LogInformation("TranslationMiddleware invoked.");

		// Capture the language from the request header
		var targetLanguage = context.Request.Headers["Accept-Language"].ToString();
		logger.LogInformation($"Accept-Language: {targetLanguage}");

		if (!string.IsNullOrEmpty(targetLanguage))
		{
			logger.LogInformation("Proceeding with translation logic.");

			// Capture the response body
			var originalBody = context.Response.Body;
			using var newBody = new MemoryStream();
			context.Response.Body = newBody;

			// Process the request
			await next(context);

			// After the request is processed, send response to Azure for translation
			if (context.Response.StatusCode == StatusCodes.Status200OK)
			{
				// Read the response body from the memory stream
				newBody.Seek(0, SeekOrigin.Begin);
				var responseBody = await new StreamReader(newBody).ReadToEndAsync();

				// Reset the original response body and write the translated text
				context.Response.Body = originalBody;

				var translatedJson = await TranslateJsonResponse(responseBody, targetLanguage);
				await context.Response.WriteAsync(translatedJson);
			}
			else
			{
				// If not a 200 status, copy the original content
				context.Response.Body = originalBody;
				newBody.Seek(0, SeekOrigin.Begin);
				await newBody.CopyToAsync(originalBody);
			}
		}
		else
		{
			logger.LogInformation("No language header found. Passing request through.");
			await next(context);
		}
	}

	private async Task<string> TranslateTextAsync(string text, string targetLanguage)
	{
		string endpoint = configuration["AzureAiTranslator:AzureTranslatorEndpoint"]!;
		string route = $"/translate?api-version=3.0&to={targetLanguage}";
		using (var client = new HttpClient())
		using (var request = new HttpRequestMessage())
		{
			// Build the request.
			request.Method = HttpMethod.Post;
			request.RequestUri = new Uri(endpoint + route);

			object[] body = new object[] { new { Text = text } };
			var requestBody = JsonConvert.SerializeObject(body);

			request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
			request.Headers.Add("Ocp-Apim-Subscription-Key", configuration["AzureAiTranslator:AzureTranslatorApiKey"]);
			// location required if you're using a multi-service or regional (not global) resource.
			request.Headers.Add("Ocp-Apim-Subscription-Region", configuration["AzureAiTranslator:Location"]);

			// Send the request and get response.
			HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
			// Read response as a string.
			string result = await response.Content.ReadAsStringAsync();
			return result;
		}
	}

	private async Task<string> TranslateJsonResponse(string jsonResponse, string targetLanguage)
	{
		logger.LogInformation($"Translating JSON response to {targetLanguage}");

		// Deserialize the JSON response
		JToken jsonObject;
		try
		{
			jsonObject = JToken.Parse(jsonResponse);
		}
		catch (JsonException ex)
		{
			logger.LogError($"Failed to parse JSON: {ex.Message}");
			return jsonResponse;  // Return original response in case of error
		}

		if (jsonObject != null)
		{
			// If the root element is an array, process each item
			if (jsonObject.Type == JTokenType.Array)
			{
				var translatedArray = new JArray();
				foreach (var item in jsonObject)
				{
					var translatedItem = await TranslateJsonObject(item, targetLanguage);
					translatedArray.Add(translatedItem);  // Add the translated item to the array
				}
				return JsonConvert.SerializeObject(translatedArray);  // Serialize and return the translated array
			}
			else
			{
				// If it's a single object, process it directly
				var translatedObject = await TranslateJsonObject(jsonObject, targetLanguage);
				return JsonConvert.SerializeObject(translatedObject);  // Serialize and return the translated object
			}
		}

		return jsonResponse;  // Return original response if JSON parsing fails
	}

	private async Task<JToken> TranslateJsonObject(JToken jsonObject, string targetLanguage)
	{
		// If it's an object, process each property
		if (jsonObject.Type == JTokenType.Object)
		{
			var translatedObject = new JObject();
			foreach (var property in jsonObject.Children<JProperty>())
			{
				var translatedValue = await TranslateJsonValue(property.Value, targetLanguage);
				translatedObject.Add(property.Name, translatedValue);  // Add translated value to new JObject
			}
			return translatedObject;  // Return translated object
		}

		// If it's not an object, return it as-is
		return jsonObject;
	}

	private async Task<JToken> TranslateJsonValue(JToken value, string targetLanguage)
	{
		if (value.Type == JTokenType.String)
		{
			// Translate string values
			var textValue = value.ToString();
			if (!string.IsNullOrEmpty(textValue))
			{
				// Call your TranslateTextAsync method to get the translated value
				var translatedText = await TranslateTextAsync(textValue, targetLanguage);

				// Extract the "text" value from the translation response
				var translationResponse = JArray.Parse(translatedText); // Parse the translation response
				var translatedTextValue = translationResponse[0]["translations"]?[0]["text"]?.ToString(); // Extract the translated "text"

				// Log the translation process
				logger.LogInformation($"Translated text for value '{textValue}' to: {translatedTextValue}");

				// Return the translated text if found
				return translatedTextValue != null ? JToken.FromObject(translatedTextValue) : value;
			}
		}
		else if (value.Type == JTokenType.Array)
		{
			// If it's an array, recursively process each item
			var translatedArray = new JArray();
			foreach (var item in value)
			{
				var translatedItem = await TranslateJsonValue(item, targetLanguage);
				translatedArray.Add(translatedItem);  // Add translated item to the array
			}
			return translatedArray;  // Return translated array
		}
		else if (value.Type == JTokenType.Object)
		{
			// If it's an object, recursively process each property
			return await TranslateJsonObject(value, targetLanguage);
		}

		// If it's not a string, array, or object (e.g., numbers, booleans), return the value as-is
		return value;
	}
}











//logger.LogInformation($"Translating text to {targetLanguage}");

//try
//{
//	var response = await _translatorClient.TranslateAsync(targetLanguage, new[] { text });
//	return response.Value[0].Translations[0].Text;
//}
//catch (Exception ex)
//{
//	logger.LogError(ex, "Error during translation.");

//	return "Translation failed.";
//}








//using Microsoft.Extensions.Logging;
//using Azure.AI.Translation.Text;
//using Microsoft.AspNetCore.Http;
//using System.IO;
//using System.Threading.Tasks;

//namespace Template.API.Middlewares
//{
//	public class TranslationMiddleware : IMiddleware
//	{
//		private readonly TextTranslationClient _translatorClient;
//		private readonly ILogger<TranslationMiddleware> logger;

//		public TranslationMiddleware(TextTranslationClient translatorClient, ILogger<TranslationMiddleware> logger)
//		{
//			_translatorClient = translatorClient;
//			logger = logger;
//		}

//		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//		{
//			logger.LogInformation("TranslationMiddleware invoked.");

//			// Capture the language from the request header
//			var targetLanguage = context.Request.Headers["Accept-Language"].ToString();
//			logger.LogInformation($"Accept-Language: {targetLanguage}");

//			if (!string.IsNullOrWhiteSpace(targetLanguage))
//			{
//				logger.LogInformation("Proceeding with translation logic.");

//				// Capture the response body
//				var originalBody = context.Response.Body;
//				using var newBody = new MemoryStream();
//				context.Response.Body = newBody;

//				// Process the request
//				await next(context);

//				// After request is processed, send response to Azure for translation
//				if (context.Response.StatusCode == StatusCodes.Status200OK)
//				{
//					// Read the response body from memory stream
//					newBody.Seek(0, SeekOrigin.Begin);
//					var responseBody = await new StreamReader(newBody).ReadToEndAsync();

//					// Translate the response text
//					var translatedText = await TranslateTextAsync(responseBody, targetLanguage);

//					// Reset the original response body and write the translated text
//					context.Response.Body = originalBody;
//					await context.Response.WriteAsync(translatedText);
//				}
//				else
//				{
//					// If not a 200 status, we simply copy the original content
//					context.Response.Body = originalBody;
//					newBody.Seek(0, SeekOrigin.Begin);
//					await newBody.CopyToAsync(originalBody);
//				}
//			}
//			else
//			{
//				logger.LogInformation("No language header found. Passing request through.");
//				await next(context);
//			}
//		}

//		private async Task<string> TranslateTextAsync(string text, string targetLanguage)
//		{
//			logger.LogInformation($"Translating text to {targetLanguage}");

//			try
//			{
//				var response = await _translatorClient.TranslateAsync(targetLanguage, new[] { text });
//				var translatedText = response.Value[0].Translations[0].Text;
//				return translatedText;
//			}
//			catch (Exception ex)
//			{
//				logger.LogError(ex, "Error during translation.");
//				return "Translation failed.";
//			}
//		}

//	}
//}


//private bool IsJsonResponse(HttpResponse response)
//{
//	// Check if the response is likely to be JSON by examining the content type
//	return response.ContentType != null && (
//	   response.ContentType.Contains("application/json") ||
//	   response.ContentType.Contains("text/html") ||
//	   response.ContentType.Contains("text/plain") ||
//	   response.ContentType.Contains("application/xml")
//   );
//}