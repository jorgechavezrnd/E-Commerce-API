using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace ECommerceAPI.Filters
{
    public class MitoCodeFilterException : IExceptionFilter
    {
        private readonly ILogger<MitoCodeFilterException> _logger;

        public MitoCodeFilterException(ILogger<MitoCodeFilterException> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is InvalidOperationException)
            {
                _logger.LogCritical($"{context.Exception.Message} {context.Exception.StackTrace}");
            }
        }
    }
}
