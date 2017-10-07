using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TestReflection
{
    public static class Utils
    {
        public static IEnumerable<Type>
            GetTestClassesFrom(Assembly assembly, TestAttributes testAttributes)
            => assembly.GetTypes()
                .Where(type => type.IsClass 
                               && type.GetMethods()
                                   .Any(method => 
                                       method.GetCustomAttributes().Any(
                                           attr => testAttributes.Contains(attr.GetType()))));

        public static IEnumerable<Assembly> GetAssembliesFrom(String path) =>
            Directory.EnumerateFiles(path, "*.exe", SearchOption.AllDirectories)
                .Select(Assembly.LoadFrom);
    }
}