using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public class MyNamespace
    {
        public string? NamespaceName { get; set; }

        public List<MyClass> ClassList { get; set; } = [];
        public void AddClass(MyClass myClass)
        {
            if(ClassList.Any(o=>o.ClassName == myClass.ClassName))
            {
                throw new ArgumentException($"Class with name {myClass.ClassName} already exists in namespace {NamespaceName}");
            }

            if (myClass.NameSpace != null)
            {
                throw new ArgumentException($"Class with name {myClass.ClassName} already contains in namespace {myClass.NameSpace.NamespaceName}");
            }

            ClassList.Add(myClass);
            myClass.NameSpace = this;
        }

        public void AddClass(MyClassBuilder myClassBuilder)
        {
            AddClass(myClassBuilder.Build());
        }
    }
}
