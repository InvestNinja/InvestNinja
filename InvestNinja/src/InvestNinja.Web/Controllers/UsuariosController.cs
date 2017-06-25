﻿using Microsoft.AspNetCore.Mvc;
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
            return Jose.JWT.Encode(CreateToken(usuario), Encoding.UTF8.GetBytes("abcdefghijklmnopqrs"), Jose.JwsAlgorithm.HS256);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public void Login([FromBody]Usuario usuario)
        {
            var token = HttpContext.Request.DecodeUserNameJWT("abcdefghijklmnopqrs");
        }

        private AuthorizationToken CreateToken(Usuario usuario) => new AuthorizationToken("http://www.investninja.com", "investninja.com", "InvestNinja", usuario.UserName, null);
    }
}
