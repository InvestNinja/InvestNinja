using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyModel;
using System.IO;

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
