using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SourceCodeBuilder.MyNamespaceBuilder;

namespace SourceCodeBuilder
{
    public class MyNamespaceBuilder
    {
        MyNamespace _myNamespace = new();
        internal string Tabs = string.Empty;
   
        public MyNamespaceBuilder(string fileName)
        {
            _myNamespace.FileName = fileName;
        }

        /// <summary>
        /// Add class
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyNamespace.AddClass(....).Build()).ToString();
        /// </code>
        /// result:
        /// <code>
        /// namespace MyNamespace
        /// {
        ///     public class Program
        ///     {
        ///         string Name;
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyNamespaceBuilder AddClass(MyClass? myClass)
        {
            if(myClass != null)
            {
                _myNamespace.AddClass(myClass);
            }
            return this;

        }

        /// <summary>
        /// Add class
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyNamespace.AddClass(......).ToString();
        /// </code>
        /// result:
        /// <code>
        /// namespace MyNamespace
        /// {
        ///     public class Program
        ///     {
        ///         string Name;
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyNamespaceBuilder AddClass(MyClassBuilder? myClassBuilder)
        {
            if (myClassBuilder != null)
            {
                _myNamespace.AddClass(myClassBuilder);
            }
            return this;

        }

        /// <summary>
        /// Add classes
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyNamespace.AddClass(......).ToString();
        /// </code>
        /// result:
        /// <code>
        /// namespace MyNamespace
        /// {
        ///     public class Program1
        ///     {
        ///         string Name1;
        ///     }
        ///     public class Program2
        ///     {
        ///         string Name2;
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyNamespaceBuilder AddClasses(IEnumerable<MyClassBuilder>? myClassBuilders)
        {
            foreach(var myClassBuilder in myClassBuilders ?? [])
            {
                if (myClassBuilder != null)
                {
                    _myNamespace.AddClass(myClassBuilder);
                }
            }
            return this;
        }

        /// <summary>
        /// Add interface
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyNamespace.AddInterface(....).Build()).ToString();
        /// </code>
        /// result:
        /// <code>
        /// namespace MyNamespace
        /// {
        ///     public interface Program
        ///     {
        ///         string Name;
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyNamespaceBuilder AddInterface(MyInterface? myInterface)
        {
            if(myInterface != null)
            {
                _myNamespace.AddInterface(myInterface);
            }
            return this;

        }

        /// <summary>
        /// Add interface
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyNamespace.AddInterface(......).ToString();
        /// </code>
        /// result:
        /// <code>
        /// namespace MyNamespace
        /// {
        ///     public interface Program
        ///     {
        ///         string Name;
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyNamespaceBuilder AddInterface(MyInterfaceBuilder? myInterfaceBuilder)
        {
            if (myInterfaceBuilder != null)
            {
                _myNamespace.AddInterface(myInterfaceBuilder);
            }
            return this;

        }

        /// <summary>
        /// Add interface
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyNamespace.AddInterface(......).ToString();
        /// </code>
        /// result:
        /// <code>
        /// namespace MyNamespace
        /// {
        ///     public interface Program
        ///     {
        ///         string Name;
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyNamespaceBuilder AddInterfaces(IEnumerable<MyInterfaceBuilder>? myInterfaceBuilders)
        {
            foreach (var myInterfaceBuilder in myInterfaceBuilders ?? [])
            {
                if (myInterfaceBuilder != null)
                {
                    _myNamespace.AddInterface(myInterfaceBuilder);
                }
            }
            return this;
        }

        /// <summary>
        /// Add class property
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyNamespace.AddUsing("System").ToString();
        /// </code>
        /// result:
        /// <code>
        /// using System;
        /// namespace MyNamespace
        /// {
        ///     public class Program
        ///     {
        ///         string Name;
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>

        public MyNamespaceBuilder AddUsing(string myUsing)
        {
            if (!string.IsNullOrEmpty(myUsing))
            {
                _myNamespace.AddUsing(myUsing);
            }
            return this;

        }

        public MyNamespaceBuilder WithName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                _myNamespace.NamespaceName = name;
            }
            return this;

        }

        /// <summary>
        /// Set file name. REQUIRED
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyNamespaceBuilder.FileName("Program.cs").ToString();
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static MyNamespaceBuilder FileName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("FileName");
            }
            return new MyNamespaceBuilder(name);
        }

        /// <summary>
        /// <example>
        /// <code>
        /// C# class code;
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _myNamespace?.ToString() ?? string.Empty;
        }

        public bool Save(Encoding? encoding = null)
        {
            MyNamespaceWriter myNamespaceWriter = new MyNamespaceWriter();
            using (StreamWriter fs = new StreamWriter(_myNamespace.FileName, false, encoding ?? Encoding.Default))
            {
                myNamespaceWriter.GenerateCode(_myNamespace, fs);
            }
            return true;
        }
    }
}
