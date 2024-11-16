using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

public class TranslationMiddleware(ILogger<TranslationMiddleware> logger, IConfiguration configuration) : IMiddleware
{
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		logger.LogInformation("TranslationMiddleware invoked.");

		// Capture the language from the request header
		var targetLanguage = context.Request.Headers["Accept-Language"].ToString();
		logger.LogInformation($"Accept-Language: {targetLanguage}");

		if (!string.IsNullOrWhiteSpace(targetLanguage))
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

		// Deserialize the JSON response using Newtonsoft.Json
		var jsonObject = JsonConvert.DeserializeObject<JObject>(jsonResponse);

		if (jsonObject != null)
		{
			// Iterate over each key-value pair in the JObject
			foreach (var property in jsonObject.Properties().ToList())
			{
				var value = property.Value;

				// Log the value type to understand what is being processed
				logger.LogInformation($"Processing key '{property.Name}' with value '{value}' of type {value.Type}");

				// Handle strings: Translate if it's a non-null, non-empty string
				if (value.Type == JTokenType.String)
				{
					var textValue = value.ToString();
					if (!string.IsNullOrEmpty(textValue))
					{
						// Log before translation
						logger.LogInformation($"Translating text for key '{property.Name}' with value: {textValue}");

						// Call the translation function
						var translatedText = await TranslateTextAsync(textValue, targetLanguage);


						//"[{\"detectedLanguage\":{\"language\":\"en\",\"score\":0.91},\"translations\":[{\"text\":\"اختبر\",\"to\":\"ar\"}]}]"

						// Log the translated text
						logger.LogInformation($"Translated text for key '{property.Name}': {translatedText}");

						// Replace the original text with the translated text (ensure it's parsed into the correct format)
						var translatedTextArray = JsonConvert.DeserializeObject<JArray>(translatedText);
						if (translatedTextArray != null && translatedTextArray.Count > 0)
						{
							var translations = translatedTextArray[0]["translations"];
							if (translations != null)
							{
								var translatedTextValue = translations?.FirstOrDefault()?["text"]?.ToString();
								if (translatedTextValue != null)
								{
									property.Value = translatedTextValue;
								}
							}
						}
					}
				}
				// Handle nested objects (JObject): Recursively translate nested objects
				else if (value.Type == JTokenType.Object)
				{
					var nestedJson = value.ToString();
					var translatedNestedJson = await TranslateJsonResponse(nestedJson, targetLanguage);
					property.Value = JsonConvert.DeserializeObject<JObject>(translatedNestedJson);
				}
				// Handle arrays (JArray): Translate string elements in arrays
				else if (value.Type == JTokenType.Array)
				{
					var translatedArray = new JArray();
					foreach (var item in value)
					{
						if (item.Type == JTokenType.String)
						{
							// Log translation for array elements
							var translatedItem = await TranslateTextAsync(item.ToString(), targetLanguage);
							logger.LogInformation($"Translating array item: {item} to {translatedItem}");
							translatedArray.Add(translatedItem);
						}
						else
						{
							translatedArray.Add(item);  // Non-string items are left as they are
						}
					}
					property.Value = translatedArray;
				}
				// Handle null or non-translatable types: Leave them unchanged
				else
				{
					logger.LogInformation($"Skipping translation for key '{property.Name}'");
				}
			}
		}

		// Serialize the updated JSON object back into a string using Newtonsoft.Json
		return JsonConvert.SerializeObject(jsonObject);
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