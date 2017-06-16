using InvestNinja.Core.Converter;
using InvestNinja.Core.Data;
using InvestNinja.Core.Domain;
using InvestNinja.Core.DTO;
using InvestNinja.Core.Infrastructure;
using InvestNinja.Core.Service;
using InvestNinja.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InvestNinja.Core.Test
{
    public class TestsBenchmarkService
    {
        private readonly IRepository<Carteira> repositoryCarteira;
        private readonly IRepository<Indice> repositoryIndice;
        private readonly IBenchmarkService benchmarkService;

        public TestsBenchmarkService()
        {
            Container.Initialize(ContainerRegisterAll.RegisterDependenciesReferenced());
            repositoryCarteira = new MongoRepository<Carteira>();
            repositoryIndice = new MongoRepository<Indice>();
            benchmarkService = new BenchmarkService(repositoryCarteira, repositoryIndice);
        }

        private BenchmarkPesquisaDTO BenchmarkDTO => new BenchmarkPesquisaDTO()
        {
            DataInicio = DateTime.Now.AddDays(-10),
            DataFim = DateTime.Now.AddDays(10),
            Indices = new List<string>() { "CDI", "IBOV" },
            Carteiras = new List<string>() { "ININJA", "IDAKAR", "TESTE" }
        };

        [Fact]
        public void TestCriacaoBenchmark()
        {
            var benchmarks = benchmarkService.GetBenchmark(BenchmarkDTO);
            Assert.True(benchmarks.Count > 0);
        }
    }
}
