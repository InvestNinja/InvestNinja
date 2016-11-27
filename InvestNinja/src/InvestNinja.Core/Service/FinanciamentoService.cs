using System;
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
            double valorParcela = CalcularParcelaTabelaPrice(valorTotal, i, n);            double saldoDevedor = valorTotal;            IList<FinanciamentoParcelaDTO> listParcelas = new List<FinanciamentoParcelaDTO>();
            FinanciamentoParcelaDTO parcela0 = new FinanciamentoParcelaDTO()
            {
                NumeroParcela = 0,                ValorJuros = 0,                ValorAmortizacao = 0,                ValorSaldoDevedor = saldoDevedor
            };            listParcelas.Add(parcela0);
            for (int j = 1; j <= n; j++)            {                FinanciamentoParcelaDTO parcelaDTO = new FinanciamentoParcelaDTO();                parcelaDTO.NumeroParcela = j;                parcelaDTO.ValorJuros = saldoDevedor * i;                parcelaDTO.ValorAmortizacao = valorParcela - parcelaDTO.ValorJuros;                saldoDevedor = saldoDevedor - parcelaDTO.ValorAmortizacao;                parcelaDTO.ValorSaldoDevedor = saldoDevedor;                listParcelas.Add(parcelaDTO);            }
            return listParcelas;
        }

        public IList<FinanciamentoParcelaDTO> CalcularFinanciamentoTabelaSacDetalhado(double valorTotal, double i, int n)
        {
            double valorAmortizacao = valorTotal / n;            double saldoDevedor = valorTotal;            IList<FinanciamentoParcelaDTO> listParcelas = new List<FinanciamentoParcelaDTO>();            FinanciamentoParcelaDTO parcela0 = new FinanciamentoParcelaDTO()
            {
                NumeroParcela = 0,                ValorJuros = 0,                ValorAmortizacao = 0,                ValorSaldoDevedor = saldoDevedor
            };
            listParcelas.Add(parcela0);
            for (int j = 1; j <= n; j++)            {                FinanciamentoParcelaDTO parcelaDTO = new FinanciamentoParcelaDTO();                parcelaDTO.NumeroParcela = j;                parcelaDTO.ValorJuros = saldoDevedor * i;                parcelaDTO.ValorAmortizacao = valorAmortizacao;                parcelaDTO.ValorParcela = valorAmortizacao + parcelaDTO.ValorJuros;                saldoDevedor = saldoDevedor - parcelaDTO.ValorAmortizacao;                parcelaDTO.ValorSaldoDevedor = saldoDevedor;                listParcelas.Add(parcelaDTO);            }            return listParcelas;            //amortização constante: se divide o valor do emprestimo pelo numero de parcelas, e se tem o valor da amortização            //após isso, se calcula o juros de acordo com a taxa, todo mês em cima do devedor, sendo o valor dos juros somado            //ao valor da amortização, se obtém a parcela.
        }

        //os juros são calculados em cada mes, e a diferença para o valor da parcela é a amortização. O ciclo se repete, até o final
        public double CalcularParcelaTabelaPrice(double valorFinanciamento, double i, int n) => valorFinanciamento / ((Math.Pow(1 + i, n) - 1) / (i * Math.Pow(1 + i, n)));

        public double CalcularTotalJurosTabelaPrice(double valorFinanciamento, double i, int n)
        {
            double valorParcela = CalcularParcelaTabelaPrice(valorFinanciamento, i, n);            return (valorParcela * n) - valorFinanciamento;
        }

        public double CalcularTotalJurosTabelaSac(double valorFinanciamento, double i, int n)
        {
            double valorJuros = 0;            double saldoDevedor = valorFinanciamento;            for (int j = 1; j <= n; j++)            {                valorJuros += saldoDevedor * i;                saldoDevedor = saldoDevedor - (valorFinanciamento / n);            }            return valorJuros;
        }
    }
}
