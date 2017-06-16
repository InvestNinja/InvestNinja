using InvestNinja.Core.Infrastructure;
using InvestNinja.Core.Service;
using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace InvestNinja.Core.Test
{
    public class TestsBolsaValoresService
    {
        private readonly IBolsaValoresService bolsaValoresService;

        public TestsBolsaValoresService()
        {
            Container.Initialize(ContainerRegisterAll.RegisterDependenciesReferenced());
            bolsaValoresService = Container.ServiceProvider.GetService<IBolsaValoresService>();
        }

        [Fact]
        public void TestCalcularCorretagemTabelaBovespa()
        {
            Assert.Equal(2.70, bolsaValoresService.CalcularCorretagemTabelaBovespa(120.0), 2);
            Assert.Equal(8.00, bolsaValoresService.CalcularCorretagemTabelaBovespa(400.0), 2);
            Assert.Equal(20.49, bolsaValoresService.CalcularCorretagemTabelaBovespa(1200.0), 2);
            Assert.Equal(40.06, bolsaValoresService.CalcularCorretagemTabelaBovespa(3000.0), 2);
            Assert.Equal(50.21, bolsaValoresService.CalcularCorretagemTabelaBovespa(5000.0), 2);
        }

        [Fact]
        public void TestCalcularEmolumentos()
        {
            Assert.Equal(0.057, bolsaValoresService.CalcularEmolumentos(300.0, true), 3);
            Assert.Equal(0.081, bolsaValoresService.CalcularEmolumentos(300.0, false), 3);
        }

        [Fact]
        public void TestCalcularLiquidacao()
        {
            Assert.Equal(0.024, bolsaValoresService.CalcularLiquidacao(300.0, true), 3);
            Assert.Equal(0.018, bolsaValoresService.CalcularLiquidacao(300.0, false), 3);
        }

        [Fact]
        public void TestCalcularPrecoMinimoVendaEmpate()
        {
            Assert.Equal(25.40, bolsaValoresService.CalcularPrecoMinimoVendaEmpate(25.0, 100, 40.00), 2);
        }

        [Fact]
        public void TestCalcularTaxas()
        {
            Assert.Equal(0.081, bolsaValoresService.CalcularTaxas(300.0, true), 3);
            Assert.Equal(0.099, bolsaValoresService.CalcularTaxas(300.0, false), 3);
        }

        [Fact]
        public void TestCalcularValorizacaoVendaDesejada()
        {
            Assert.Equal(20.00, bolsaValoresService.CalcularValorizacaoVendaDesejada(25.0, 30.0), 2);
        }

        [Fact]
        public void TestCalcularValorizacaoVendaDesejadaQtdAtivos()
        {
            Assert.Throws<NotImplementedException>(() => bolsaValoresService.CalcularValorizacaoVendaDesejada(12.0, 15.0, 300));
        }
    }
}
