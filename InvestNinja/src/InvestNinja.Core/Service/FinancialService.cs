using InvestNinja.Core.Tipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Core.Service
{
    public class FinancialService : IFinancialService
    {
        public double CalcularMontante(double pv, double i, int n) => pv * Math.Pow(1 + (i / 100), n);

        public double CalcularMontanteMensal(double pv, double mv, double i, int n, TipoMovimentacao tipoMovimentacao)
        {
            double aux = pv;
            for (int a = 0; a < n; a++)
            {
                if (tipoMovimentacao == TipoMovimentacao.Aplicacao)
                    aux = CalcularMontante(aux + mv, i, 1);
                else
                    aux = CalcularMontante(aux, i, 1) - mv;
            }
            return aux;
        }

        public double CalcularMontanteMensalCompleto(double pv, double mv, double i, int n, double inflacao, double taxaAdm, double taxaCarregamento, double impostoRenda, TipoMovimentacao tipoMovimentacao)
        {
            return CalcularMontanteMensalCompleto(pv, mv, i, n, inflacao, taxaAdm, taxaCarregamento, impostoRenda, tipoMovimentacao, false);
        }

        public double CalcularMontanteMensalCompleto(double pv, double mv, double i, int n, double inflacao, double taxaAdm, double taxaCarregamento, double impostoRenda, TipoMovimentacao tipoMovimentacao, bool comeCotas)
        {
            double aux = pv;
            double rendimentoTotal = 0.0;
            double rendimentoParcial = 0.0;
            double oldAux = aux;
            mv = mv - (mv * taxaCarregamento);
            for (int a = 0; a < n; a++)
            {
                aux = aux - (aux * taxaAdm);
                if (tipoMovimentacao == TipoMovimentacao.Aplicacao)
                {
                    aux = CalcularMontante(aux + mv, i - inflacao, 1);
                    rendimentoTotal = rendimentoTotal + (aux - oldAux - mv);
                    rendimentoParcial = rendimentoParcial + (aux - oldAux - mv);
                    if ((comeCotas) && (a % 6 == 0))
                    {
                        aux = aux - (rendimentoParcial * 0.15);
                        rendimentoParcial = 0.0;
                    }
                    oldAux = aux;
                }
                else
                {
                    aux = CalcularMontante(aux, i - inflacao, 1) - mv;
                    rendimentoTotal = rendimentoTotal + (aux - oldAux);
                    rendimentoParcial = rendimentoParcial + (aux - oldAux);
                    if ((comeCotas) && (a % 6 == 0))
                    {
                        aux = aux - (rendimentoParcial * 0.15);
                        rendimentoParcial = 0.0;
                    }
                    oldAux = aux;
                }
            }
            if (comeCotas)
            {
                aux = aux - (rendimentoParcial * 0.15);
                aux = aux - (rendimentoTotal * (impostoRenda - 0.15));
            }
            else
            {
                aux = aux - (rendimentoTotal * impostoRenda);
            }
            return aux;
        }

        public double CalcularMontanteMensalInflacao(double pv, double mv, double i, int n, double inflacao, TipoMovimentacao tipoMovimentacao)
        {
            return CalcularMontanteMensalCompleto(pv, mv, i, n, inflacao, 0.0, 0.0, 0.0, tipoMovimentacao, false);
        }

        public double CalcularMontanteMensalInflacaoTaxaAdm(double pv, double mv, double i, int n, double inflacao, double taxaAdm, TipoMovimentacao tipoMovimentacao)
        {
            return CalcularMontanteMensalCompleto(pv, mv, i, n, inflacao, taxaAdm, 0.0, 0.0, tipoMovimentacao, false);
        }

        public double CalcularMontanteMensalTaxaAdm(double pv, double mv, double i, int n, double taxaAdm, TipoMovimentacao tipoMovimentacao)
        {
            return CalcularMontanteMensalCompleto(pv, mv, i, n, 0.0, taxaAdm, 0.0, 0.0, tipoMovimentacao, false);
        }

        public double CalcularTaxaEmValorFuturo(double fv, double pv, int n) => ((fv / pv) - 1) / n;

        public double CalcularTaxaJuros(double pv, int n, double j) => j / (pv* n);

        public double CalcularTempo(double pv, double i, double j) => j / (pv * (i / 100));

        public double CalcularTempoEmValorFuturo(double fv, double pv, double i) => ((fv / pv) - 1) / (i / 100);

        public double CalcularTotalJurosSimples(double pv, double i, int n) => pv * (i / 100) * n;

        public double CalcularValorFuturo(double pv, double i, int n) => pv * (1 + (i / 100) * n);

        public double CalcularValorPresente(double i, int n, double j) => j / ((i / 100) * n);

        public double CalcularValorPresenteEmMontante(double fv, double i, int n) => fv / ((i / 100) * n);
    }
}
