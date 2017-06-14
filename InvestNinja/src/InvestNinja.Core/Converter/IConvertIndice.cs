using InvestNinja.Core.Domain;

namespace InvestNinja.Core.Converter
{
    public interface IConvertIndice
    {
        Indice FromCarteira(Carteira carteira);
    }
}
