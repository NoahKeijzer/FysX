using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fysio.Areas.Treator.Models
{
    public class TokenModel
    {
        public string token { get; set; }
        public string expiration { get; set; }
        public TokenModel()
        {

        }

        public TokenModel(string token, string expiration)
        {
            this.token = token;
            this.expiration = expiration;
        }
    }
}
