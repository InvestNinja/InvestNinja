using InvestNinja.Core.Domain;
using System;
using System.Linq;
using Xunit;

namespace InvestNinja.Core.Test
{
    public class TestsIndice
    {
        private Indice CreateIndice() => new Indice("ITest", "Teste", 100.0, DateTime.Now);

        [Fact]
        public void TestCriacaoDeIndice() 
        {
            Indice indice = CreateIndice();
            Assert.Equal(100.0, indice.ValorCotaAtual, 2);
            Assert.Equal(0.0, indice.VariacaoFinanceira, 2);
            Assert.Equal(0.0, indice.VariacaoCotaPercentual, 2);
        }

        [Fact]
        public void TestCriacaoDePrimeiroItemIndice()
        {
            Indice indice = CreateIndice();
            IItemCotizacao item = indice.Itens.LastOrDefault();
            Assert.Equal(0.0, item.VariacaoCotaPercentual, 2);
            Assert.Equal(0.0, item.VariacaoFinanceira, 2);
            Assert.Equal(100.0, item.ValorCota, 2);
        }

        [Fact]
        public void TesteIndiceComAdicaoDeSegundoItemIndice()
        {
            Indice indice = CreateIndice();
            indice.AddItemByValorCota(DateTime.Now.AddDays(1), 110.0);
            Assert.Equal(110.0, indice.ValorCotaAtual, 2);
            Assert.Equal(10.0, indice.VariacaoFinanceira, 2);
            Assert.Equal(1.1, indice.VariacaoCotaPercentual, 2);
        }

        [Fact]
        public void TesteCriacaoDeSegundoItemIndice()
        {
            Indice indice = CreateIndice();
            indice.AddItemByValorCota(DateTime.Now.AddDays(1), 110.0);
            IItemCotizacao item = indice.Itens.LastOrDefault();
            Assert.Equal(1.1, item.VariacaoCotaPercentual, 2);
            Assert.Equal(10.0, item.VariacaoFinanceira, 2);
            Assert.Equal(110.0, item.ValorCota, 2);
        }

        [Fact]
        public void TesteIndiceComAdicaoDeSegundoItemIndiceByPercentual()
        {
            Indice indice = CreateIndice();
            indice.AddItemByVariacaoCota(DateTime.Now.AddDays(1), 1.1);
            Assert.Equal(110.0, indice.ValorCotaAtual, 2);
            Assert.Equal(10.0, indice.VariacaoFinanceira, 2);
            Assert.Equal(1.1, indice.VariacaoCotaPercentual, 2);
        }

        [Fact]
        public void TesteCriacaoDeSegundoItemIndiceByPercentual()
        {
            Indice indice = CreateIndice();
            indice.AddItemByVariacaoCota(DateTime.Now.AddDays(1), 1.1);
            IItemCotizacao item = indice.Itens.LastOrDefault();
            Assert.Equal(1.1, item.VariacaoCotaPercentual, 2);
            Assert.Equal(10.0, item.VariacaoFinanceira, 2);
            Assert.Equal(110.0, item.ValorCota, 2);
        }
    }
}
