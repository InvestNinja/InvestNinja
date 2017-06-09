using InvestNinja.Core.Domain;
using InvestNinja.Core.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace InvestNinja.Core.Test
{
    public class TestsCarteira
    {
        private Carteira CreateCarteira() => new Carteira("ITest", "Teste", 100.0, DateTime.Now, 1000.0);

        [Fact]
        public void TestCriacaoDeCarteira() 
        {
            Carteira Carteira = CreateCarteira();
            Assert.Equal(10.0, Carteira.QtdCotasAtual, 2);
            Assert.Equal(100.0, Carteira.ValorCotaAtual, 2);
            Assert.Equal(1000.0, Carteira.Saldo, 2);
            Assert.Equal(1000.0, Carteira.TotalAplicado, 2);
            Assert.Equal(0.0, Carteira.VariacaoFinanceira, 2);
            Assert.Equal(0.0, Carteira.VariacaoCotaPercentual, 2);
        }

        [Fact]
        public void TestCriacaoDePrimeiroItemCarteira()
        {
            Carteira Carteira = CreateCarteira();
            ItemCarteira item = Carteira.Itens.LastOrDefault();
            Assert.Equal(1000.0, item.ValorMovimentacoes, 2);
            Assert.Equal(1000.0, item.Saldo, 2);
            Assert.Equal(0.0, item.QtdCotasAnterior, 2);
            Assert.Equal(10.0, item.QtdCotasMovimentacao, 2);
            Assert.Equal(10.0, item.QtdCotasAtual, 2);
            Assert.Equal(0.0, item.VariacaoCotaPercentual, 2);
            Assert.Equal(0.0, item.VariacaoFinanceira, 2);
            Assert.Equal(100.0, item.ValorCota, 2);
        }

        [Fact]
        public void TesteCarteiraComAdicaoDeSegundoItemCarteiraSemMovimentacao()
        {
            Carteira Carteira = CreateCarteira();
            Carteira.AddItem(DateTime.Now.AddDays(1), 1100.0);
            Assert.Equal(10.0, Carteira.QtdCotasAtual, 2);
            Assert.Equal(110.0, Carteira.ValorCotaAtual, 2);
            Assert.Equal(1100.0, Carteira.Saldo, 2);
            Assert.Equal(1000.0, Carteira.TotalAplicado, 2);
            Assert.Equal(100.0, Carteira.VariacaoFinanceira, 2);
            Assert.Equal(1.1, Carteira.VariacaoCotaPercentual, 2);
        }

        [Fact]
        public void TesteCriacaoDeSegundoItemCarteiraSemMovimentacao()
        {
            Carteira Carteira = CreateCarteira();
            Carteira.AddItem(DateTime.Now.AddDays(1), 1100.0);
            ItemCarteira item = Carteira.Itens.LastOrDefault();
            Assert.Equal(0.0, item.ValorMovimentacoes, 2);
            Assert.Equal(1100.0, item.Saldo, 2);
            Assert.Equal(10.0, item.QtdCotasAnterior, 2);
            Assert.Equal(0.0, item.QtdCotasMovimentacao, 2);
            Assert.Equal(10.0, item.QtdCotasAtual, 2);
            Assert.Equal(1.1, item.VariacaoCotaPercentual, 2);
            Assert.Equal(100.0, item.VariacaoFinanceira, 2);
            Assert.Equal(110.0, item.ValorCota, 2);
        }

        [Fact]
        public void TesteCarteiraComAdicaoDeSegundoItemCarteiraComAplicacao()
        {
            Carteira Carteira = CreateCarteira();
            var list = new List<MovimentacaoCarteira>()
            {
                new MovimentacaoCarteira()
                {
                    Valor = 100.0,
                    Descricao = "Aplicação",
                    Tipo = TipoMovimentacao.Aplicacao
                }
            };
            Carteira.AddItem(DateTime.Now.AddDays(1), 1200.0, list);
            Assert.Equal(10.91, Carteira.QtdCotasAtual, 2);
            Assert.Equal(110.0, Carteira.ValorCotaAtual, 2);
            Assert.Equal(1200.0, Carteira.Saldo, 2);
            Assert.Equal(1100.0, Carteira.TotalAplicado, 2);
            Assert.Equal(100.0, Carteira.VariacaoFinanceira, 2);
            Assert.Equal(1.1, Carteira.VariacaoCotaPercentual, 2);
        }

        [Fact]
        public void TesteCriacaoDeSegundoItemCarteiraComAplicacao()
        {
            Carteira Carteira = CreateCarteira();
            var list = new List<MovimentacaoCarteira>()
            {
                new MovimentacaoCarteira()
                {
                    Valor = 100.0,
                    Descricao = "Aplicação",
                    Tipo = TipoMovimentacao.Aplicacao
                }
            };
            Carteira.AddItem(DateTime.Now.AddDays(1), 1200.0, list);
            ItemCarteira item = Carteira.Itens.LastOrDefault();
            Assert.Equal(100.0, item.ValorMovimentacoes, 2);
            Assert.Equal(1200.0, item.Saldo, 2);
            Assert.Equal(10.0, item.QtdCotasAnterior, 2);
            Assert.Equal(0.91, item.QtdCotasMovimentacao, 2);
            Assert.Equal(10.91, item.QtdCotasAtual, 2);
            Assert.Equal(1.1, item.VariacaoCotaPercentual, 2);
            Assert.Equal(100.0, item.VariacaoFinanceira, 2);
            Assert.Equal(110.0, item.ValorCota, 2);
        }

        [Fact]
        public void TesteCarteiraComAdicaoDeSegundoItemCarteiraComResgate()
        {
            Carteira Carteira = CreateCarteira();
            var list = new List<MovimentacaoCarteira>()
            {
                new MovimentacaoCarteira()
                {
                    Valor = 100.0,
                    Descricao = "Resgate",
                    Tipo = TipoMovimentacao.Resgate
                }
            };
            Carteira.AddItem(DateTime.Now.AddDays(1), 1000.0, list);
            Assert.Equal(9.09, Carteira.QtdCotasAtual, 2);
            Assert.Equal(110.0, Carteira.ValorCotaAtual, 2);
            Assert.Equal(1000.0, Carteira.Saldo, 2);
            Assert.Equal(900.0, Carteira.TotalAplicado, 2);
            Assert.Equal(100.0, Carteira.VariacaoFinanceira, 2);
            Assert.Equal(1.1, Carteira.VariacaoCotaPercentual, 2);
        }

        [Fact]
        public void TesteCriacaoDeSegundoItemCarteiraComResgate()
        {
            Carteira Carteira = CreateCarteira();
            var list = new List<MovimentacaoCarteira>()
            {
                new MovimentacaoCarteira()
                {
                    Valor = 100.0,
                    Descricao = "Resgate",
                    Tipo = TipoMovimentacao.Resgate
                }
            };
            Carteira.AddItem(DateTime.Now.AddDays(1), 1000.0, list);
            ItemCarteira item = Carteira.Itens.LastOrDefault();
            Assert.Equal(-100.0, item.ValorMovimentacoes, 2);
            Assert.Equal(1000.0, item.Saldo, 2);
            Assert.Equal(10.0, item.QtdCotasAnterior, 2);
            Assert.Equal(-0.91, item.QtdCotasMovimentacao, 2);
            Assert.Equal(9.09, item.QtdCotasAtual, 2);
            Assert.Equal(1.1, item.VariacaoCotaPercentual, 2);
            Assert.Equal(100.0, item.VariacaoFinanceira, 2);
            Assert.Equal(110.0, item.ValorCota, 2);
        }

        [Fact]
        public void TesteCarteiraComAdicaoDeSegundoItemCarteiraComMovimentacoes()
        {
            Carteira Carteira = CreateCarteira();
            var list = new List<MovimentacaoCarteira>()
            {
                new MovimentacaoCarteira()
                {
                    Valor = 200.0,
                    Descricao = "Resgate",
                    Tipo = TipoMovimentacao.Resgate
                },
                new MovimentacaoCarteira()
                {
                    Valor = 100.0,
                    Descricao = "Aluguel",
                    Tipo = TipoMovimentacao.Rendimento
                }
            };
            Carteira.AddItem(DateTime.Now.AddDays(1), 1000.0, list);
            Assert.Equal(9.09, Carteira.QtdCotasAtual, 2);
            Assert.Equal(110.0, Carteira.ValorCotaAtual, 2);
            Assert.Equal(1000.0, Carteira.Saldo, 2);
            Assert.Equal(900.0, Carteira.TotalAplicado, 2);
            Assert.Equal(100.0, Carteira.VariacaoFinanceira, 2);
            Assert.Equal(1.1, Carteira.VariacaoCotaPercentual, 2);
        }

        [Fact]
        public void TesteCriacaoDeTerceiroItemCarteiraComMovimentacoes()
        {
            Carteira Carteira = CreateCarteira();
            var list = new List<MovimentacaoCarteira>()
            {
                new MovimentacaoCarteira()
                {
                    Valor = 200.0,
                    Descricao = "Resgate",
                    Tipo = TipoMovimentacao.Resgate
                },
                new MovimentacaoCarteira()
                {
                    Valor = 100.0,
                    Descricao = "Aluguel",
                    Tipo = TipoMovimentacao.Rendimento
                }
            };
            Carteira.AddItem(DateTime.Now.AddDays(1), 1000.0, list);
            ItemCarteira item = Carteira.Itens.LastOrDefault();
            Assert.Equal(-100.0, item.ValorMovimentacoes, 2);
            Assert.Equal(1000.0, item.Saldo, 2);
            Assert.Equal(10.0, item.QtdCotasAnterior, 2);
            Assert.Equal(-0.91, item.QtdCotasMovimentacao, 2);
            Assert.Equal(9.09, item.QtdCotasAtual, 2);
            Assert.Equal(1.1, item.VariacaoCotaPercentual, 2);
            Assert.Equal(100.0, item.VariacaoFinanceira, 2);
            Assert.Equal(110.0, item.ValorCota, 2);
        }
    }
}
