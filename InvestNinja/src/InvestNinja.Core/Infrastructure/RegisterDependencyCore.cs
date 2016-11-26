using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using InvestNinja.Core.Service;

namespace InvestNinja.Core.Infrastructure
{
    public class RegisterDependencyCore : IRegisterDependency
    {
        public int Order => 1;

        public void Register(IServiceCollection collection)
        {
            collection.AddTransient<IFinancialService, FinancialService>();
            collection.AddTransient<IImpostoRendaService, ImpostoRendaService>();
        }
    }
}
