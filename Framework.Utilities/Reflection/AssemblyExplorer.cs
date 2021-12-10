using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.Utilities.Reflection
{
    public static class AssemblyExplorer {
        private static Assembly GetAssemblyForName(string name)
            => Assembly.Load(new AssemblyName(name));

        public static List<Type> GetClassesInAssemblyNameContains(string assamblyName, string toFind)
        {
            var kernelData = GetAssemblyForName(assamblyName);
            return kernelData
                .GetTypes()
                .Where(t => t.GetTypeInfo().IsClass && t.Name.ToLower().Contains(toFind.ToLower()))
                .ToList();
        }

        public static List<Type> GetClassesInAssemblyNameNotContains(string assamblyName, string toFind)
        {
            var kernelData = GetAssemblyForName(assamblyName);
            return kernelData
                .GetTypes()
                .Where(t => t.GetTypeInfo().IsClass && !t.Name.ToLower().Contains(toFind.ToLower()))
                .ToList();
        }

        public static List<Type> GetClassesInAssemblyDescendantsOfType(string assamblyName, Type parent)
        {
            var kernelData = GetAssemblyForName(assamblyName);
            return kernelData
                .GetTypes()
                .Where(t => t.GetTypeInfo().IsClass && parent.IsAssignableFrom(t) && (parent != t))
                .ToList();
        }

        public static List<Type> GetInterfacesInAssemblyDescendantsOfType(string assamblyName, Type parent)
        {
            var kernelData = GetAssemblyForName(assamblyName);
            return kernelData
                .GetTypes()
                .Where(t => t.GetTypeInfo().IsInterface && parent.IsAssignableFrom(t) && (parent != t))
                .ToList();
        }

        public static List<Type> GetClassesInAssemblyAncestorsOfType(string assamblyName, Type child)
        {
            var kernelData = GetAssemblyForName(assamblyName);
            return kernelData
                .GetTypes()
                .Where(t => t.GetTypeInfo().IsClass && t.IsAssignableFrom(child) && (child != t))
                .ToList();
        }

        public static List<string> GetAssemblyNamesForSolution()
            => AppDomain.CurrentDomain.GetAssemblies().Select(a => a.GetName().Name).ToList();
    }
}
