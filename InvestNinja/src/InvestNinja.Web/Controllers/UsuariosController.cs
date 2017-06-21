using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InvestNinja.Core.Domain;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvestNinja.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        [HttpPost]
        public string Post([FromBody]Usuario usuario)
        {
            var s = new {
                iss = "http://www.investninja.com",
                aud = "investninja.com",
                sub = "123456",
                exp = 1499863217,
                roles = new List<string>() { "User" },
                username = "ninja"
            };

            var payload = new Dictionary<string, object>() { { "username", usuario.UserName }, { "roles", "[User]" } };
            var secretKey = Encoding.UTF8.GetBytes("abcdefghijklmnopqrs");
            return Jose.JWT.Encode(s, secretKey, Jose.JwsAlgorithm.HS256);
        }
        
        [HttpPost("login")]
        public void Login([FromBody]Usuario usuario)
        {
            var at = HttpContext.Request.Headers["AuthToken"];
            //MyDomainObject obj = Jose.JWT.Decode<MyDomainObject>(token, secretKey);
            var x = Jose.JWT.Decode(at, Encoding.UTF8.GetBytes("abcdefghijklmnopqrs"));
            var i = at.Count;
        }
    }
}
