using InvestNinja.Core.Domain;

namespace InvestNinja.Core.Service
{
    public interface ILoginService
    {
        AuthorizationToken Autenticar(string userName, string senha);

        AuthorizationToken Create(Usuario usuario);

        void AlterarUsuario(string userName, string email, string nome);

        void AlterarSenha(string userName, string senhaAtual, string senhaNova);
    }
}
