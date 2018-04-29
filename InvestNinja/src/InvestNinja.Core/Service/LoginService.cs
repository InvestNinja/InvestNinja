using InvestNinja.Core.Data;
using InvestNinja.Core.Domain;
using System;

namespace InvestNinja.Core.Service
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<Usuario> repositoryUsuario;

        public LoginService(IRepository<Usuario> repositoryUsuario)
        {
            this.repositoryUsuario = repositoryUsuario;
        }

        public AuthorizationToken Create(Usuario usuario)
        {
            if (Exists(usuario.UserName))
                throw new Exception("Usuário já cadastrado no sistema. Por favor utilizar outro username");
            repositoryUsuario.Insert(usuario);
            return CreateToken(usuario.UserName);
        }

        public bool Exists(string userName) => !string.IsNullOrEmpty(repositoryUsuario.GetById(userName).UserName);

        public bool Login(string userName, string senha) => repositoryUsuario.GetById(userName).Senha == senha;

        private AuthorizationToken CreateToken(string userName) => new AuthorizationToken("http://www.investninja.com", "investninja.com", "InvestNinja", userName, null);

        public AuthorizationToken Autenticar(string userName, string senha)
        {
            if (!Login(userName, senha))
                throw new Exception("Usuário e/ou senha inválido");
            return CreateToken(userName);
        }

        public void AlterarSenha(string userName, string senhaAtual, string senhaNova)
        {
            if (!Login(userName, senhaAtual))
                throw new Exception("Usuário e/ou senha inválido");

            Usuario usuario = repositoryUsuario.GetById(userName);
            usuario.Senha = senhaNova;
            repositoryUsuario.Update(usuario);
        }

        public void AlterarUsuario(string userName, string email, string nome)
        {
            Usuario usuario = repositoryUsuario.GetById(userName);
            usuario.Email = email;
            usuario.Nome = nome;
            repositoryUsuario.Update(usuario);
        }
    }
}
