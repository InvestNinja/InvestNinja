using InvestNinja.Core.Tipos;

namespace InvestNinja.Core.Domain.Carteira
{
    public class MovimentacaoCarteira
    {
        public double Valor { get; set; }

        public TipoMovimentacao Tipo { get; set; }

        public string Descricao { get; set; }
    }
}
