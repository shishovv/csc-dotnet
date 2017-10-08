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
            GetTestClassesFrom(Assembly assembly, Type testAttribute)
            => assembly.GetTypes()
                .Where(type => IsTestClass(type, testAttribute));

        private static bool IsTestClass(Type type, Type testAttribute) =>
            type.IsClass && HasTestMethod(type.GetMethods(), testAttribute);

        private static bool HasTestMethod(IEnumerable<MethodInfo> methods, Type testAttribute) =>
            methods.Any(method =>
                method.GetCustomAttributes()
                    .Select(attribute => attribute.GetType())
                    .Any(attributeType => attributeType == testAttribute));
        
        public static IEnumerable<Assembly> GetAssembliesFrom(String path) =>
            Directory.EnumerateFiles(path)
                .Where(file => file.EndsWith(".exe") || file.EndsWith(".dll"))
                .Select(Assembly.LoadFrom);
    }
}