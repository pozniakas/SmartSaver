using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.TokenAuthentication
{
    public class Token
    {
        public string Value { get; set; }
        public DateTime ExipirationTime { get; set; }
    }
}
