using InvestNinja.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Core.Service
{
    public interface IFinanciamentoService
    {
        double CalcularParcelaTabelaPrice(double valorFinanciamento, double i, int n);

        double CalcularTotalJurosTabelaPrice(double valorFinanciamento, double i, int n);

        double CalcularTotalJurosTabelaSac(double valorFinanciamento, double i, int n);

        IList<FinanciamentoParcelaDTO> CalcularFinanciamentoTabelaPriceDetalhado(double valorTotal, double i, int n);

        IList<FinanciamentoParcelaDTO> CalcularFinanciamentoTabelaSacDetalhado(double valorTotal, double i, int n);
    }
}
