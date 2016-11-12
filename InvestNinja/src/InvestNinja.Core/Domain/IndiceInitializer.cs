using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Core.Domain
{
    public class IndiceInitializer
    {
        public IndiceInitializer(string codigo, string Descricao, double valorCotaInicial, DateTime dataCota, double valorMovimentacao, double saldo)
        {
            this.Codigo = codigo;
            this.Descricao = Descricao;
            this.ValorCotaInicial = valorCotaInicial;
            this.DataCota = dataCota;
            this.ValorMovimentacao = valorMovimentacao;
            this.Saldo = saldo;
        }

        public string Codigo { get; private set; }
        public DateTime DataCota { get; private set; }
        public string Descricao { get; private set; }
        public double Saldo { get; private set; }
        public double ValorCotaInicial { get; private set; }
        public double ValorMovimentacao { get; private set; }
    }
}
