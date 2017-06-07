using System;

namespace InvestNinja.Core.Domain
{
    public interface IItemCotizacao
    {
        DateTime DataCota { get; set; }

        double VariacaoCotaPercentual { get; set; }

        double VariacaoFinanceira { get; set; }

        double ValorCota { get; set; }
    }
}
