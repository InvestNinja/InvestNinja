using InvestNinja.Core.Infrastructure;
using InvestNinja.Core.Service;
using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace InvestNinja.Core.Test
{
    public class TestsDI
    {
        public TestsDI()
        {
            Container.Initialize(ContainerRegisterAll.RegisterDependenciesReferenced());
        }

        [Fact]
        public void TestDI() => Assert.Equal(typeof(FinancialService), Container.ServiceProvider.GetService<IFinancialService>().GetType());
    }
}
