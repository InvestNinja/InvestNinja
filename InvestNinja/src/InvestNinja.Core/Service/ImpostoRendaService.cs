﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Core.Service
{
    public class ImpostoRendaService : IImpostoRendaService
    {
        public double CalcularAliquotaIRTabelaProgressiva(int qtdMeses)
        {
            throw new NotImplementedException();
        }

        public double CalcularAliquotaIRTabelaRegressiva(int qtdDias, bool curtoPrazo)
        {
            if (qtdDias <= 180)
        }

        public double CalcularAliquotaIRTabelaRegressivaPrevidencia(double qtdAnos)
        {
            if (qtdAnos <= 2)
        }
    }
}