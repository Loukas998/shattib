using Azure.AI.Translation.Text;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Text.Json.Nodes;

public class TranslationMiddleware : IMiddleware
{
	private readonly TextTranslationClient _translatorClient;
	private readonly ILogger<TranslationMiddleware> _logger;

	public TranslationMiddleware(TextTranslationClient translatorClient, ILogger<TranslationMiddleware> logger)
	{

		_translatorClient = translatorClient;
		
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		_logger.LogInformation("TranslationMiddleware invoked.");

		// Capture the language from the request header
		var targetLanguage = context.Request.Headers["Accept-Language"].ToString();
		_logger.LogInformation($"Accept-Language: {targetLanguage}");

		if (!string.IsNullOrWhiteSpace(targetLanguage))
		{
			_logger.LogInformation("Proceeding with translation logic.");

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
			_logger.LogInformation("No language header found. Passing request through.");
			await next(context);
		}
	}

	private bool IsJsonResponse(HttpResponse response)
	{
		// Check if the response is likely to be JSON by examining the content type
		return response.ContentType != null && (
		   response.ContentType.Contains("application/json") ||
		   response.ContentType.Contains("text/html") ||
		   response.ContentType.Contains("text/plain") ||
		   response.ContentType.Contains("application/xml")
	   );
	}

	private async Task<string> TranslateTextAsync(string text, string targetLanguage)
	{
		_logger.LogInformation($"Translating text to {targetLanguage}");

		try
		{
			var response = await _translatorClient.TranslateAsync(targetLanguage, new[] { text });
			return response.Value[0].Translations[0].Text;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error during translation.");
			
			return "Translation failed.";
		}
	}

	private async Task<string> TranslateJsonResponse(string jsonResponse, string targetLanguage)
	{
		_logger.LogInformation($"Translating JSON response to {targetLanguage}");

		// Deserialize the JSON response into a dictionary
		var jsonObject = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonResponse);

		if (jsonObject != null)
		{
			// Iterate over each key-value pair in the dictionary
			foreach (var key in jsonObject.Keys.ToList())
			{
				var value = jsonObject[key];

				// Log the value type to understand what is being processed
				_logger.LogInformation($"Processing key '{key}' with value '{value}' of type");

				// Handle strings: Translate if it's a non-null, non-empty string
				if (value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.String)
				{
					var textValue = jsonElement.GetString();
					if (!string.IsNullOrEmpty(textValue))
					{
						// Log before translation
						_logger.LogInformation($"Translating text for key '{key}' with value: {textValue}");

						// Call the translation function
						var translatedText = await TranslateTextAsync(textValue, targetLanguage);

						// Log the translated text
						_logger.LogInformation($"Translated text for key '{key}': {translatedText}");

						// Replace the original text with the translated text
						jsonObject[key] = translatedText;
					}
				}
				// Handle nested objects (JsonElement): Recursively translate nested objects
				else if (value is JsonElement nestedObject && nestedObject.ValueKind == JsonValueKind.Object)
				{
					var nestedJson = nestedObject.GetRawText();
					var translatedNestedJson = await TranslateJsonResponse(nestedJson, targetLanguage);
					jsonObject[key] = JsonSerializer.Deserialize<object>(translatedNestedJson);
				}
				// Handle arrays (JsonArray): Translate string elements in arrays
				else if (value is JsonArray jsonArray)
				{
					var translatedArray = new List<object>();
					foreach (var item in jsonArray)
					{
						if (item.GetValueKind() == JsonValueKind.String)
						{
							// Log translation for array elements
							var translatedItem = await TranslateTextAsync(item.ToString(), targetLanguage);
							_logger.LogInformation($"Translating array item: {item} to {translatedItem}");
							translatedArray.Add(translatedItem);
						}
						else
						{
							translatedArray.Add(item);  // Non-string items are left as they are
						}
					}
					jsonObject[key] = translatedArray;
				}
				// Handle null or non-translatable types: Leave them unchanged
				else
				{
					_logger.LogInformation($"Skipping translation for key '{key}'");
				}
			}
		}

		// Serialize the updated JSON object back into a string
		return JsonSerializer.Serialize(jsonObject);
	}




}




















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
//		private readonly ILogger<TranslationMiddleware> _logger;

//		public TranslationMiddleware(TextTranslationClient translatorClient, ILogger<TranslationMiddleware> logger)
//		{
//			_translatorClient = translatorClient;
//			_logger = logger;
//		}

//		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//		{
//			_logger.LogInformation("TranslationMiddleware invoked.");

//			// Capture the language from the request header
//			var targetLanguage = context.Request.Headers["Accept-Language"].ToString();
//			_logger.LogInformation($"Accept-Language: {targetLanguage}");

//			if (!string.IsNullOrWhiteSpace(targetLanguage))
//			{
//				_logger.LogInformation("Proceeding with translation logic.");

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
//				_logger.LogInformation("No language header found. Passing request through.");
//				await next(context);
//			}
//		}

//		private async Task<string> TranslateTextAsync(string text, string targetLanguage)
//		{
//			_logger.LogInformation($"Translating text to {targetLanguage}");

//			try
//			{
//				var response = await _translatorClient.TranslateAsync(targetLanguage, new[] { text });
//				var translatedText = response.Value[0].Translations[0].Text;
//				return translatedText;
//			}
//			catch (Exception ex)
//			{
//				_logger.LogError(ex, "Error during translation.");
//				return "Translation failed.";
//			}
//		}

//	}
//}
