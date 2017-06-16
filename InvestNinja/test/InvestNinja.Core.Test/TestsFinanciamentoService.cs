using System;
using Microsoft.Extensions.DependencyInjection;
using InvestNinja.Core.Infrastructure;
using InvestNinja.Core.Service;
using Xunit;

namespace InvestNinja.Core.Test
{
    public class TestsFinanciamentoService
    {
        private readonly IFinanciamentoService financiamentoService;

        public TestsFinanciamentoService()
        {
            Container.Initialize(ContainerRegisterAll.RegisterDependenciesReferenced());
            financiamentoService = Container.ServiceProvider.GetService<IFinanciamentoService>();
        }

        [Fact]
        public void TestCalcularParcelaTabelaPrice()
        {
            Assert.Equal(2224.44, financiamentoService.CalcularParcelaTabelaPrice(100000.00, 0.01, 60), 2);
            Assert.Equal(2750.40, financiamentoService.CalcularParcelaTabelaPrice(30000.00, 0.015, 12), 2);
        }

        [Fact]
        public void TestCalcularTotalJurosTabelaPrice() => Assert.Equal(3004.80, financiamentoService.CalcularTotalJurosTabelaPrice(30000.00, 0.015, 12), 2);

        [Fact]
        public void TestCalcularTotalJurosTabelaSac()
        {
            Assert.Throws<NotImplementedException>(() => financiamentoService.CalcularTotalJurosTabelaSac(30000.00, 1.5, 12));
            //Assert.Equal(2708.24, financiamentoService.CalcularTotalJurosTabelaSac(30000.00, 1.5, 12), 2);
        }
    }
}
