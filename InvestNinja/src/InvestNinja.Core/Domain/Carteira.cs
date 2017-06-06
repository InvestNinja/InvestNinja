using InvestNinja.Core.Data;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestNinja.Core.Domain
{
    public class Carteira : IEntity
    {
        public Carteira()
        {
            Itens = new List<ItemCarteira>();
        }

        public Carteira(CarteiraInitializer initializer)
        {
            Itens = new List<ItemCarteira>();
            this.Codigo = initializer.Codigo;
            this.Descricao = initializer.Descricao;
            this.ValorCotaInicial = initializer.ValorCotaInicial;
            AddPrimeiroItem(initializer.DataCota, initializer.Saldo);
        }

        [BsonId]
        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public double ValorCotaInicial { get; set; }

        public IList<ItemCarteira> Itens { get; set; }

        private void AddPrimeiroItem(DateTime dataCota, double saldo)
        {
            ItemCarteira item = new ItemCarteira()
            {
                DataCota = dataCota,
                ValorMovimentacao = saldo,
                Saldo = saldo,
                QtdCotasAnterior = 0.0,
                QtdCotasMovimentacao = saldo / this.ValorCotaInicial,
                QtdCotasAtual = saldo / this.ValorCotaInicial,
                VariacaoCotaPercentual = 0.0,
                ValorCota = this.ValorCotaInicial,
                VariacaoFinanceira = 0.0
            };
            Itens.Add(item);
        }

        public void AddItem(DateTime dataCota, double valorMovimentacao, double saldo)
        {
            ItemCarteira item = new ItemCarteira()
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

        private ItemCarteira Last => this.Itens.LastOrDefault() == null ? new ItemCarteira() : this.Itens.Last();

        public double ValorCotaAtual => Last.ValorCota;

        public double VariacaoCotaPercentual => Last.VariacaoCotaPercentual;

        public double VariacaoFinanceira => Last.VariacaoFinanceira;

        public double TotalAplicado => this.Itens.Sum(it => it.ValorMovimentacao);

        public double QtdCotasAtual => Last.QtdCotasAtual;

        public double Saldo => Last.Saldo;
    }
}
