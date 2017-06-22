using InvestNinja.Core.Tipos;
using InvestNinja.Core.Utils;
using Xunit;

namespace InvestNinja.Core.Test
{
    public class TestsUtils
    {
        [Fact]
        public void TesteEnumExtender()
        {
            Assert.Equal("Aplicação", TipoMovimentacao.Aplicacao.GetDescription());
            Assert.Equal("Resgate", TipoMovimentacao.Resgate.GetDescription());
            Assert.Equal("Rendimento", TipoMovimentacao.Rendimento.GetDescription());
            Assert.Equal("Amortização", TipoMovimentacao.Amortizacao.GetDescription());
        }
    }
}
