using Azure;
using Azure.AI.Translation.Text;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Template.API.Middlewares
{
	public class TranslationMiddleware : IMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly TextTranslationClient _translatorClient;

		public TranslationMiddleware(RequestDelegate next, TextTranslationClient translatorClient)
		{
			_next = next;
			_translatorClient = translatorClient;
		}
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			// Capture the specified language from the request header
			var targetLanguage = context.Request.Headers["Accept-Language"].ToString();

			// Proceed if a language is specified
			if (!string.IsNullOrWhiteSpace(targetLanguage))
			{
				// Capture the response body
				var originalBody = context.Response.Body;
				using var newBody = new MemoryStream();
				context.Response.Body = newBody;

				// Process the request
				await _next(context);

				// Translate the response if it’s successful and not empty
				if (context.Response.StatusCode == StatusCodes.Status200OK)
				{
					context.Response.Body.Seek(0, SeekOrigin.Begin);
					var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
					var translatedText = await TranslateTextAsync(responseBody, targetLanguage);

					context.Response.Body = originalBody;
					await context.Response.WriteAsync(translatedText);
				}
				else
				{
					context.Response.Body = originalBody;
					newBody.Seek(0, SeekOrigin.Begin);
					await newBody.CopyToAsync(originalBody);
				}
			}
			else
			{
				await _next(context);
			}
		}

		private async Task<string> TranslateTextAsync(string text, string targetLanguage)
		{
			// Perform the translation
			var response = await _translatorClient.TranslateAsync(targetLanguage, new[] { text });

			// Retrieve the translated text (first translation in the response)
			var translatedText = response.Value[0].Translations[0].Text;

			return translatedText;
		}
	}
}
