using InvestNinja.Core.Data;
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

        public Indice(IndiceInitializer initializer)
        {
            Itens = new List<ItemIndice>();
            this.Codigo = initializer.Codigo;
            this.Descricao = initializer.Descricao;
            this.ValorCotaInicial = initializer.ValorCotaInicial;
            AddPrimeiroItem(initializer.DataCota);
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

        public void AddItem(DateTime dataCota, double valorCota)
        {
            ItemIndice item = new ItemIndice()
            {
                DataCota = dataCota,
                VariacaoCotaPercentual = valorCota / this.ValorCotaInicial,
                ValorCota = valorCota,
                VariacaoFinanceira = valorCota - this.ValorCotaInicial
            };
            Itens.Add(item);
        }

        private ItemIndice Last => this.Itens.LastOrDefault() == null ? new ItemIndice() : this.Itens.Last();

        public double ValorCotaAtual => Last.ValorCota;

        public double VariacaoCotaPercentual => Last.VariacaoCotaPercentual;

        public double VariacaoFinanceira => Last.VariacaoFinanceira;
    }
}
