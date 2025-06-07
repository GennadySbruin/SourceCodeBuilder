namespace SourceCodeBuilder
{
    /// <summary>
    /// <para>AccessModifiersList ClassName : BaseClassList</para>
    /// <para>{</para>
    /// <para>    Members</para>
    /// <para>}</para>    
    /// </summary>
    public class MyClass
    {
        public string? ClassName { get; set; }
        public List<string>? BaseClassList { get; set; }
        public Dictionary<string, string>? GenericConstraintDictionary { get; set; }
        public List<string>? GenericList { get; set; }
        public string GenericWhere { get; set; }
        public List<MyField>? Fields { get; set; }
        public List<MyProperty>? Properties { get; set; }
        public List<MyMethod>? Methods { get; set; }
        public List<MyConstructor>? Constructors { get; set; }
        public List<string> Attributes { get; set; } = [];
        public List<MyClass>? Classes { get; set; }
        public List<string> Comments { get; set; } = [];
        public List<AccessModifiers?> AccessModifiersList { get; set; } = [];
        public enum AccessModifiers
        {
            Abstract,
            Static,
            Private,
            Internal,
            Public,
            Sealed,
            Protected,
            Partial
        }

        /// <summary>
        /// Class with private access. Use only for nested types
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClass.PrivateNested.Name("Program").Build().ToString();
        /// </code>
        /// result:
        /// <code>
        /// private class Program { }
        /// </code>
        /// </example>
        /// <para>Remark: Use only as nested type. Example:</para>
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// myClass = MyClass.PrivateNested.Name("Program").Build();
        /// parentClass = myClass.Name("AnyParentClassForExample");
        /// parentClass.AddNestedClass(myClass);
        /// parentClass.ToString();
        /// </code>
        /// result:
        /// <code>
        /// class AnyParentClassForExample
        /// {
        ///    private class Program { }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyClassBuilder PrivateNested => MyClassBuilder.PrivateNested;

        /// <summary>
        /// Class with protected modifier. Use only for nested types
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.ProtectedNested.Name("Program").Build().ToString();
        /// </code>
        /// result:
        /// <code>
        /// protected class Program { }
        /// </code>
        /// </example>
        /// <para>Remark: Use only as nested type. Example:</para>
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// myClass = MyClassBuilder.ProtectedNested.Name("Program");
        /// parentClass = MyClassBuilder.Name("AnyParentClassForExample");
        /// parentClass.AddNestedClass(myClass);
        /// parentClass.ToString();
        /// </code>
        /// result:
        /// <code>
        /// class AnyParentClassForExample
        /// {
        ///    protected class Program { }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyClassBuilder ProtectedNested => MyClassBuilder.ProtectedNested;


        /// <summary>
        /// Class with internal access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClass.Internal.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>internal class Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyClassBuilder Internal => MyClassBuilder.Internal;
        
        /// <summary>
        /// Class with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClass.Public.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>public class Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyClassBuilder Public => MyClassBuilder.Public;




        /// <summary>
        /// Class with public access
        /// <example>
        /// <para>Example 1:</para>
        /// <code>
        /// MyClass.PublicClass("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>public class Program { }</c></para>
        /// </example>
        /// <example>
        /// <para>Example 2:</para>
        /// <code>
        /// MyClass.PublicClass("Program", "IProgram", "IBase").ToString();
        /// </code>
        /// result:
        /// <para><c>public class Program : IProgram, IBase { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyClassBuilder PublicClass(string className, params string[] baseNames)
        {
            if (baseNames == null)
            {
                return MyClassBuilder.Public.Name(className);
            }
            else
            {
                return MyClassBuilder.Public.Name(className).BaseTypes(baseNames);
            }
        }

        /// <summary>
        /// Class with internal access
        /// <example>
        /// <para>Example 1:</para>
        /// <code>
        /// MyClass.InternalClass("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>internal class Program { }</c></para>
        /// </example>
        /// <example>
        /// <para>Example 2:</para>
        /// <code>
        /// MyClass.InternalClass("Program", "IProgram", "IBase").ToString();
        /// </code>
        /// result:
        /// <para><c>internal class Program : IProgram, IBase { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyClassBuilder InternalClass(string className, params string[] baseNames)
        {
            if (baseNames == null)
            {
                return MyClassBuilder.Internal.Name(className);
            }
            else
            {
                return MyClassBuilder.Internal.Name(className).BaseTypes(baseNames);
            }
        }

        /// <summary>
        /// Add class field
        /// </summary>
        /// <param name="field"></param>
        public void AddField(MyField field)
        {
            if(Fields == null)
            {
                Fields = [];
            }
            Fields.Add(field);
        }

        /// <summary>
        /// Add class property
        /// </summary>
        /// <param name="property"></param>
        public void AddProperty(MyProperty property)
        {
            if (Properties == null)
            {
                Properties = [];
            }
            Properties.Add(property);
        }

        /// <summary>
        /// Add class method
        /// </summary>
        /// <param name="method"></param>
        public void AddMethod(MyMethod method)
        {
            if (Methods == null)
            {
                Methods = [];
            }
            Methods.Add(method);
        }

        /// <summary>
        /// Add class method
        /// </summary>
        /// <param name="method"></param>
        public void AddConstructor(MyConstructor constructor)
        {
            if (Constructors == null)
            {
                Constructors = [];
            }
            Constructors.Add(constructor);
        }

        /// <summary>
        /// Generate code
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            return ToString(new MyClassWriter());
        }

        /// <summary>
        /// Generate code
        /// </summary>
        /// <param name="formatter"></param>
        /// <returns></returns>
        public string? ToString(ICodeWriter<MyClass> formatter)
        {
            return formatter?.WriteCode(this);
        }
    }
}
