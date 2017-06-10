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
            if (qtdDias <= 180)
                return 22.5;
            if ((qtdDias <= 360) || (curtoPrazo))
                return 20.0;
            if (qtdDias <= 720)
                return 17.5;
            return 15.0; //qtdDias > 720
        }

        public double CalcularAliquotaIRTabelaRegressivaPrevidencia(double qtdAnos)
        {
            if (qtdAnos <= 2)
                return 35.0;
            if (qtdAnos <= 4)
                return 30.0;
            if (qtdAnos <= 6)
                return 25.0;
            if (qtdAnos <= 8)
                return 20.0;
            if (qtdAnos <= 10)
                return 15.0;
            return 10.0; //qtdAnos > 10
        }
    }
}
