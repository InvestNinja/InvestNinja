﻿using InvestNinja.Core.Data;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestNinja.Core.Domain
{
    public class Indice : IEntity, ICotizacao<ItemIndice>
    {
        public Indice()
        {
            Itens = new List<ItemIndice>();
        }

        public Indice(string codigo, string descricao, double valorCotaInicial, DateTime dataCota)
        {
            Itens = new List<ItemIndice>();
            this.Codigo = codigo;
            this.Descricao = descricao;
            this.ValorCotaInicial = valorCotaInicial;
            AddPrimeiroItem(dataCota);
        }

        [BsonId]
        public string Codigo { get; set; }

        public string Descricao { get; set; }

        public double ValorCotaInicial { get; set; }

        public IList<ItemIndice> Itens { get; set; }

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
            ValidateNewItemDate(dataCota);
            ItemIndice item = new ItemIndice()
            {
                DataCota = dataCota,
                VariacaoCotaPercentual = valorCota / this.ValorCotaAtual,
                ValorCota = valorCota,
                VariacaoFinanceira = valorCota - this.ValorCotaAtual
            };
            Itens.Add(item);
        }

        private void ValidateNewItemDate(DateTime dataCota)
        {
            if (dataCota.Date <= Last.DataCota)
                throw new Exception("Item adicionado com data menor ou igual à data do último item.");
        }

        public void AddItemByVariacaoCota(DateTime dataCota, double variacaoCotaPercentual) => AddItemByValorCota(dataCota, Last.ValorCota * variacaoCotaPercentual);

        private ItemIndice Last => this.Itens.LastOrDefault() == null ? new ItemIndice() : this.Itens.Last();

        public double ValorCotaAtual => Last.ValorCota;

        public double VariacaoCotaPercentual => Last.VariacaoCotaPercentual;

        public double VariacaoFinanceira => Last.VariacaoFinanceira;
    }
}
