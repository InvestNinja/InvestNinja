using InvestNinja.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using InvestNinja.Core.Infrastructure;

namespace InvestNinja.Core.Test
{
    public class TestsFinancialService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IFinancialService financialService;

        public TestsFinancialService()
        {
            serviceProvider = ContainerRegisterAll.RegisterDependenciesReferenced();
            financialService = serviceProvider.GetService<IFinancialService>();
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
        public void TestCalcularMontanteMensalCompletoComAplicacao()
        {
            var montante = financialService.CalcularMontanteMensalCompleto(1000.00, 100.0, 1.5, 6, 0.5, 0.0, 0.0, 0.0, Tipos.TipoMovimentacao.Aplicacao, false);
            Assert.Equal(1682.87, montante, 2);
        }

        [Fact]
        public void TestCalcularMontanteMensalCompletoComResgate()
        {
            var montante = financialService.CalcularMontanteMensalCompleto(1000.00, 100.0, 1.5, 6, 0.5, 0.0, 0.0, 0.0, Tipos.TipoMovimentacao.Resgate, false);
            Assert.Equal(446.32, montante, 2);
        }
    }
}
