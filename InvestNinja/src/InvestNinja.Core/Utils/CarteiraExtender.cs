using InvestNinja.Core.Converter;
using InvestNinja.Core.Domain;
using InvestNinja.Core.Domain.Carteira;
using InvestNinja.Core.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace InvestNinja.Core.Utils
{
    public static class CarteiraExtender
    {
        public static Indice ToIndice(this Carteira carteira) => Container.ServiceProvider.GetService<IConvertIndice>().FromCarteira(carteira);
    }
}
