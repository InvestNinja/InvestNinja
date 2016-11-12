using InvestNinja.Core.Data;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InvestNinja.Core.Domain
{
    public class Indice : IEntity
    {
        public Indice()
        {
            Itens = new List<ItemIndice>();
        }

        public Indice(IndiceInitializer initializer)
        {
            Itens = new List<ItemIndice>();
            this.Codigo = initializer.Codigo;
            this.Descricao = initializer.Descricao;
            this.ValorCotaInicial = initializer.ValorCotaInicial;
            AddPrimeiroItem(initializer.DataCota, initializer.ValorCotaInicial, initializer.Saldo);
        }

        [BsonId]
        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public double ValorCotaInicial { get; set; }

        public IList<ItemIndice> Itens { get; set; }

        private void AddPrimeiroItem(DateTime dataCota, double valorMovimentacao, double saldo)
        {
            ItemIndice item = new ItemIndice()
            {
                DataCota = dataCota,
                ValorMovimentacao = valorMovimentacao,
                Saldo = saldo,
                QtdCotasAnterior = 0.0,
                QtdCotasMovimentacao = valorMovimentacao / this.ValorCotaInicial,
                QtdCotasAtual = valorMovimentacao / this.ValorCotaInicial,
                VariacaoCotaPercentual = 0.0,
                ValorCota = this.ValorCotaInicial,
                VariacaoFinanceira = 0.0
            };
            Itens.Add(item);
        }

        public void AddItem(DateTime dataCota, double valorMovimentacao, double saldo)
        {
            ItemIndice item = new ItemIndice()
            {
                DataCota = dataCota,
                ValorMovimentacao = valorMovimentacao,
                Saldo = saldo,
                QtdCotasAnterior = this.QtdCotasAtual,
                QtdCotasMovimentacao = valorMovimentacao / (this.ValorCotaAtual * ((saldo - valorMovimentacao) / this.Saldo)),
                QtdCotasAtual = this.QtdCotasAtual + (valorMovimentacao / (this.ValorCotaAtual * ((saldo - valorMovimentacao) / this.Saldo))),
                VariacaoCotaPercentual = (saldo - valorMovimentacao) / this.Saldo,
                ValorCota = this.ValorCotaAtual * ((saldo - valorMovimentacao) / this.Saldo),
                VariacaoFinanceira = (saldo - valorMovimentacao) - this.Saldo
            };
            Itens.Add(item);
        }

        private ItemIndice Last => this.Itens.LastOrDefault() == null ? new ItemIndice() : this.Itens.Last();

        public double ValorCotaAtual => Last.ValorCota;

        public double VariacaoCotaPercentual => Last.VariacaoCotaPercentual;

        public double VariacaoFinanceira => Last.VariacaoFinanceira;

        public double TotalAplicado => this.Itens.Sum(it => it.ValorMovimentacao);

        public double QtdCotasAtual => Last.QtdCotasAtual;

        public double Saldo => Last.Saldo;
    }
}
