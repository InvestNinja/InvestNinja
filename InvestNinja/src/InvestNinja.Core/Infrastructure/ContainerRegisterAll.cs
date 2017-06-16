using InvestNinja.ClassFinder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvestNinja.Core.Infrastructure
{
    public class ContainerRegisterAll
    {
        public static IServiceProvider RegisterDependencies(IList<IAssemblyLoader> loaders)
        {
            IServiceCollection collection = new ServiceCollection();
            AppTypeFinder typeFinder = new AppTypeFinder(loaders);
            var drTypes = typeFinder.FindClassesOfType<IRegisterDependency>();
            var drInstances = new List<IRegisterDependency>();
            foreach (var drType in drTypes)
                drInstances.Add((IRegisterDependency)Activator.CreateInstance(drType));

            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(collection);

            return collection.BuildServiceProvider();
        }

        public static IServiceProvider RegisterDependenciesReferenced() => RegisterDependencies(new List<IAssemblyLoader>() { new DependencyContextLoader() });
    }
}
