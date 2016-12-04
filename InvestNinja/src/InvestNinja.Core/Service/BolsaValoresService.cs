using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Core.Service
{
    public class BolsaValoresService : IBolsaValoresService
    {
        public double CalcularCorretagemTabelaBovespa(double valorOperacao)
        {
            double porcentCorretagem = 0.0;
            double fixoCorretagem = 0.0;
            if (valorOperacao <= 135.05)
            {
                fixoCorretagem = 2.70;
                return fixoCorretagem;
            }
            else if (valorOperacao <= 498.61)
            {
                porcentCorretagem = 2.0;
                fixoCorretagem = 0.0;
            }
            else if (valorOperacao <= 1514.68)
            {
                porcentCorretagem = 1.5;
                fixoCorretagem = 2.49;
            }
            else if (valorOperacao <= 3029.38)
            {
                porcentCorretagem = 1.0;
                fixoCorretagem = 10.06;
            }
            else
            {
                porcentCorretagem = 0.5;
                fixoCorretagem = 25.21;
            }
            return valorOperacao * (porcentCorretagem / 100) + fixoCorretagem;
        }

        public double CalcularEmolumentos(double valorOperacao, bool isDayTrade)
        {
            double porcentEmolumentos = isDayTrade ? 0.019 : 0.027;
            return valorOperacao * (porcentEmolumentos / 100);
        }

        public double CalcularLiquidacao(double valorOperacao, bool isDayTrade)
        {
            double porcentLiquidacao = isDayTrade ? 0.008 : 0.006;
            return valorOperacao * (porcentLiquidacao / 100);
        }

        public double CalcularPrecoMinimoVendaEmpate(double valorCompra, int qtdAtivos, double valorTaxa) => (valorCompra * qtdAtivos + valorTaxa) / qtdAtivos;
        
        public double CalcularTaxas(double valorOperacao, bool isDayTrade) => CalcularLiquidacao(valorOperacao, isDayTrade) + CalcularEmolumentos(valorOperacao, isDayTrade);

        public double CalcularValorizacaoVendaDesejada(double valorCompra, double valorAlvo) => ((valorAlvo / valorCompra) - 1) * 100;

        public double CalcularValorizacaoVendaDesejada(double valorCompra, double valorAlvo, int qtdAtivos)
        {
            throw new NotImplementedException();
        }
    }
}
