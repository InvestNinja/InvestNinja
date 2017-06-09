using InvestNinja.Core.Tipos;

namespace InvestNinja.Core.Domain
{
    public class MovimentacaoCarteira
    {
        public double Valor { get; set; }

        public TipoMovimentacao Tipo { get; set; }

        public string Descricao { get; set; }
    }
}
