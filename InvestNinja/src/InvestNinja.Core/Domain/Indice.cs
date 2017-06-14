using InvestNinja.Core.Data;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestNinja.Core.Domain
{
    public class Indice : IEntity, ICotizacao<IItemCotizacao>
    {
        public Indice()
        {
            Itens = new List<IItemCotizacao>();
        }

        public Indice(string codigo, string descricao, double valorCotaInicial, DateTime dataCota)
        {
            Itens = new List<IItemCotizacao>();
            this.Codigo = codigo;
            this.Descricao = descricao;
            this.ValorCotaInicial = valorCotaInicial;
            AddPrimeiroItem(dataCota);
        }

        [BsonId]
        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public double ValorCotaInicial { get; set; }

        public IList<IItemCotizacao> Itens { get; set; }

        private void AddPrimeiroItem(DateTime dataCota)
        {
            ItemIndice item = new ItemIndice()
            {
                DataCota = dataCota,
                VariacaoCotaPercentual = 0.0,
                ValorCota = this.ValorCotaInicial,
                VariacaoFinanceira = 0.0
            };
            Itens.Add(item);
        }

        public void AddItemByValorCota(DateTime dataCota, double valorCota)
        {
            ItemIndice item = new ItemIndice()
            {
                DataCota = dataCota,
                VariacaoCotaPercentual = valorCota / this.ValorCotaAtual,
                ValorCota = valorCota,
                VariacaoFinanceira = valorCota - this.ValorCotaAtual
            };
            Itens.Add(item);
        }

        public void AddItemByVariacaoCota(DateTime dataCota, double variacaoCotaPercentual) => AddItemByValorCota(dataCota, Last.ValorCota * variacaoCotaPercentual);

        private ItemIndice Last => this.Itens.LastOrDefault() == null ? new ItemIndice() : (ItemIndice)this.Itens.Last();

        public double ValorCotaAtual => Last.ValorCota;

        public double VariacaoCotaPercentual => Last.VariacaoCotaPercentual;

        public double VariacaoFinanceira => Last.VariacaoFinanceira;
    }
}
