using System;
using System.Collections.Generic;

namespace MyNUnit
{
    public interface IMethod
    {
        void Invoke(Object obj, Object[] args);
    }
}
