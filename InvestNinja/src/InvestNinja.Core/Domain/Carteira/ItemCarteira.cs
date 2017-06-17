using System;
using System.Collections.Generic;

namespace InvestNinja.Core.Domain.Carteira
{
    public class ItemCarteira : IItemCotizacao
    {
        public ItemCarteira()
        {
            Movimentacoes = new List<MovimentacaoCarteira>();
        }

        private DateTime dataCota;
        public DateTime DataCota
        {
            get
            {
                return dataCota;
            }
            set
            {
                dataCota = value.Date;
            }
        }

        public double ValorMovimentacoes { get; set; }

        public double Saldo { get; set; }

        public double QtdCotasAnterior { get; set; }

        public double QtdCotasMovimentacao { get; set; }

        public double QtdCotasAtual { get; set; }

        public double VariacaoCotaPercentual { get; set; }

        public double VariacaoFinanceira { get; set; }

        public double ValorCota { get; set; }

        public IList<MovimentacaoCarteira> Movimentacoes { get; set; }
    }
}
