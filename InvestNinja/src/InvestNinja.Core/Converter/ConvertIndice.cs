using InvestNinja.Core.Domain;
using InvestNinja.Core.Utils;
using System.Linq;

namespace InvestNinja.Core.Converter
{
    public class ConvertIndice : IConvertIndice
    {
        public Indice FromCarteira(Carteira carteira)
        {
            if (carteira != null)
            {
                Indice indice = new Indice(carteira.Codigo, carteira.Descricao, carteira.ValorCotaInicial, carteira.Itens.FirstOrDefault().DataCota);
                carteira.Itens.RemoveAt(0);
                carteira.Itens.ForEach(itemCarteira => indice.AddItemByVariacaoCota(itemCarteira.DataCota, itemCarteira.VariacaoCotaPercentual));
                return indice;
            }
            return null;
        }
    }
}
