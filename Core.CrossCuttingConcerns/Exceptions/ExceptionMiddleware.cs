using Core.CrossCuttingConcerns.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpExceptionHandler _httpExceptionHandler;
        //private readonly LoggerServiceBase _loggerServiceBase;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        public ExceptionMiddleware(RequestDelegate next /*,LoggerServiceBase loggerServiceBase, IHttpContextAccessor httpContextAccessor*/)
        {
            _next = next;
            _httpExceptionHandler = new HttpExceptionHandler();
            //_loggerServiceBase = loggerServiceBase;
            //_httpContextAccessor = httpContextAccessor;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                //await LogException(context, exception);
                await HandleExceptionAsync(context.Response, exception);
            }
        }

        //private Task LogException(HttpContext context, Exception exception)
        //{
        //    List<LogParameter> parameters = new List<LogParameter>()
        //    {
        //        new LogParameter(){Type=context.GetType().Name,Value=exception.ToString()}
        //    };

        //    LogDetailWithException logDetailWithMessage = new LogDetailWithException()
        //    {
        //        ExceptionMessage = exception.Message,
        //        MethodName = _next.Method.Name,
        //        LogParameters = parameters,
        //        User = _httpContextAccessor.HttpContext?.User.Identity.Name ?? "?"
        //    };

        //    _loggerServiceBase.Error(JsonSerializer.Serialize(logDetailWithMessage));

        //    return Task.CompletedTask;
        //}

        private Task HandleExceptionAsync(HttpResponse response, Exception exception)
        {
            response.ContentType = "application/json";
            _httpExceptionHandler.Response = response;
            return _httpExceptionHandler.HandleExceptionAsync(exception);
        }
    }
}