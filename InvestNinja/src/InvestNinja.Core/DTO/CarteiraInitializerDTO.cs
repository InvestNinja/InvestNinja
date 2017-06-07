using System;

namespace InvestNinja.Core.DTO
{
    public class CarteiraInitializerDTO
    {
        public CarteiraInitializerDTO(string codigo, string descricao, double valorCotaInicial, DateTime dataCota, double saldo)
        {
            this.Codigo = codigo;
            this.Descricao = descricao;
            this.ValorCotaInicial = valorCotaInicial;
            this.DataCota = dataCota;
            this.Saldo = saldo;
        }

        public string Codigo { get; private set; }
        public DateTime DataCota { get; private set; }
        public string Descricao { get; private set; }
        public double Saldo { get; private set; }
        public double ValorCotaInicial { get; private set; }
    }
}
