using InvestNinja.Core.Infrastructure;
using InvestNinja.Core.Service;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using InvestNinja.Core.Data;
using InvestNinja.Core.Domain.Carteira;
using InvestNinja.Core.Domain;

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

        [Fact]
        public void TestDIRepositoryIsType() => Assert.IsType<MongoRepository<Carteira>>(Container.ServiceProvider.GetService<IRepository<Carteira>>());

        [Fact]
        public void TestDIRepositoryIsNotType() => Assert.IsNotType<MongoRepository<Indice>>(Container.ServiceProvider.GetService<IRepository<Carteira>>());
    }
}
