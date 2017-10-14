using System;
using System.Collections.Generic;
using System.Reflection;
using NSubstitute;

namespace MyNUnit.Tests
{
    public class Utils
    {
        public static IEnumerable<MethodBase> GetTestMethods(TestAttributes testAttributes)
        {
            var beforeClassMethod = Substitute.For<MethodBase>();
            beforeClassMethod.GetCustomAttributes(Arg.Any<bool>())
                             .Returns(new Object[] { Activator.CreateInstance(testAttributes.BeforeClassAttribute) });

            var beforeMethod = Substitute.For<MethodBase>();
            beforeMethod.GetCustomAttributes(Arg.Any<bool>())
                        .Returns(new Object[] { Activator.CreateInstance(testAttributes.BeforeAttribute) });

            //var testMethod = Substitute.For<MethodBase>();
            //testMethod.GetCustomAttributes(Arg.Any<bool>())
            //.Returns(new Object[] { Activator.CreateInstance(testAttributes.TestAttribute) });

            var afterMethod = Substitute.For<MethodBase>();
            afterMethod.GetCustomAttributes(Arg.Any<bool>())
                       .Returns(new Object[] { Activator.CreateInstance(testAttributes.AfterAttribute) });

            var afterClassMethod = Substitute.For<MethodBase>();
            afterClassMethod.GetCustomAttributes(Arg.Any<bool>())
                            .Returns(new Object[] { Activator.CreateInstance(testAttributes.AfterClassAttribute) });

            return new MethodBase[] {
                beforeClassMethod,
                beforeMethod,
                afterMethod,
                afterClassMethod };
        }
    }
}
