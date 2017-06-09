using System;
using System.Linq;
using Xunit;

namespace InvestNinja.ClassFinder.Test
{
    public class TestsAssemblyLoader
    {
        [Fact]
        public void TestGetAssemblies()
        {
            IAssemblyLoader loader = new DependencyContextLoader();
            Assert.True(loader.GetAssemblies().Where(a => a.FullName.Contains("InvestNinja")).Count() > 0);
        }
    }
}
