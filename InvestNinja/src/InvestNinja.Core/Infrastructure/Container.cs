using Microsoft.Extensions.DependencyInjection;
using System;

namespace InvestNinja.Core.Infrastructure
{
    public static class Container
    {
        private static IServiceProvider _serviceProvider;

        public static IServiceProvider ServiceProvider
        {
            get
            {
                if (_serviceProvider == null)
                    throw new Exception("Container não foi inicializado!");

                return _serviceProvider;
            }
        }

        public static void Initialize(IServiceCollection services)
        {
            if (_serviceProvider == null)
                _serviceProvider = services.BuildServiceProvider();
        }
    }
}
