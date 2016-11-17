using InvestNinja.Core.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Core.Service
{
    public interface IFinancialService
    {
        double CalcularMontante(double pv, double i, int n);

        double CalcularMontanteMensal(double pv, double mv, double i, int n, TipoMovimentacao tipoMovimentacao);

        double CalcularMontanteMensalInflacao(double pv, double mv, double i, int n, double inflacao, TipoMovimentacao tipoMovimentacao);

        double CalcularMontanteMensalTaxaAdm(double pv, double mv, double i, int n, double taxaAdm, TipoMovimentacao tipoMovimentacao);

        double CalcularMontanteMensalInflacaoTaxaAdm(double pv, double mv, double i, int n, double inflacao, double taxaAdm, TipoMovimentacao tipoMovimentacao);

        double CalcularMontanteMensalCompleto(double pv, double mv, double i, int n, double inflacao, double taxaAdm, double taxaCarregamento, double impostoRenda, TipoMovimentacao tipoMovimentacao);

        double CalcularMontanteMensalCompleto(double pv, double mv, double i, int n, double inflacao, double taxaAdm, double taxaCarregamento, double impostoRenda, TipoMovimentacao tipoMovimentacao, bool comeCotas);

        double CalcularTotalJurosSimples(double pv, double i, int n);

        double CalcularValorPresente(double i, int n, double j);

        double CalcularValorFuturo(double pv, double i, int n);

        double CalcularTempo(double pv, double i, double j);

        double CalcularTaxaJuros(double pv, int n, double j);

        double CalcularValorPresenteEmMontante(double fv, double i, int n);

        double CalcularTempoEmValorFuturo(double fv, double pv, double i);

        double CalcularTaxaEmValorFuturo(double fv, double pv, int n);
    }
}
