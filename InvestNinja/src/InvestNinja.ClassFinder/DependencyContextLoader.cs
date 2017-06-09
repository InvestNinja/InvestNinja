using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace InvestNinja.ClassFinder
{
    public class DependencyContextLoader : IAssemblyLoader
    {
        public IList<Assembly> GetAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;
            foreach (var library in dependencies)
            {
                try
                {
                    var assembly = Assembly.Load(new AssemblyName(library.Name));
                    assemblies.Add(assembly);
                }
                catch
                {
                    //não dar erro se não conseguir carregar o assembly
                }
            }
            return assemblies.ToArray();
        }
    }
}
