using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SourceCodeBuilder
{
    public class MyClassBuilder
    {
        public static bool DEFAULT_PARTIAL = true;

        MyClass _myClass = new();
        internal string Tabs = string.Empty;

        /// <summary>
        /// Class with internal access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Internal.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>internal class Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyClassBuilder Internal
        {
            get
            {
                MyClassBuilder myClassBuilder = new MyClassBuilder();
                myClassBuilder.SetAccessModifier(MyClass.AccessModifiers.Internal);
                return myClassBuilder;
            }
        }

        /// <summary>
        /// Partial class
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Partial.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>partial class Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyClassBuilder Partial
        {
            get
            {
                MyClassBuilder myClassBuilder = new MyClassBuilder();
                myClassBuilder.SetAccessModifier(MyClass.AccessModifiers.Partial);
                return myClassBuilder;
            }
        }

        /// <summary>
        /// Class with private access. Use only for nested types
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.PrivateNested.Name("Program").Build().ToString();
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
        /// myClass = MyClassBuilder.PrivateNested.Name("Program");
        /// parentClass = MyClassBuilder.Name("AnyParentClassForExample");
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
        public static MyClassBuilder PrivateNested
        {
            get
            {
                MyClassBuilder myClassBuilder = new MyClassBuilder();
                myClassBuilder.SetAccessModifier(MyClass.AccessModifiers.Private);
                return myClassBuilder;
            }
        }

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
        public static MyClassBuilder ProtectedNested
        {
            get
            {
                MyClassBuilder myClassBuilder = new MyClassBuilder();
                myClassBuilder.SetAccessModifier(MyClass.AccessModifiers.Protected);
                return myClassBuilder;
            }
        }

        /// <summary>
        /// Class with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Public.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>public class Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyClassBuilder Public
        {
            get
            {
                MyClassBuilder myClassBuilder = new MyClassBuilder();
                myClassBuilder.SetAccessModifier(MyClass.AccessModifiers.Public);
                return myClassBuilder;
            }
        }

        /// <summary>
        /// Class with abstract modifier
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Abstract.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>abstract class Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder Abstract
        {
            get
            {
                SetAccessModifier(MyClass.AccessModifiers.Abstract);
                return this;
            }
        }

        /// <summary>
        /// Add base interface with same name
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Name("Program").BaseInterface.ToString();
        /// </code>
        /// result:
        /// <code><c>class Program : IProgram { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder BaseInterface
        {
            get
            {
                if (string.IsNullOrEmpty(_myClass.ClassName))
                {
                    throw new ArgumentNullException("Class name");
                }

                AddBase($"I{_myClass.ClassName}");
                return this;
            }
        }

        /// <summary>
        /// Add generic type T for class
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Name("Program").Generic_T.ToString();
        /// </code>
        /// result:
        /// <code><c>class Program>T> { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder Generic_T
        {
            get
            {
                AddGeneric("T");
                return this;
            }
        }

        /// <summary>
        /// Add generic types Tkey, TValue for class
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Name("Program").Generic_TKey_TValue.ToString();
        /// </code>
        /// result:
        /// <code><c>class Program>Tkey, TValue> { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder Generic_TKey_TValue
        {
            get
            {
                AddGeneric("TKey");
                AddGeneric("TValue");
                return this;
            }
        }

        /// <summary>
        /// Class with sealed modifier
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Sealed.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>sealed class Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder Sealed
        {
            get
            {
                SetAccessModifier(MyClass.AccessModifiers.Sealed);
                return this;
            }
        }

        /// <summary>
        /// Class with static modifier
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Static.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>static class Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder Static
        {
            get
            {
                SetAccessModifier(MyClass.AccessModifiers.Static);
                return this;
            }

        }

        /// <summary>
        /// Add class field
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyClass.PublicClass("Program").AddField(MyField.String("Name").Build()).ToString();
        /// </code>
        /// result:
        /// <code>
        /// public class Program
        /// {
        ///     string Name;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder AddField(MyField? field)
        {
            if(field != null)
            {
                _myClass.AddField(field);
                //field.Tabs = _myClass.Tabs + "    ";
            }
            return this;

        }

        /// <summary>
        /// Add class field
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyClass.PublicClass("Program").AddField(MyField.String("Name")).ToString();
        /// </code>
        /// result:
        /// <code>
        /// public class Program
        /// {
        ///     string Name;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder AddField(MyFieldBuilder fieldBuilder)
        {
            return AddField(fieldBuilder?.Build());
        }

        /// <summary>
        /// Add class property
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyClass.PublicClass("Program").AddProperty(MyProperty.Int("Value").Init(100).Build()).ToString();
        /// </code>
        /// result:
        /// <code>
        /// public class Program
        /// {
        ///     int Value { get; set; } = 100;;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>

        public MyClassBuilder AddProperty(MyProperty? property)
        {
            if (property != null)
            {
                _myClass.AddProperty(property);
                //property.Tabs = _myClass.Tabs + "    ";
            }
            return this;

        }

        /// <summary>
        /// Add class property
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyClass.PublicClass("Program").AddProperty(MyProperty.Int("Value").Init(100)).ToString();
        /// </code>
        /// result:
        /// <code>
        /// public class Program
        /// {
        ///     int Value { get; set; } = 100;;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder AddProperty(MyPropertyBuilder propertyBiulder)
        {
            return AddProperty(propertyBiulder?.Build());
        }

        /// <summary>
        /// Add method
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /////////////////////////////////////// MyClass.PublicClass("Program").AddMetho(MyField.String("Name")).ToString();
        /// </code>
        /// result:
        /// <code>
        /// public class Program
        /// {
        ///     string Name;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder AddMethod(MyMethodBuilder methodBuilder)
        {
            return AddMethod(methodBuilder?.Build());
        }

        /// <summary>
        /// Add method
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /////////////////////////////////////// MyClass.PublicClass("Program").AddMetho(MyField.String("Name")).ToString();
        /// </code>
        /// result:
        /// <code>
        /// public class Program
        /// {
        ///     string Name;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder AddMethod(MyMethod method)
        {
            if (method != null)
            {
                _myClass.AddMethod(method);
                //method.AddTabs(_myClass.Tabs + "    ");
            }
            return this;
        }

        /// <summary>
        /// Add base type with name
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Name("Program").BaseType("MyBaseClass").ToString();
        /// </code>
        /// result:
        /// <code><c>class Program : MyBaseClass { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder BaseType(string baseTypeName)
        {
            AddBase(baseTypeName);
            return this;
        }

        /// <summary>
        /// Add base type with names
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Name("Program").BaseType("MyBaseClass", "IList", "IBase").ToString();
        /// </code>
        /// result:
        /// <code><c>class Program : MyBaseClass, IList, IBase { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder BaseTypes(params string[] baseTypeNames)
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
        /// MyClassBuilder.Name("Program").BaseType("MyBaseClass,IList, IBase").ToString();
        /// </code>
        /// result:
        /// <code><c>class Program : MyBaseClass, IList, IBase { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder BaseTypes(string baseTypeNames)
        {
            string[] arr = baseTypeNames.Split(',');
            foreach (var generycTypeName in arr)
            {
                AddBase(generycTypeName.Trim());
            }

            return this;
        }

        public MyClass Build()
        {
            SetDefaults();
            return _myClass;
        }

        private void SetDefaults()
        {
            if (MyClassBuilder.DEFAULT_PARTIAL)
            {
                if (!_myClass.AccessModifiersList.Contains(MyClass.AccessModifiers.Partial))
                {
                    SetAccessModifier(MyClass.AccessModifiers.Partial);
                }
            }
        }

        public virtual void CheckAccessModifier(MyClass.AccessModifiers newModifier)
        {
            if (_myClass.AccessModifiersList.Contains(newModifier))
            {
                throw new ArgumentException($"Class already contains access modifier '{newModifier}'");
            }
            CheckAccessConflict(newModifier);
            CheckStaticConflict(newModifier);
            CheckSealedConflict(newModifier);
            CheckAbstractConflict(newModifier);
        }

        /// <summary>
        /// Add generic type for class
        /// <example>
        /// <para>Example 1:</para>
        /// <code>
        /// MyClassBuilder.Name("Program").Generic("T").ToString();
        /// </code>
        /// result:
        /// <code><c>class Program>T> { }</c></code>
        /// </example>
        /// <example>
        /// <para>Example 2:</para>
        /// <code>
        /// MyClassBuilder.Name("Program").Generic("T", "class").ToString();
        /// </code>
        /// result:
        /// <code><c>class Program>T> where T : class { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder Generic(string generycTypeName, string genericTypeConstraint = null)
        {
            AddGeneric(generycTypeName, genericTypeConstraint);
            return this;
        }

        /// <summary>
        /// Add generic Constraint for class
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Name("Program").GenericConstraint("T", "Delegate").ToString();
        /// </code>
        /// result:
        /// <code><c>class Program>T> where T : Delegate { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">When class not contain <param>generycTypeName</param></exception>
        public MyClassBuilder GenericConstraint(string generycTypeName, string genericTypeConstraint)
        {
            AddGenericConstraint(generycTypeName, genericTypeConstraint);
            return this;
        }

        /// <summary>
        /// Add list of generic types for class
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Name("Program").Generics("T", "T1", "T2").ToString();
        /// </code>
        /// result:
        /// <code><c>class Program>T, T1, T2> { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder Generics(params string[] generycTypeNames)
        {
            foreach (var generycTypeName in generycTypeNames)
            {
                AddGeneric(generycTypeName);
            }

            return this;
        }

        /// <summary>
        /// Add list of generic types for class.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Name("Program").Generics("T,T1 ,T2").ToString();
        /// </code>
        /// result:
        /// <code><c>class Program>T, T1, T2> { }</c></code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyClassBuilder Generics(string generycTypeNames)
        {
            string[] arr = generycTypeNames.Split(',');
            foreach (var generycTypeName in arr)
            {
                AddGeneric(generycTypeName.Trim());
            }

            return this;
        }

        /// <summary>
        /// Set name of class. REQUIRED
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyClassBuilder.Name("Program").ToString();
        /// </code>
        /// result:
        /// <para><c>class Program { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyClassBuilder Name(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Class name");
            }
            _myClass.ClassName = name;
            return this;
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
            return _myClass?.ToString() ?? string.Empty;
        }

        internal void AddBase(string baseType)
        {
            if (string.IsNullOrEmpty(baseType))
            {
                throw new ArgumentNullException("Base type name");
            }

            if (_myClass.BaseClassList == null)
            {
                _myClass.BaseClassList = [];
            }
            else if (_myClass.BaseClassList.Any(o => o == baseType))
            {
                throw new ArgumentException($"Base type '{baseType}' already exits in class");
            }

            _myClass.BaseClassList.Add(baseType);
        }

        internal void AddGeneric(string genericType, string genericTypeConstraint = null)
        {
            if (string.IsNullOrEmpty(genericType))
            {
                throw new ArgumentNullException("Generic type name");
            }

            if (_myClass.GenericList == null)
            {
                _myClass.GenericList = [];
            }
            else if (_myClass.GenericList.Any(o => o == genericType))
            {
                throw new ArgumentException($"Generic type '{genericType}' already exits in class");
            }

            _myClass.GenericList.Add(genericType);

            if (!string.IsNullOrEmpty(genericTypeConstraint))
            {
                if (_myClass.GenericConstraintDictionary == null)
                {
                    _myClass.GenericConstraintDictionary = [];
                }
                _myClass.GenericConstraintDictionary.Add(genericType, genericTypeConstraint);
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

            if (_myClass.GenericList == null)
            {
                _myClass.GenericList = [];
            }

            if (_myClass.GenericConstraintDictionary == null)
            {
                _myClass.GenericConstraintDictionary = [];
            }

            if (_myClass.GenericConstraintDictionary.ContainsKey(genericType))
            {
                throw new ArgumentNullException($"Generic type constraint already exists for generic type {genericType}");
            }

            if (!_myClass.GenericList.Any(o => o == genericType))
            {
                _myClass.GenericList.Add(genericType);
            }

            _myClass.GenericConstraintDictionary.Add(genericType, genericTypeConstraint);
        }

        internal void SetAccessModifier(MyClass.AccessModifiers modifier)
        {
            CheckAccessModifier(modifier);
            _myClass.AccessModifiersList.Add(modifier);
        }

        private void CheckAbstractConflict(MyClass.AccessModifiers newModifier)
        {
            if (newModifier != MyClass.AccessModifiers.Abstract)
            {
                return;
            }

            var sealedOrStatic = _myClass.AccessModifiersList
                .FirstOrDefault(o => o == MyClass.AccessModifiers.Sealed
                       || o == MyClass.AccessModifiers.Static);

            if (sealedOrStatic != null)
            {
                throw new ArgumentException($"Failed to add access modifier '{newModifier}' for {sealedOrStatic} class");
            }
        }

        private void CheckAccessConflict(MyClass.AccessModifiers newModifier)
        {
            MyClass.AccessModifiers? classAccess = _myClass.AccessModifiersList
                .FirstOrDefault(o => o == MyClass.AccessModifiers.Private
                       || o == MyClass.AccessModifiers.Internal
                       || o == MyClass.AccessModifiers.Public);

            if (classAccess != null && classAccess != newModifier)
            {
                if (newModifier == MyClass.AccessModifiers.Private
                       || newModifier == MyClass.AccessModifiers.Internal
                       || newModifier == MyClass.AccessModifiers.Public)
                {
                    throw new ArgumentException($"Class already contains access modifier '{classAccess}'. Failed to add '{newModifier}'");
                }
            }

            if (newModifier == MyClass.AccessModifiers.Protected)
            {
                if (_myClass.AccessModifiersList.Any(o => o == MyClass.AccessModifiers.Public))
                {
                    throw new ArgumentException($"Class already contains access modifier 'public' and can`t contain '{newModifier}'");
                }
            }
        }

        private void CheckSealedConflict(MyClass.AccessModifiers newModifier)
        {
            if (newModifier != MyClass.AccessModifiers.Sealed)
            {
                return;
            }

            var abstractOrStatic = _myClass.AccessModifiersList
                .FirstOrDefault(o => o == MyClass.AccessModifiers.Abstract
                       || o == MyClass.AccessModifiers.Static);

            if (abstractOrStatic != null)
            {
                throw new ArgumentException($"Failed to add access modifier '{newModifier}' for {abstractOrStatic} class");
            }
        }

        private void CheckStaticConflict(MyClass.AccessModifiers newModifier)
        {
            if (newModifier != MyClass.AccessModifiers.Static)
            {
                return;
            }

            var abstractOrSealed = _myClass.AccessModifiersList
                .FirstOrDefault(o => o == MyClass.AccessModifiers.Abstract
                       || o == MyClass.AccessModifiers.Sealed);

            if (abstractOrSealed != null)
            {
                throw new ArgumentException($"Failed to add access modifier '{newModifier}' for {abstractOrSealed} class");
            }
        }


        class TestClass<T, M> where T : class where M : class { }
    }
}
