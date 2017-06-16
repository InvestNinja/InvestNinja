using InvestNinja.Core.Domain;
using InvestNinja.Core.Domain.Carteira;

namespace InvestNinja.Core.Converter
{
    public interface IConvertIndice
    {
        Indice FromCarteira(Carteira carteira);
    }
}
