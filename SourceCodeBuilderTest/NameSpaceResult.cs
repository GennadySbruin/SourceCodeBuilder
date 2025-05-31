
using System;
using System.Text;

namespace MyNamespace.Library
{
    public partial interface IMyClass
    {
    }
    public partial class MyClass : IMyClass
    {
        public int Count;
        public static int[] IntArr => Method1();
        static int[] Method1()
        {
            return [1];
        }
    }
}