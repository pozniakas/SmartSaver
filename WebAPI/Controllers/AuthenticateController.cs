using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.TokenAuthentication;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly ITokenManager _tokenManager;

        public AuthenticateController(ITokenManager tokenManager)
        {
            _tokenManager = tokenManager;
        }

        public IActionResult Authenticate(string username, string password)
        {
            if (_tokenManager.Authenticate(username, password))
            {
                return Ok(new { Token = _tokenManager.NewToken() });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not authorized.");
                return Unauthorized(ModelState);
            }
        }
    }
}
