using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace InvestNinja.ClassFinder
{
    public interface IAssemblyLoader
    {
        IList<Assembly> GetAssemblies();
    }
}
