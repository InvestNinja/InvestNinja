using System.Collections.Generic;

namespace InvestNinja.Core.Domain
{
    public interface ICotizacao<T> where T : IItemCotizacao
    {
        string Codigo { get; set; }

        string Descricao { get; set; }

        double ValorCotaInicial { get; set; }

        IList<T> Itens { get; set; }
        
        double ValorCotaAtual { get; }

        double VariacaoCotaPercentual { get; }

        double VariacaoFinanceira { get; }
    }
}
