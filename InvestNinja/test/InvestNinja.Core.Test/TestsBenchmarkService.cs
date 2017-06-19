using InvestNinja.Core.Data;
using InvestNinja.Core.Domain;
using InvestNinja.Core.Domain.Carteira;
using InvestNinja.Core.DTO;
using InvestNinja.Core.Infrastructure;
using InvestNinja.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace InvestNinja.Core.Test
{
    public class TestsBenchmarkService : IDisposable
    {
        private readonly IRepository<Carteira> repositoryCarteira;
        private readonly IRepository<Indice> repositoryIndice;
        private readonly IBenchmarkService benchmarkService;

        public TestsBenchmarkService()
        {
            Container.Initialize(ContainerRegisterAll.RegisterDependenciesReferenced());
            repositoryCarteira = Container.ServiceProvider.GetService<IRepository<Carteira>>();
            repositoryIndice = Container.ServiceProvider.GetService<IRepository<Indice>>();
            CreateCarteira();
            CreateIndice();
            benchmarkService = Container.ServiceProvider.GetService<IBenchmarkService>();
        }

        private void CreateCarteira()
        {
            var carteiraDelete = repositoryCarteira.GetById("CB");
            if (carteiraDelete != null)
                repositoryCarteira.Delete(carteiraDelete);
            Carteira carteira = new Carteira("CB", "Carteira Benchmark", 100.0, DateTime.Now.AddDays(-2), 1000.0);
            carteira.AddItem(DateTime.Now.AddDays(-1), 1100.0);
            carteira.AddItem(DateTime.Now, 1210.0);
            repositoryCarteira.Insert(carteira);
        }

        private void CreateIndice()
        {
            var indiceDelete = repositoryIndice.GetById("IB");
            if (indiceDelete != null)
                repositoryIndice.Delete(indiceDelete);
            Indice indice = new Indice("IB", "Indice Benchmark", 100.0, DateTime.Now.AddDays(-2));
            indice.AddItemByValorCota(DateTime.Now.AddDays(-1), 110.0);
            indice.AddItemByValorCota(DateTime.Now, 121.0);
            repositoryIndice.Insert(indice);
        }

        private BenchmarkPesquisaDTO BenchmarkDTO => new BenchmarkPesquisaDTO()
        {
            DataInicio = DateTime.Now.AddDays(-1),
            DataFim = DateTime.Now,
            Indices = new List<string>() { "IB" },
            Carteiras = new List<string>() { "CB" }
        };

        [Fact]
        public void TestCriacaoBenchmark()
        {
            var benchmarks = benchmarkService.GetBenchmark(BenchmarkDTO);
            Assert.Equal(2, benchmarks.Count);
            Assert.Equal(4, benchmarks.Sum(indice => indice.Itens.Count));
            foreach(Indice indice in benchmarks)
            {
                Assert.Equal(1.1, indice.ValorCotaAtual);
                Assert.Equal(1.1, indice.VariacaoCotaPercentual);
            }
        }

        public void Dispose()
        {
            repositoryIndice.Delete(repositoryIndice.GetById("IB"));
            repositoryCarteira.Delete(repositoryCarteira.GetById("CB"));
        }
    }
}
