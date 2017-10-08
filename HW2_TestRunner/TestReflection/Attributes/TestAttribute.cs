﻿using System;

namespace TestReflection.Attributes
{
    public class TestAttribute : Attribute
    {
        public Type ExpectedEceptionType { get;  }
        public String IgnoreReason { get;  }

        public TestAttribute(Type expectedEceptionType = null, String ignoreReason = null)
        {
            ExpectedEceptionType = expectedEceptionType;
            IgnoreReason = ignoreReason;
        }
    }
}