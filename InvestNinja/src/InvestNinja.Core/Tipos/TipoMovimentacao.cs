using System.ComponentModel;

namespace InvestNinja.Core.Tipos
{
    public enum TipoMovimentacao
    {
        [Description("Aplicação")]
        Aplicacao,
        [Description("Resgate")]
        Resgate,
        [Description("Rendimento")]
        Rendimento,
        [Description("Amortização")]
        Amortizacao
    };
}
