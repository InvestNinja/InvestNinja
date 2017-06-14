using System;
using System.Collections.Generic;

namespace InvestNinja.Core.DTO
{
    public class BenchmarkPesquisaDTO
    {
        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public IList<string> Indices { get; set; }

        public IList<string> Carteiras { get; set; }

        public string CodigoBenchmark { get; set; }
    }
}