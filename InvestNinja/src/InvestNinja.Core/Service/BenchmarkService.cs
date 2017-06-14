using System;
using System.Collections.Generic;
using System.Text;
using InvestNinja.Core.Domain;
using InvestNinja.Core.DTO;
using InvestNinja.Core.Data;
using InvestNinja.Core.Utils;
using System.Linq;

namespace InvestNinja.Core.Service
{
    public class BenchmarkService : IBenchmarkService
    {
        private readonly IRepository<Carteira> repositoryCarteira;
        private readonly IRepository<Indice> repositoryIndice;

        public BenchmarkService(IRepository<Carteira> repositoryCarteira, IRepository<Indice> repositoryIndice)
        {
            this.repositoryCarteira = repositoryCarteira;
            this.repositoryIndice = repositoryIndice;
        }

        public IList<ICotizacao<IItemCotizacao>> GetBenchmark(BenchmarkPesquisaDTO benchmarkDTO)
        {
            IList<ICotizacao<IItemCotizacao>> listCotizacao = new List<ICotizacao<IItemCotizacao>>();
            benchmarkDTO.Indices.ForEach(indice => listCotizacao.Add(GetCotizacaoAjustadaPorData(repositoryIndice.GetById(indice), benchmarkDTO.DataInicio, benchmarkDTO.DataFim)));
            benchmarkDTO.Carteiras.ForEach(carteira => listCotizacao.Add(GetCotizacaoAjustadaPorData(CarteiraToIndice(repositoryCarteira.GetById(carteira)), benchmarkDTO.DataInicio, benchmarkDTO.DataFim)));
            return listCotizacao.Where(cotizacao => cotizacao != null).ToList();
        }

        private ICotizacao<IItemCotizacao> GetCotizacaoAjustadaPorData(ICotizacao<IItemCotizacao> cotizacaoCompleta, DateTime dataInicio, DateTime dataFim)
        {
            var itensFiltered = cotizacaoCompleta?.Itens?.Where(item => item.DataCota >= dataInicio && item.DataCota <= dataFim)?.ToList();
            if (itensFiltered?.Count > 0)
            {
                Indice indiceAjustado = new Indice(cotizacaoCompleta.Codigo, cotizacaoCompleta.Descricao, 1.0, itensFiltered.First().DataCota);
                itensFiltered.RemoveAt(0);
                itensFiltered.ForEach(item => indiceAjustado.AddItemByVariacaoCota(item.DataCota, item.VariacaoCotaPercentual));
                return indiceAjustado;
            }
            return null;
        }

        private Indice CarteiraToIndice(Carteira carteira)
        {
            if (carteira != null)
            {
                Indice indice = new Indice(carteira.Codigo, carteira.Descricao, carteira.ValorCotaInicial, carteira.Itens.First().DataCota);
                carteira.Itens.RemoveAt(0);
                carteira.Itens.ForEach(itemCarteira => indice.AddItemByVariacaoCota(itemCarteira.DataCota, itemCarteira.VariacaoCotaPercentual));
                return indice;
            }
            return null;
        }
    }
}
