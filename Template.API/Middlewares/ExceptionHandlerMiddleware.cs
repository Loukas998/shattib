using Template.Domain.Exceptions;
namespace Template.API.Middlewares
{
	public class ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger) : IMiddleware
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
	
		}
	}
}
