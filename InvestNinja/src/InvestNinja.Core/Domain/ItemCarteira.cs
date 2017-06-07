using System;

namespace InvestNinja.Core.Domain
{
    public class ItemCarteira : IItemCotizacao
    {
        public DateTime DataCota { get; set; }

        public double ValorMovimentacao { get; set; }

        public double Saldo { get; set; }

        public double QtdCotasAnterior { get; set; }

        public double QtdCotasMovimentacao { get; set; }

        public double QtdCotasAtual { get; set; }

        public double VariacaoCotaPercentual { get; set; }

        public double VariacaoFinanceira { get; set; }

        public double ValorCota { get; set; }
    }
}
