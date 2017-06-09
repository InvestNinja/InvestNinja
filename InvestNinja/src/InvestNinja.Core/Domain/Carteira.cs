using InvestNinja.Core.Data;
using InvestNinja.Core.Tipos;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestNinja.Core.Domain
{
    public class Carteira : IEntity, ICotizacao<ItemCarteira>
    {
        public Carteira()
        {
            Itens = new List<ItemCarteira>();
        }

        public Carteira(string codigo, string descricao, double valorCotaInicial, DateTime dataCota, double saldo)
        {
            Itens = new List<ItemCarteira>();
            this.Codigo = codigo;
            this.Descricao = descricao;
            this.ValorCotaInicial = valorCotaInicial;
            AddPrimeiroItem(dataCota, saldo);
        }

        [BsonId]
        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public double ValorCotaInicial { get; set; }

        public string UserName { get; set; }

        public IList<ItemCarteira> Itens { get; set; }

        private void AddPrimeiroItem(DateTime dataCota, double saldo)
        {
            ItemCarteira item = new ItemCarteira()
            {
                DataCota = dataCota,
                Saldo = saldo,
                QtdCotasAnterior = 0.0,
                QtdCotasMovimentacao = saldo / this.ValorCotaInicial,
                QtdCotasAtual = saldo / this.ValorCotaInicial,
                VariacaoCotaPercentual = 0.0,
                ValorCota = this.ValorCotaInicial,
                VariacaoFinanceira = 0.0,
                ValorMovimentacoes = saldo
            };
            item.Movimentacoes.Add(new MovimentacaoCarteira() { Valor = saldo, Descricao = "Aplicação inicial", Tipo = TipoMovimentacao.Aplicacao });
            Itens.Add(item);
        }

        public void AddItem(DateTime dataCota, double saldo) => Itens.Add(CreateItem(dataCota, saldo, 0.0));

        public void AddItem(DateTime dataCota, double saldo, IList<MovimentacaoCarteira> movimentacoes)
        {
            ItemCarteira item = CreateItem(dataCota, saldo, GetValorMovimentacoes(movimentacoes));
            item.Movimentacoes = movimentacoes;
            Itens.Add(item);
        }

        private double GetValorMovimentacoes(IList<MovimentacaoCarteira> movimentacoes)
        {
            double valorAdicionado = movimentacoes.Where(movimentacao => movimentacao.Tipo != TipoMovimentacao.Resgate).Sum(movimentacao => movimentacao.Valor);
            double valorSubtraido = movimentacoes.Where(movimentacao => movimentacao.Tipo == TipoMovimentacao.Resgate).Sum(movimentacao => movimentacao.Valor);
            return valorAdicionado - valorSubtraido;
        }

        private ItemCarteira CreateItem(DateTime dataCota, double saldo, double valorMovimentacao)
        {
            return new ItemCarteira()
            {
                DataCota = dataCota,
                Saldo = saldo,
                QtdCotasAnterior = this.QtdCotasAtual,
                QtdCotasMovimentacao = valorMovimentacao / (this.ValorCotaAtual * ((saldo - valorMovimentacao) / this.Saldo)),
                QtdCotasAtual = this.QtdCotasAtual + (valorMovimentacao / (this.ValorCotaAtual * ((saldo - valorMovimentacao) / this.Saldo))),
                VariacaoCotaPercentual = (saldo - valorMovimentacao) / this.Saldo,
                ValorCota = this.ValorCotaAtual * ((saldo - valorMovimentacao) / this.Saldo),
                VariacaoFinanceira = (saldo - valorMovimentacao) - this.Saldo,
                ValorMovimentacoes = valorMovimentacao
            };
        }

        private ItemCarteira Last => this.Itens.LastOrDefault() == null ? new ItemCarteira() : this.Itens.Last();

        public double ValorCotaAtual => Last.ValorCota;

        public double VariacaoCotaPercentual => Last.VariacaoCotaPercentual;

        public double VariacaoFinanceira => Last.VariacaoFinanceira;

        public double TotalAplicado => this.Itens.Sum(it => it.ValorMovimentacoes);

        public double QtdCotasAtual => Last.QtdCotasAtual;

        public double Saldo => Last.Saldo;
    }
}
