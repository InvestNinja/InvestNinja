using Microsoft.Extensions.DependencyInjection;
using InvestNinja.Core.Service;
using InvestNinja.Core.Converter;

namespace InvestNinja.Core.Infrastructure
{
    public class RegisterDependencyCore : IRegisterDependency
    {
        public int Order => 1;

        public void Register(IServiceCollection collection)
        {
            collection.AddTransient<IFinancialService, FinancialService>();
            collection.AddTransient<IImpostoRendaService, ImpostoRendaService>();
            collection.AddTransient<IFinanciamentoService, FinanciamentoService>();
            collection.AddTransient<IBolsaValoresService, BolsaValoresService>();
            collection.AddTransient<IBenchmarkService, BenchmarkService>();
            collection.AddTransient<IConvertIndice, ConvertIndice>();
        }
    }
}
