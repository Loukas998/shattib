using Template.Domain.Exceptions;
namespace Template.API.Middlewares
{
	public class ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger) : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next.Invoke(context);
			}
			catch (NotFoundException notFound)
			{
				logger.LogWarning(notFound.Message);

				context.Response.StatusCode = 404;
				await context.Response.WriteAsync(notFound.Message);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);

				context.Response.StatusCode = 500;
				await context.Response.WriteAsync("Something went wrong");
			}
		}
	}
}
