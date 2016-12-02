using InvestNinja.Core.Infrastructure;
using InvestNinja.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace InvestNinja.Core.Test
{
    public class TestsImpostoRendaService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IImpostoRendaService impostoRendaService;

        public TestsImpostoRendaService()
        {
            serviceProvider = ContainerRegisterAll.RegisterDependenciesReferenced();
            impostoRendaService = serviceProvider.GetService<IImpostoRendaService>();
        }

        [Fact]
        public void TestCalcularAliquotaIRTabelaRegressiva()
        {
            Assert.Equal(22.5, impostoRendaService.CalcularAliquotaIRTabelaRegressiva(100, false));
            Assert.Equal(20.0, impostoRendaService.CalcularAliquotaIRTabelaRegressiva(200, false));
            Assert.Equal(17.5, impostoRendaService.CalcularAliquotaIRTabelaRegressiva(400, false));
            Assert.Equal(15.0, impostoRendaService.CalcularAliquotaIRTabelaRegressiva(800, false));
            Assert.Equal(20.0, impostoRendaService.CalcularAliquotaIRTabelaRegressiva(800, true));
        }

        [Fact]
        public void TestCalcularAliquotaIRTabelaRegressivaPrevidencia()
        {
            Assert.Equal(35.0, impostoRendaService.CalcularAliquotaIRTabelaRegressivaPrevidencia(1));
            Assert.Equal(30.0, impostoRendaService.CalcularAliquotaIRTabelaRegressivaPrevidencia(3));
            Assert.Equal(25.0, impostoRendaService.CalcularAliquotaIRTabelaRegressivaPrevidencia(5));
            Assert.Equal(20.0, impostoRendaService.CalcularAliquotaIRTabelaRegressivaPrevidencia(7));
            Assert.Equal(15.0, impostoRendaService.CalcularAliquotaIRTabelaRegressivaPrevidencia(9));
            Assert.Equal(10.0, impostoRendaService.CalcularAliquotaIRTabelaRegressivaPrevidencia(11));
        }
    }
}
