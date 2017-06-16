using InvestNinja.Core.Service;
using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using InvestNinja.Core.Infrastructure;

namespace InvestNinja.Core.Test
{
    public class TestsFinancialService
    {
        private readonly IFinancialService financialService;

        public TestsFinancialService()
        {
            Container.Initialize(ContainerRegisterAll.RegisterDependenciesReferenced());
            financialService = Container.ServiceProvider.GetService<IFinancialService>();
        }

        [Fact]
        public void TestCalcularMontante() => Assert.Equal(1061.52, financialService.CalcularMontante(1000.00, 1.0, 6), 2);

        [Fact]
        public void TestCalcularMontanteMensalComAplicacao() => Assert.Equal(1682.87, financialService.CalcularMontanteMensal(1000.00, 100.0, 1.0, 6, Tipos.TipoMovimentacao.Aplicacao), 2);

        [Fact]
        public void TestCalcularMontanteMensalComResgate() => Assert.Equal(446.32, financialService.CalcularMontanteMensal(1000.00, 100.0, 1.0, 6, Tipos.TipoMovimentacao.Resgate), 2);

        [Fact]
        public void TestCalcularMontanteMensalInflacaoComAplicacao() => Assert.Equal(1682.87, financialService.CalcularMontanteMensalInflacao(1000.00, 100.0, 1.5, 6, 0.5, Tipos.TipoMovimentacao.Aplicacao), 2);

        [Fact]
        public void TestCalcularMontanteMensalInflacaoComResgate() => Assert.Equal(446.32, financialService.CalcularMontanteMensalInflacao(1000.00, 100.0, 1.5, 6, 0.5, Tipos.TipoMovimentacao.Resgate), 2);

        [Fact]
        public void TestCalcularMontanteMensalCompletoComAplicacao() => Assert.Equal(1682.87, financialService.CalcularMontanteMensalCompleto(1000.00, 100.0, 1.5, 6, 0.5, 0.0, 0.0, 0.0, Tipos.TipoMovimentacao.Aplicacao, false), 2);

        [Fact]
        public void TestCalcularMontanteMensalCompletoComResgate() => Assert.Equal(446.32, financialService.CalcularMontanteMensalCompleto(1000.00, 100.0, 1.5, 6, 0.5, 0.0, 0.0, 0.0, Tipos.TipoMovimentacao.Resgate, false), 2);
    }
}
