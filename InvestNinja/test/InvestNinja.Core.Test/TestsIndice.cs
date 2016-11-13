﻿using InvestNinja.Core.Data;
using InvestNinja.Core.Domain;
using InvestNinja.Data;
using System;
using System.Linq;
using Xunit;

namespace Tests
{
    public class TestsIndice
    {
        private IRepository<Indice> repository;

        public TestsIndice()
        {
            IRepository<Indice> repository = new MongoRepository<Indice>();
        }

        private Indice CreateIndice() => new Indice(new IndiceInitializer("ITest", "Teste", 100.0, DateTime.Now, 1000.0, 1000.0));

        [Fact]
        public void TestCriacaoDeIndice() 
        {
            Indice indice = CreateIndice();
            Assert.Equal(10.0, indice.QtdCotasAtual, 2);
            Assert.Equal(100.0, indice.ValorCotaAtual, 2);
            Assert.Equal(1000.0, indice.Saldo, 2);
            Assert.Equal(1000.0, indice.TotalAplicado, 2);
            Assert.Equal(0.0, indice.VariacaoFinanceira, 2);
            Assert.Equal(0.0, indice.VariacaoCotaPercentual, 2);
        }

        [Fact]
        public void TestCriacaoDePrimeiroItemIndice()
        {
            Indice indice = CreateIndice();
            ItemIndice item = indice.Itens.LastOrDefault();
            Assert.Equal(1000.0, item.ValorMovimentacao, 2);
            Assert.Equal(1000.0, item.Saldo, 2);
            Assert.Equal(0.0, item.QtdCotasAnterior, 2);
            Assert.Equal(10.0, item.QtdCotasMovimentacao, 2);
            Assert.Equal(10.0, item.QtdCotasAtual, 2);
            Assert.Equal(0.0, item.VariacaoCotaPercentual, 2);
            Assert.Equal(0.0, item.VariacaoFinanceira, 2);
            Assert.Equal(100.0, item.ValorCota, 2);
        }

        [Fact]
        public void TesteCriacaoDeItemIndiceSemMovimentacao()
        {
            Indice indice = CreateIndice();
            indice.AddItem(DateTime.Now.AddDays(1), 0.0, 1100.0);
            Assert.Equal(10.0, indice.QtdCotasAtual, 2);
            Assert.Equal(110.0, indice.ValorCotaAtual, 2);
            Assert.Equal(1100.0, indice.Saldo, 2);
            Assert.Equal(1000.0, indice.TotalAplicado, 2);
            Assert.Equal(100.0, indice.VariacaoFinanceira, 2);
            Assert.Equal(1.1, indice.VariacaoCotaPercentual, 2);
        }

        [Fact]
        public void TesteCriacaoDeItemIndiceComAplicacao()
        {
            Indice indice = CreateIndice();
            indice.AddItem(DateTime.Now.AddDays(1), 100.0, 1200.0);
            Assert.Equal(10.91, indice.QtdCotasAtual, 2);
            Assert.Equal(110.0, indice.ValorCotaAtual, 2);
            Assert.Equal(1200.0, indice.Saldo, 2);
            Assert.Equal(1100.0, indice.TotalAplicado, 2);
            Assert.Equal(100.0, indice.VariacaoFinanceira, 2);
            Assert.Equal(1.1, indice.VariacaoCotaPercentual, 2);
        }

        [Fact]
        public void TesteCriacaoDeItemIndiceComResgate()
        {
            Indice indice = CreateIndice();
            indice.AddItem(DateTime.Now.AddDays(1), -100.0, 1000.0);
            Assert.Equal(9.09, indice.QtdCotasAtual, 2);
            Assert.Equal(110.0, indice.ValorCotaAtual, 2);
            Assert.Equal(1000.0, indice.Saldo, 2);
            Assert.Equal(900.0, indice.TotalAplicado, 2);
            Assert.Equal(100.0, indice.VariacaoFinanceira, 2);
            Assert.Equal(1.1, indice.VariacaoCotaPercentual, 2);
        }
    }
}
