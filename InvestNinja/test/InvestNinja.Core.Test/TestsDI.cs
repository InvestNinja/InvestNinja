using InvestNinja.ClassFinder;
using InvestNinja.Core.Infrastructure;
using InvestNinja.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace InvestNinja.Core.Test
{
    public class TestsDI
    {
        [Fact]
        public void TestLoader()
        {
            var bml = new DependencyContextLoader();
            var ls = bml.GetAssemblies();
            foreach(var l in ls)
            {
                System.Console.Out.WriteLine(l.FullName);
            }
        }

        [Fact]
        public void TestDI()
        {
            var x = ContainerRegisterAll.RegisterDependenciesReferenced();
            var s = x.GetService<IFinancialService>();
            Assert.Equal(1061.52, s.CalcularMontante(1000.00, 1.0, 6), 2);
        }
    }
}
