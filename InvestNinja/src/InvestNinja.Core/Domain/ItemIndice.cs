using System;

namespace InvestNinja.Core.Domain
{
    public class ItemIndice : IItemCotizacao
    {
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

        public double VariacaoCotaPercentual { get; set; }

        public double VariacaoFinanceira { get; set; }

        public double ValorCota { get; set; }
    }
}
