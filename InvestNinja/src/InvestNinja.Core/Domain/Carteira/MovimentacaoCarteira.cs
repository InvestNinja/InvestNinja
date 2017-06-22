using InvestNinja.Core.Tipos;
using InvestNinja.Core.Utils;

namespace InvestNinja.Core.Domain.Carteira
{
    public class MovimentacaoCarteira
    {
        public double Valor { get; set; }

        public TipoMovimentacao Tipo { get; set; }

        public string Descricao { get; set; }

        public string TipoDescricao => Tipo.GetDescription();

        public int Ordem { get; set; }
    }
}
