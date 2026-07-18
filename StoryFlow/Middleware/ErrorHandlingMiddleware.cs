using StoryFlow.Exceptions;

namespace StoryFlow.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(BadRequestException ex)
            {
                _logger.LogError(ex.ToString());
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                _logger.LogError(ex.ToString());
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Please log in to access this resource.");
            }

            catch (ForbiddenException ex)
            {
                _logger.LogError(ex.ToString());
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("You don't have permission to perform this action.");
            }

            catch (NotFoundException ex)
            {
                _logger.LogError(ex.ToString());
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (ConflictException ex)
            {
                _logger.LogError(ex.ToString());
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Unexpected server error. Try again later.");
            }
        }


    }
}
