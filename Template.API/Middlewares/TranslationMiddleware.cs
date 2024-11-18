using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Template.API.Middlewares;

public class TranslationMiddleware(ILogger<TranslationMiddleware> logger) : IMiddleware
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

    private async Task<string> TranslateTextAsync(string text, string targetLanguage)
    {
        var endpoint = "https://api.cognitive.microsofttranslator.com";
        var route = $"/translate?api-version=3.0&to={targetLanguage}";
        using (var client = new HttpClient())
        using (var request = new HttpRequestMessage())
        {
            // Build the request.
            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri(endpoint + route);
            request.Content = new StringContent(text, Encoding.UTF8, "application/json");
            request.Headers.Add("Ocp-Apim-Subscription-Key",
                "FqyfAN1ywtRSCX0QB42HgCDMLzUuIUpq0EH9WiAf1wxqpUBuoFp0JQQJ99AKACF24PCXJ3w3AAAbACOGGFpp");
            // location required if you're using a multi-service or regional (not global) resource.
            request.Headers.Add("Ocp-Apim-Subscription-Region", "uaenorth");

            // Send the request and get response.
            var response = await client.SendAsync(request).ConfigureAwait(false);
            // Read response as a string.
            var result = await response.Content.ReadAsStringAsync();
            return result;
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
    }

    private async Task<string> TranslateJsonResponse(string jsonResponse, string targetLanguage)
    {
        logger.LogInformation($"Translating JSON response to {targetLanguage}");

        // Deserialize the JSON response into a dictionary
        var jsonObject = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonResponse);

        if (jsonObject != null)
            // Iterate over each key-value pair in the dictionary
            foreach (var key in jsonObject.Keys.ToList())
            {
                var value = jsonObject[key];

                // Log the value type to understand what is being processed
                logger.LogInformation($"Processing key '{key}' with value '{value}' of type");

                // Handle strings: Translate if it's a non-null, non-empty string
                if (value is JsonElement jsonElement && jsonElement.ValueKind == JsonValueKind.String)
                {
                    var textValue = jsonElement.GetString();
                    if (!string.IsNullOrEmpty(textValue))
                    {
                        // Log before translation
                        logger.LogInformation($"Translating text for key '{key}' with value: {textValue}");

                        // Call the translation function
                        var translatedText = await TranslateTextAsync(textValue, targetLanguage);

                        // Log the translated text
                        logger.LogInformation($"Translated text for key '{key}': {translatedText}");

                        // Replace the original text with the translated text
                        jsonObject[key] = translatedText;
                    }
                }
                // Handle nested objects (JsonElement): Recursively translate nested objects
                else if (value is JsonElement nestedObject && nestedObject.ValueKind == JsonValueKind.Object)
                {
                    var nestedJson = nestedObject.GetRawText();
                    var translatedNestedJson = await TranslateJsonResponse(nestedJson, targetLanguage);
                    jsonObject[key] = JsonSerializer.Deserialize<object>(translatedNestedJson!);
                }
                // Handle arrays (JsonArray): Translate string elements in arrays
                else if (value is JsonArray jsonArray)
                {
                    var translatedArray = new List<object>();
                    foreach (var item in jsonArray)
                        if (item!.GetValueKind() == JsonValueKind.String)
                        {
                            // Log translation for array elements
                            var translatedItem = await TranslateTextAsync(item.ToString(), targetLanguage);
                            logger.LogInformation($"Translating array item: {item} to {translatedItem}");
                            translatedArray.Add(translatedItem);
                        }
                        else
                        {
                            translatedArray.Add(item); // Non-string items are left as they are
                        }

                    jsonObject[key] = translatedArray;
                }
                // Handle null or non-translatable types: Leave them unchanged
                else
                {
                    logger.LogInformation($"Skipping translation for key '{key}'");
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