using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InvestNinja.Core.Domain;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using InvestNinja.Web.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvestNinja.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public string Post([FromBody]Usuario usuario)
        {
            //criar usuário no banco de dados
            //return Jose.JWT.Encode(new AuthorizationToken("ninja"), Encoding.UTF8.GetBytes("abcdefghijklmnopqrs"), Jose.JwsAlgorithm.HS256);
            return "";
        }
        
        [HttpPost("login")]
        public void Login([FromBody]Usuario usuario)
        {
            var token = HttpContext.Request.DecodeUserNameJWT("abcdefghijklmnopqrs");
        }
    }
}
