using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Core.DTO
{
    public class FinanciamentoParcelaDTO
    {
        public int NumeroParcela { get; set; }

        public double ValorParcela { get; set; }

        public double ValorJuros { get; set; }

        public double ValorAmortizacao { get; set; }

        public double ValorSaldoDevedor { get; set; }
    }
}
