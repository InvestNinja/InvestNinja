using InvestNinja.Core.Infrastructure;
using InvestNinja.Core.Service;
using System;
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
        public void TestDI() => Assert.Equal(typeof(FinancialService), serviceProvider.GetService<IFinancialService>().GetType());
    }
}
