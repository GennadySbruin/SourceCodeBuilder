using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public class MyInterfaceBuilder
    {
        public static bool DEFAULT_PARTIAL = true;

        MyInterface _myInterface = new();
        internal string Tabs = string.Empty;

        /// <summary>
        /// Interface with internal access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Internal.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>internal interface Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyInterfaceBuilder Internal
        {
            get
            {
                MyInterfaceBuilder myInterfaceBuilder = new MyInterfaceBuilder();
                myInterfaceBuilder.SetAccessModifier(MyInterface.AccessModifiers.Internal);
                return myInterfaceBuilder;
            }
        }

        /// <summary>
        /// Partial interface
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Partial.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>partial interface Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyInterfaceBuilder Partial
        {
            get
            {
                MyInterfaceBuilder myInterfaceBuilder = new MyInterfaceBuilder();
                myInterfaceBuilder.SetAccessModifier(MyInterface.AccessModifiers.Partial);
                return myInterfaceBuilder;
            }
        }


        /// <summary>
        /// Interface with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Public.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>public interface Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyInterfaceBuilder Public
        {
            get
            {
                MyInterfaceBuilder myInterfaceBuilder = new MyInterfaceBuilder();
                myInterfaceBuilder.SetAccessModifier(MyInterface.AccessModifiers.Public);
                return myInterfaceBuilder;
            }
        }

        
        /// <summary>
        /// Add base interface with same name
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Name("Program").BaseInterface.ToString();
        /// </code>
        /// result:
        /// <code><c>interface Program : IProgram { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder BaseInterface
        {
            get
            {
                if (string.IsNullOrEmpty(_myInterface.InterfaceName))
                {
                    throw new ArgumentNullException("Interface name");
                }

                AddBase($"I{_myInterface.InterfaceName}");
                return this;
            }
        }

        /// <summary>
        /// Add generic type T for interface
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Name("Program").Generic_T.ToString();
        /// </code>
        /// result:
        /// <code><c>interface Program>T> { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder Generic_T
        {
            get
            {
                AddGeneric("T");
                return this;
            }
        }

        /// <summary>
        /// Add generic types Tkey, TValue for interface
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Name("Program").Generic_TKey_TValue.ToString();
        /// </code>
        /// result:
        /// <code><c>interface Program>Tkey, TValue> { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder Generic_TKey_TValue
        {
            get
            {
                AddGeneric("TKey");
                AddGeneric("TValue");
                return this;
            }
        }
                
        /// <summary>
        /// Add interface property
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyInterface.PublicInterface("Program").AddProperty(MyProperty.Int("Value").Init(100).Build()).ToString();
        /// </code>
        /// result:
        /// <code>
        /// public interface Program
        /// {
        ///     int Value { get; set; } = 100;;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>

        public MyInterfaceBuilder AddProperty(MyProperty? property)
        {
            if (property != null)
            {
                _myInterface.AddProperty(property);
            }
            return this;

        }

        /// <summary>
        /// Add interface property
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyInterface.PublicInterface("Program").AddProperty(MyProperty.Int("Value").Init(100)).ToString();
        /// </code>
        /// result:
        /// <code>
        /// public interface Program
        /// {
        ///     int Value { get; set; } = 100;;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder AddProperty(MyPropertyBuilder propertyBiulder)
        {
            return AddProperty(propertyBiulder?.Build());
        }

        /// <summary>
        /// Add interface properties
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// string[] ints = ["Length", "Length_01", "Length_02"];
        /// MyInterface.PublicInterface("Program").AddProperties
        ///         (ints.Select(o=> MyProperty.PublicInt(o).Init(o.Length))).ToString();
        /// </code>
        /// result:
        /// <code>
        /// public interface Program
        /// {
        ///     public int Length { get; set; } = 6;
        ///     public int Length_01 { get; set; } = 9;
        ///     public int Length_02 { get; set; } = 9;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder AddProperties(IEnumerable<MyPropertyBuilder> propertyBiulders)
        {
            foreach (var p in propertyBiulders)
            {
                AddProperty(p);
            }
            return this;
        }
                
        /// <summary>
        /// Add interface methods
        /// </summary>
        /// <param name="methodBiulders"></param>
        /// <returns></returns>
        public MyInterfaceBuilder AddMethods(IEnumerable<MyMethodBuilder> methodBiulders)
        {
            foreach (var m in methodBiulders)
            {
                AddMethod(m);
            }
            return this;
        }

        /// <summary>
        /// Add method
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyInterface.PublicInterface("Service")
        ///        .AddMethod(MyMethod.PublicInt("Calc").Static
        ///        .AddVariable(MyField.Int("constInt").Const.Init(100))
        ///        .AddLine("return constInt * 8;")).Build().ToString();
        /// </code>
        /// result:
        /// <code>
        /// public interface Service
        /// {
        ///     public static int Calc(int number)
        ///     {
        ///         const int constInt = 100;
        ///         return constInt * 8;
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder AddMethod(MyMethodBuilder methodBuilder)
        {
            return AddMethod(methodBuilder?.Build());
        }

        /// <summary>
        /// Add method
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyInterface.PublicInterface("Service")
        ///        .AddMethod(MyMethod.PublicInt("Calc").Static
        ///        .AddVariable(MyField.Int("constInt").Const.Init(100))
        ///        .AddLine("return constInt * 8;")).ToString();
        /// </code>
        /// result:
        /// <code>
        /// public interface Service
        /// {
        ///     public static int Calc(int number)
        ///     {
        ///         const int constInt = 100;
        ///         return constInt * 8;
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder AddMethod(MyMethod method)
        {
            if (method != null)
            {
                _myInterface.AddMethod(method);
                //method.AddTabs(_myInterface.Tabs + "    ");
            }
            return this;
        }

        /// <summary>
        /// Add base type with name
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Name("Program").BaseType("MyBaseInterface").ToString();
        /// </code>
        /// result:
        /// <code><c>interface Program : MyBaseInterface { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder BaseType(string baseTypeName)
        {
            AddBase(baseTypeName);
            return this;
        }

        /// <summary>
        /// Add base type with names
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Name("Program").BaseType("MyBaseInterface", "IList", "IBase").ToString();
        /// </code>
        /// result:
        /// <code><c>interface Program : MyBaseInterface, IList, IBase { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder BaseTypes(params string[] baseTypeNames)
        {
            foreach (var generycTypeName in baseTypeNames)
            {
                AddBase(generycTypeName);
            }

            return this;
        }

        /// <summary>
        /// Add base type with names
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Name("Program").BaseType("MyBaseInterface,IList, IBase").ToString();
        /// </code>
        /// result:
        /// <code><c>interface Program : MyBaseInterface, IList, IBase { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder BaseTypes(string baseTypeNames)
        {
            string[] arr = baseTypeNames.Split(',');
            foreach (var generycTypeName in arr)
            {
                AddBase(generycTypeName.Trim());
            }

            return this;
        }

        public MyInterface Build()
        {
            SetDefaults();
            return _myInterface;
        }

        private void SetDefaults()
        {
            if (MyInterfaceBuilder.DEFAULT_PARTIAL)
            {
                if (!_myInterface.AccessModifiersList.Contains(MyInterface.AccessModifiers.Partial))
                {
                    SetAccessModifier(MyInterface.AccessModifiers.Partial);
                }
            }
        }

        public virtual void CheckAccessModifier(MyInterface.AccessModifiers newModifier)
        {
            if (_myInterface.AccessModifiersList.Contains(newModifier))
            {
                throw new ArgumentException($"Interface already contains access modifier '{newModifier}'");
            }
            CheckAccessConflict(newModifier);
        }

        /// <summary>
        /// Add generic type for interface
        /// <example>
        /// <para>Example 1:</para>
        /// <code>
        /// MyInterfaceBuilder.Name("Program").Generic("T").ToString();
        /// </code>
        /// result:
        /// <code><c>interface Program>T> { }</c></code>
        /// </example>
        /// <example>
        /// <para>Example 2:</para>
        /// <code>
        /// MyInterfaceBuilder.Name("Program").Generic("T", "interface").ToString();
        /// </code>
        /// result:
        /// <code><c>interface Program>T> where T : interface { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder Generic(string generycTypeName, string genericTypeConstraint = null)
        {
            AddGeneric(generycTypeName, genericTypeConstraint);
            return this;
        }

        /// <summary>
        /// Add generic Constraint for interface
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Name("Program").GenericConstraint("T", "Delegate").ToString();
        /// </code>
        /// result:
        /// <code><c>interface Program>T> where T : Delegate { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">When interface not contain <param>generycTypeName</param></exception>
        public MyInterfaceBuilder GenericConstraint(string generycTypeName, string genericTypeConstraint)
        {
            AddGenericConstraint(generycTypeName, genericTypeConstraint);
            return this;
        }

        /// <summary>
        /// Add list of generic types for interface
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Name("Program").Generics("T", "T1", "T2").ToString();
        /// </code>
        /// result:
        /// <code><c>interface Program>T, T1, T2> { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder Generics(params string[] generycTypeNames)
        {
            foreach (var generycTypeName in generycTypeNames)
            {
                AddGeneric(generycTypeName);
            }

            return this;
        }

        /// <summary>
        /// Add list of generic types for interface.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Name("Program").Generics("T,T1 ,T2").ToString();
        /// </code>
        /// result:
        /// <code><c>interface Program>T, T1, T2> { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyInterfaceBuilder Generics(string generycTypeNames)
        {
            string[] arr = generycTypeNames.Split(',');
            foreach (var generycTypeName in arr)
            {
                AddGeneric(generycTypeName.Trim());
            }

            return this;
        }

        /// <summary>
        /// Set name of interface. REQUIRED
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyInterfaceBuilder.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>interface Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyInterfaceBuilder Name(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Interface name");
            }
            _myInterface.InterfaceName = name;
            return this;
        }

        public MyInterfaceBuilder AddComment(string commentLine)
        {
            _myInterface.Comments.Add(commentLine);
            return this;
        }
        public MyInterfaceBuilder AddComments(IEnumerable<string> commentLines)
        {
            foreach (var commentLine in commentLines)
            {
                _myInterface.Comments.Add(commentLine);
            }
            return this;
        }

        /// <summary>
        /// <example>
        /// <code>
        /// C# interface code;
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _myInterface?.ToString() ?? string.Empty;
        }

        internal void AddBase(string baseType)
        {
            if (string.IsNullOrEmpty(baseType))
            {
                throw new ArgumentNullException("Base type name");
            }

            if (_myInterface.BaseInterfaceList == null)
            {
                _myInterface.BaseInterfaceList = [];
            }
            else if (_myInterface.BaseInterfaceList.Any(o => o == baseType))
            {
                throw new ArgumentException($"Base type '{baseType}' already exits in interface");
            }

            _myInterface.BaseInterfaceList.Add(baseType);
        }

        internal void AddGeneric(string genericType, string genericTypeConstraint = null)
        {
            if (string.IsNullOrEmpty(genericType))
            {
                throw new ArgumentNullException("Generic type name");
            }

            if (_myInterface.GenericList == null)
            {
                _myInterface.GenericList = [];
            }
            else if (_myInterface.GenericList.Any(o => o == genericType))
            {
                throw new ArgumentException($"Generic type '{genericType}' already exits in interface");
            }

            _myInterface.GenericList.Add(genericType);

            if (!string.IsNullOrEmpty(genericTypeConstraint))
            {
                if (_myInterface.GenericConstraintDictionary == null)
                {
                    _myInterface.GenericConstraintDictionary = [];
                }
                _myInterface.GenericConstraintDictionary.Add(genericType, genericTypeConstraint);
            }
        }

        internal void AddGenericConstraint(string genericType, string genericTypeConstraint)
        {
            if (string.IsNullOrEmpty(genericType))
            {
                throw new ArgumentNullException("Generic type name");
            }

            if (string.IsNullOrEmpty(genericTypeConstraint))
            {
                throw new ArgumentNullException("Generic type constraint");
            }

            if (_myInterface.GenericList == null)
            {
                _myInterface.GenericList = [];
            }

            if (_myInterface.GenericConstraintDictionary == null)
            {
                _myInterface.GenericConstraintDictionary = [];
            }

            if (_myInterface.GenericConstraintDictionary.ContainsKey(genericType))
            {
                throw new ArgumentNullException($"Generic type constraint already exists for generic type {genericType}");
            }

            if (!_myInterface.GenericList.Any(o => o == genericType))
            {
                _myInterface.GenericList.Add(genericType);
            }

            _myInterface.GenericConstraintDictionary.Add(genericType, genericTypeConstraint);
        }

        internal void SetAccessModifier(MyInterface.AccessModifiers modifier)
        {
            CheckAccessModifier(modifier);
            _myInterface.AccessModifiersList.Add(modifier);
        }

        private void CheckAccessConflict(MyInterface.AccessModifiers newModifier)
        {
            MyInterface.AccessModifiers? interfaceAccess = _myInterface.AccessModifiersList
                .FirstOrDefault(o => o == MyInterface.AccessModifiers.Internal
                       || o == MyInterface.AccessModifiers.Public);

            if (interfaceAccess != null && interfaceAccess != newModifier)
            {
                if (newModifier == MyInterface.AccessModifiers.Internal
                       || newModifier == MyInterface.AccessModifiers.Public)
                {
                    throw new ArgumentException($"Class already contains access modifier '{interfaceAccess}'. Failed to add '{newModifier}'");
                }
            }
        }
    }
}
