using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Core.Service
{
    public interface IImpostoRendaService
    {
        double CalcularAliquotaIRTabelaProgressiva(int qtdMeses);

        double CalcularAliquotaIRTabelaRegressiva(int qtdDias, bool curtoPrazo);

        double CalcularAliquotaIRTabelaRegressivaPrevidencia(double qtdAnos);
    }
}
