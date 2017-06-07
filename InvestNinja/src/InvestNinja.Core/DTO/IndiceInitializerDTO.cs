using System;

namespace InvestNinja.Core.DTO
{
    public class IndiceInitializerDTO
    {
        public IndiceInitializerDTO(string codigo, string descricao, double valorCotaInicial, DateTime dataCota)
        {
            this.Codigo = codigo;
            this.Descricao = descricao;
            this.ValorCotaInicial = valorCotaInicial;
            this.DataCota = dataCota;
        }

        public string Codigo { get; private set; }
        public DateTime DataCota { get; private set; }
        public string Descricao { get; private set; }
        public double ValorCotaInicial { get; private set; }
    }
}
