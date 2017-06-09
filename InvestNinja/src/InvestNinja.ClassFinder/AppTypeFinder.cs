using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace InvestNinja.ClassFinder
{
    public class AppTypeFinder : ITypeFinder
    {
        private bool ignoreReflectionErrors = true;
        private IList<IAssemblyLoader> listLoader;

        public string RestrictToPattern { get; set; }
        public string SkipPattern { get; set; }

        public AppTypeFinder(IList<IAssemblyLoader> loaders)
        {
            this.listLoader = loaders;
        }

        public IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true) => FindClassesOfType(typeof(T), onlyConcreteClasses);

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true) => FindClassesOfType(assignTypeFrom, GetAssemblies(), onlyConcreteClasses);

        public IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true) => FindClassesOfType(typeof(T), assemblies, onlyConcreteClasses);

        public IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true)
        {
            var result = new List<Type>();
            try
            {
                foreach (var a in assemblies)
                {
                    Type[] types = null;
                    try
                    {
                        types = a.GetTypes();
                    }
                    catch
                    {
                        if (!ignoreReflectionErrors)
                        {
                            throw;
                        }
                    }
                    if (types != null)
                    {
                        foreach (var t in types)
                        {
                            if (assignTypeFrom.IsAssignableFrom(t) || (assignTypeFrom.GetTypeInfo().IsGenericTypeDefinition && DoesTypeImplementOpenGeneric(t, assignTypeFrom)))
                            {
                                if (!t.GetTypeInfo().IsInterface)
                                {
                                    if (onlyConcreteClasses)
                                    {
                                        if (t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract)
                                        {
                                            result.Add(t);
                                        }
                                    }
                                    else
                                    {
                                        result.Add(t);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                var msg = string.Empty;
                foreach (var e in ex.LoaderExceptions)
                    msg += e.Message + Environment.NewLine;

                var fail = new Exception(msg, ex);
                Debug.WriteLine(fail.Message, fail);

                throw fail;
            }
            return result;
        }

        private IList<Assembly> GetAssemblies()
        {
            var addedAssemblyNames = new List<string>();
            var assemblies = new List<Assembly>();

            foreach (IAssemblyLoader loader in listLoader)
                AddAssemblies(addedAssemblyNames, assemblies, loader);

            return assemblies;
        }

        private void AddAssemblies(List<string> addedAssemblyNames, List<Assembly> assemblies, IAssemblyLoader loader)
        {
            StringMatch match = new StringMatch(RestrictToPattern, SkipPattern);
            foreach (Assembly assembly in loader.GetAssemblies())
            {
                if (match.Matches(assembly.FullName))
                {
                    if (!addedAssemblyNames.Contains(assembly.FullName))
                    {
                        assemblies.Add(assembly);
                        addedAssemblyNames.Add(assembly.FullName);
                    }
                }
            }
        }

        private bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
                foreach (var implementedInterface in type.GetTypeInfo().FindInterfaces((objType, objCriteria) => true, null))
                {
                    if (!implementedInterface.GetTypeInfo().IsGenericType)
                        continue;

                    return genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition());
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
