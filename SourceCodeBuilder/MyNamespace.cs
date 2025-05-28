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
        public string? FileName { get; set; }
        public List<string> Usings { get; set; } = [];
        public List<MyInterface> InterfaceList { get; set; } = [];
        public List<MyClass> ClassList { get; set; } = [];

        public void AddInterface(MyInterface myInterface)
        {
            if (InterfaceList.Any(o => o.InterfaceName == myInterface.InterfaceName))
            {
                throw new ArgumentException($"Interface with name {myInterface.InterfaceName} already exists in namespace {NamespaceName}");
            }
            InterfaceList.Add(myInterface);
        }

        public void AddInterface(MyInterfaceBuilder myInterfaceBuilder)
        {
            AddInterface(myInterfaceBuilder.Build());
        }
        public void AddClass(MyClass myClass)
        {
            if(ClassList.Any(o=>o.ClassName == myClass.ClassName))
            {
                throw new ArgumentException($"Class with name {myClass.ClassName} already exists in namespace {NamespaceName}");
            }
            ClassList.Add(myClass);
        }

        public void AddClass(MyClassBuilder myClassBuilder)
        {
            AddClass(myClassBuilder.Build());
        }

        public void AddUsing(string myUsing)
        {
            Usings.Add(myUsing);
        }

        public override string? ToString()
        {
            return ToString(new MyNamespaceWriter());
        }

        public string? ToString(ICodeWriter<MyNamespace> formatter)
        {
            return formatter?.WriteCode(this);
        }
    }
}
