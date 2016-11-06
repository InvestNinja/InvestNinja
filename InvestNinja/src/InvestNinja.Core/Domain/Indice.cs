using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestNinja.Core.Domain
{
    public class Indice
    {
        public Indice()
        {
            Itens = new List<ItemIndice>();
        }

        public Indice(string codigo, string Descricao, double valorCotaInicial, DateTime dataCota, double valorMovimentacao, double saldo)
        {
            Itens = new List<ItemIndice>();
            this.Codigo = codigo;
            this.Descricao = Descricao;
            this.ValorCotaInicial = valorCotaInicial;
            AddPrimeiroItem(dataCota, valorCotaInicial, saldo);
        }

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

        public double ValorCotaAtual => this.Itens.Last().ValorCota;

        public double VariacaoCotaPercentual => this.Itens.Last().VariacaoCotaPercentual;

        public double VariacaoFinanceira => this.Itens.Last().VariacaoFinanceira;

        public double TotalAplicado => this.Itens.Sum(it => it.ValorMovimentacao);

        public double QtdCotasAtual => this.Itens.Last().QtdCotasAtual;

        public double Saldo => this.Itens.Last().Saldo;
    }
}
