using InvestNinja.ClassFinder;
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
    public class TestsDI
    {
        private readonly IServiceProvider serviceProvider;

        public TestsDI()
        {
            serviceProvider = ContainerRegisterAll.RegisterDependenciesReferenced();
        }

        [Fact]
        public void TestLoader()
        {
            IAssemblyLoader assemblyLoader = new DependencyContextLoader();
            var listAssemblies = assemblyLoader.GetAssemblies();
            Assert.Equal(1, listAssemblies.Where(assembly => assembly.ManifestModule.Name == "InvestNinja.Core.dll").Count());
        }

        [Fact]
        public void TestDI()
        {
            var financialService = serviceProvider.GetService<IFinancialService>();
            Assert.Equal(typeof(FinancialService), financialService.GetType());
            Assert.Equal(1061.52, financialService.CalcularMontante(1000.00, 1.0, 6), 2);
        }
    }
}
