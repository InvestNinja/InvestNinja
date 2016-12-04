using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Core.Service
{
    public interface IBolsaValoresService
    {
        double CalcularCorretagemTabelaBovespa(double valorOperacao);

        double CalcularEmolumentos(double valorOperacao, bool isDayTrade);

        double CalcularLiquidacao(double valorOperacao, bool isDayTrade);

        double CalcularTaxas(double valorOperacao, bool isDayTrade);

        double CalcularPrecoMinimoVendaEmpate(double valorCompra, int qtdAtivos, double valorTaxa);

        double CalcularValorizacaoVendaDesejada(double valorCompra, double valorAlvo, int qtdAtivos);

        double CalcularValorizacaoVendaDesejada(double valorCompra, double valorAlvo);
    }
}
