using System;
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
            if (qtdDias <= 180)                return 22.5;            else if ((qtdDias <= 360) || (curtoPrazo))                return 20.0;            else if (qtdDias <= 720)                return 17.5;            else //qtdDias > 720                return 15.0;
        }

        public double CalcularAliquotaIRTabelaRegressivaPrevidencia(double qtdAnos)
        {
            if (qtdAnos <= 2)                return 35.0;            else if (qtdAnos <= 4)                return 30.0;            else if (qtdAnos <= 6)                return 25.0;            else if (qtdAnos <= 8)                return 20.0;            else if (qtdAnos <= 10)                return 15.0;            else //qtdAnos > 10                return 10.0;
        }
    }
}
