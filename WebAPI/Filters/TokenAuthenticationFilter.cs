using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Security.Principal;
using WebAPI.TokenAuthentication;

namespace WebAPI.Filters
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenManager = (ITokenManager)context.HttpContext.RequestServices.GetService(typeof(ITokenManager));

            if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token) || !tokenManager.VerifyToken(token)) {
                context.ModelState.AddModelError("Unauthorized", "You are not authorized");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }

            // Add userId to HttpContext
        }
    }
}
