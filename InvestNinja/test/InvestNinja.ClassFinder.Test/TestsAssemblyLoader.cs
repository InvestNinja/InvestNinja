using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace InvestNinja.ClassFinder.Test
{
    [TestClass]
    public class TestsAssemblyLoader
    {
        public TestsAssemblyLoader()
        {
            
        }


        [TestMethod]
        public void TestGetAssemblies()
        {
            IAssemblyLoader loader = new DependencyContextLoader();
            Assert.IsTrue(loader.GetAssemblies().Where(a => a.FullName.Contains("InvestNinja")).Count() > 0);
        }
    }
}
