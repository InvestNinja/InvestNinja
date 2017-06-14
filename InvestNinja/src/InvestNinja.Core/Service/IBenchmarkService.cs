using InvestNinja.Core.Domain;
using InvestNinja.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvestNinja.Core.Service
{
    public interface IBenchmarkService
    {
        IList<Indice> GetBenchmark(BenchmarkPesquisaDTO benchmarkDTO);
    }
}
