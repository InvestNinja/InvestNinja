using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvestNinja.Core.Infrastructure
{
    public interface IRegisterDependency
    {
        void Register(IServiceCollection collection);

        int Order { get; }
    }
}
