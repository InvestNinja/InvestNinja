using System;
using System.Collections.Generic;
using System.Text;
using InvestNinja.Core.Domain;
using InvestNinja.Core.DTO;
using InvestNinja.Core.Data;
using InvestNinja.Core.Utils;
using System.Linq;
using InvestNinja.Core.Converter;

namespace InvestNinja.Core.Service
{
    public class BenchmarkService : IBenchmarkService
    {
        private readonly IRepository<Carteira> repositoryCarteira;
        private readonly IRepository<Indice> repositoryIndice;
        private readonly IConvertIndice convertIndice;

        public BenchmarkService(IRepository<Carteira> repositoryCarteira, IRepository<Indice> repositoryIndice, IConvertIndice convertIndice)
        {
            this.repositoryCarteira = repositoryCarteira;
            this.repositoryIndice = repositoryIndice;
            this.convertIndice = convertIndice;
        }

        public IList<Indice> GetBenchmark(BenchmarkPesquisaDTO benchmarkDTO)
        {
            IList<Indice> listIndiceBenchmark = new List<Indice>();
            benchmarkDTO.Indices.ForEach(indice => listIndiceBenchmark.Add(GetCotizacaoAjustadaPorData(repositoryIndice.GetById(indice), benchmarkDTO.DataInicio, benchmarkDTO.DataFim)));
            benchmarkDTO.Carteiras.ForEach(carteira => listIndiceBenchmark.Add(GetCotizacaoAjustadaPorData(convertIndice.FromCarteira(repositoryCarteira.GetById(carteira)), benchmarkDTO.DataInicio, benchmarkDTO.DataFim)));
            return listIndiceBenchmark.Where(cotizacao => cotizacao != null).ToList();
        }

        private Indice GetCotizacaoAjustadaPorData(Indice indiceCompleto, DateTime dataInicio, DateTime dataFim)
        {
            var itensFiltered = indiceCompleto?.Itens?.Where(item => item.DataCota >= dataInicio && item.DataCota <= dataFim)?.ToList();
            if (itensFiltered?.Count > 0)
            {
                Indice indiceAjustado = new Indice(indiceCompleto.Codigo, indiceCompleto.Descricao, 1.0, itensFiltered.First().DataCota);
                itensFiltered.RemoveAt(0);
                itensFiltered.ForEach(item => indiceAjustado.AddItemByVariacaoCota(item.DataCota, item.VariacaoCotaPercentual));
                return indiceAjustado;
            }
            return null;
        }
    }
}
