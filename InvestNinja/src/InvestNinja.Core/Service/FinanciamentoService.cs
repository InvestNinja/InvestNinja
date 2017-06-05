﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestNinja.Core.DTO;

namespace InvestNinja.Core.Service
{
    public class FinanciamentoService : IFinanciamentoService
    {
        public IList<FinanciamentoParcelaDTO> CalcularFinanciamentoTabelaPriceDetalhado(double valorTotal, double i, int n)
        {
            double valorParcela = CalcularParcelaTabelaPrice(valorTotal, i, n);
            FinanciamentoParcelaDTO parcela0 = new FinanciamentoParcelaDTO()
            {
                NumeroParcela = 0,
            };
            for (int j = 1; j <= n; j++)
            return listParcelas;
        }

        public IList<FinanciamentoParcelaDTO> CalcularFinanciamentoTabelaSacDetalhado(double valorTotal, double i, int n)
        {
            double valorAmortizacao = valorTotal / n;
            {
                NumeroParcela = 0,
            };
            listParcelas.Add(parcela0);
            for (int j = 1; j <= n; j++)
        }

        //os juros são calculados em cada mes, e a diferença para o valor da parcela é a amortização. O ciclo se repete, até o final
        public double CalcularParcelaTabelaPrice(double valorFinanciamento, double i, int n) => valorFinanciamento * ((i * Math.Pow(1 + i, n)) / (Math.Pow(1 + i, n) - 1));

        public double CalcularTotalJurosTabelaPrice(double valorFinanciamento, double i, int n)
        {
            double valorParcela = CalcularParcelaTabelaPrice(valorFinanciamento, i, n);
        }

        public double CalcularTotalJurosTabelaSac(double valorFinanciamento, double i, int n)
        {
            throw new NotImplementedException();
        }
    }
}