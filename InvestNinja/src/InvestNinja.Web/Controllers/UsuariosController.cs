using Microsoft.AspNetCore.Mvc;
using InvestNinja.Core.Domain;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using InvestNinja.Web.Utils;
using InvestNinja.Core.Service;
using InvestNinja.Core.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InvestNinja.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly ILoginService loginService;

        public UsuariosController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public string Post([FromBody]Usuario usuario) => EncodeJWT(loginService.Create(usuario));

        [AllowAnonymous]
        [HttpPost("login")]
        public string Login([FromBody]Usuario usuario) => EncodeJWT(loginService.Autenticar(usuario.UserName, usuario.Senha));

        private string EncodeJWT(AuthorizationToken token) => Jose.JWT.Encode(token, Encoding.UTF8.GetBytes("abcdefghijklmnopqrs"), Jose.JwsAlgorithm.HS256);

        [HttpPut("{userName}")]
        public void Put([FromBody]Usuario usuario, string userName) => loginService.AlterarUsuario(userName, usuario.Email, usuario.Nome);

        [HttpPut("{userName}")]
        public void ChangePassword([FromBody]UsuarioDTO usuarioDTO, string userName) => loginService.AlterarSenha(userName, usuarioDTO.SenhaAtual, usuarioDTO.SenhaNova);

        //var tokenUserName = HttpContext.Request.DecodeUserNameJWT("abcdefghijklmnopqrs"); pegar o usuário do JWT
    }
}
