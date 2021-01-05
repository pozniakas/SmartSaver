using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private List<Token> _listTokens;

        public TokenManager()
        {
            _listTokens = new List<Token>();
        }

        public bool Authenticate(string username, string password)
        {
            if (!string.IsNullOrWhiteSpace(username) &&
                !string.IsNullOrWhiteSpace(password) &&
                username == "admin" &&
                password == "password")
                return true;
            return false;
        }

        public Token NewToken()
        {
            var token = new Token
            {
                Value = Guid.NewGuid().ToString(),
                ExipirationTime = DateTime.Now.AddMinutes(1)
            };

            _listTokens.Add(token);
            return token;
        }

        public bool VerifyToken(string token)
        {
            if (_listTokens.Any(x => x.Value == token && x.ExipirationTime > DateTime.Now))
            {
                return true;
            }

            return false;
        }
    }
}
