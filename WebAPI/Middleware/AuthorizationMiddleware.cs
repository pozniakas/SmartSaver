using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public AuthorizationMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var controllerActionDescriptor =
                context
                .GetEndpoint();
            var meta = controllerActionDescriptor.Metadata
                .GetMetadata<ControllerActionDescriptor>();

            var controllerName = meta.ControllerName;
            var actionName = meta.ActionName;

            await _next(context);

            sw.Stop();

            _logger.Information($"It took {sw.ElapsedMilliseconds} ms to perform " +
                $"this action {actionName} in this controller {controllerName}");
        }

        private void SetPrincipal(IPrincipal principal)
        {
            Thread.CurrentPrincipal = principal;
            //if (HttpContext.Current != null)
            //{
            //    HttpContext.Current.User = principal;
            //}
        }
    }
}
