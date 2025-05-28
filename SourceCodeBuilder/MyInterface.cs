using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public class MyInterface
    {
        public string? InterfaceName { get; set; }
        public List<string>? BaseInterfaceList { get; set; }
        public Dictionary<string, string>? GenericConstraintDictionary { get; set; }
        public List<string>? GenericList { get; set; }
        public List<MyProperty>? Properties { get; set; }
        public List<MyMethod>? Methods { get; set; }
        public List<AccessModifiers?> AccessModifiersList { get; set; } = [];
        public enum AccessModifiers
        {
            Internal,
            Public,
            Partial
        }


        /// <summary>
        /// Interface with internal access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterface.Internal.Name("IProgram").ToString();
        /// </code>
        /// result:
        /// <para><c>internal interface IProgram { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyInterfaceBuilder Internal => MyInterfaceBuilder.Internal;

        /// <summary>
        /// Interface with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterface.Public.Name("IProgram").ToString();
        /// </code>
        /// result:
        /// <para><c>public interface IProgram { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyInterfaceBuilder Public => MyInterfaceBuilder.Public;




        /// <summary>
        /// Interface with public access
        /// <example>
        /// <para>Example 1:</para>
        /// <code>
        /// MyInterface.PublicInterface("IProgram").ToString();
        /// </code>
        /// result:
        /// <para><c>public interface IProgram { }</c></para>
        /// </example>
        /// <example>
        /// <para>Example 2:</para>
        /// <code>
        /// MyInterface.PublicInterface("IProgram", "IIProgram", "IBase").ToString();
        /// </code>
        /// result:
        /// <para><c>public interface IProgram : IIProgram, IBase { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyInterfaceBuilder PublicInterface(string interfaceName, params string[] baseNames)
        {
            if (baseNames == null)
            {
                return MyInterfaceBuilder.Public.Name(interfaceName);
            }
            else
            {
                return MyInterfaceBuilder.Public.Name(interfaceName).BaseTypes(baseNames);
            }
        }

        /// <summary>
        /// Interface with internal access
        /// <example>
        /// <para>Example 1:</para>
        /// <code>
        /// MyInterface.InternalInterface("IProgram").ToString();
        /// </code>
        /// result:
        /// <para><c>internal interface IProgram { }</c></para>
        /// </example>
        /// <example>
        /// <para>Example 2:</para>
        /// <code>
        /// MyInterface.InternalInterface("IProgram", "IIProgram", "IBase").ToString();
        /// </code>
        /// result:
        /// <para><c>internal interface IProgram : IIProgram, IBase { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyInterfaceBuilder InternalInterface(string interfaceName, params string[] baseNames)
        {
            if (baseNames == null)
            {
                return MyInterfaceBuilder.Internal.Name(interfaceName);
            }
            else
            {
                return MyInterfaceBuilder.Internal.Name(interfaceName).BaseTypes(baseNames);
            }
        }

        public void AddProperty(MyProperty property)
        {
            if (Properties == null)
            {
                Properties = [];
            }
            Properties.Add(property);
        }

        public void AddMethod(MyMethod method)
        {
            if (Methods == null)
            {
                Methods = [];
            }
            Methods.Add(method);
        }

        public override string? ToString()
        {
            return ToString(new MyInterfaceWriter());
        }

        public string? ToString(ICodeWriter<MyInterface> formatter)
        {
            return formatter?.WriteCode(this);
        }
    }
}
