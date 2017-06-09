using System.Collections.Generic;
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
            Assert.Equal(1, loader.GetAssemblies().Where(assembly => assembly.ManifestModule.Name == "InvestNinja.ClassFinder.dll").Count());
        }

        [Fact]
        public void TestStringMatch() => Assert.True(new StringMatch("Teste", "").Matches("Teste"));

        [Fact]
        public void TestStringDoesntMatch() => Assert.False(new StringMatch("Restrict", "").Matches("Teste"));

        [Fact]
        public void TestStringSkipMatch() => Assert.False(new StringMatch("", "Teste").Matches("Teste"));

        [Fact]
        public void TestAppTypeFinder()
        {
            AppTypeFinder typeFinder = new AppTypeFinder(new List<IAssemblyLoader>() { new DependencyContextLoader() });
            var types = typeFinder.FindClassesOfType<IAssemblyLoader>();
            Assert.True(types.Where(type => type == typeof(DependencyContextLoader)).Count() > 0);
        }
    }
}
