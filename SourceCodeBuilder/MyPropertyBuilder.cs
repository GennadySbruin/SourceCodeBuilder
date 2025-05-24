using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public class MyPropertyBuilder
    {
        MyProperty _myProperty = new();
        public MyProperty Build()
        {
            return _myProperty;
        }

        /// <summary>
        /// Property with internal access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Internal.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>internal string Description { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder Internal
        {
            get
            {
                MyPropertyBuilder myPropertyBuilder = new MyPropertyBuilder();
                myPropertyBuilder.SetAccessModifier(MyProperty.AccessModifiers.Internal);
                return myPropertyBuilder;
            }
        }

        /// <summary>
        /// Property with private access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Private.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>private string Description { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder Private
        {
            get
            {
                MyPropertyBuilder myPropertyBuilder = new MyPropertyBuilder();
                myPropertyBuilder.SetAccessModifier(MyProperty.AccessModifiers.Private);
                return myPropertyBuilder;
            }
        }

        /// <summary>
        /// Property with protected access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Protected.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>protected string Description { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder Protected
        {
            get
            {
                SetAccessModifier(MyProperty.AccessModifiers.Protected);
                return this;
            }
        }

        /// <summary>
        /// Property with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Public.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>public string Description { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder Public
        {
            get
            {
                MyPropertyBuilder myPropertyBuilder = new MyPropertyBuilder();
                myPropertyBuilder.SetAccessModifier(MyProperty.AccessModifiers.Public);
                return myPropertyBuilder;
            }
        }

        /// <summary>
        /// Property with abstruct modifier. Use only in abstract class
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Abstract.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>abstract string Description { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder Abstract
        {
            get
            {
                SetAccessModifier(MyProperty.AccessModifiers.Abstract);
                return this;
            }
        }

        public virtual void CheckAccessModifier(MyProperty.AccessModifiers newModifier)
        {
            if (_myProperty.AccessModifiersList.Contains(newModifier))
            {
                throw new ArgumentException($"Property already contains access modifier '{newModifier}'");
            }
            CheckAccessConflict(newModifier);
            CheckStaticConflict(newModifier);
            CheckSealedConflict(newModifier);
            CheckAbstractConflict(newModifier);
        }

        /// <summary>
        /// Property with sealed modifier.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Sealed.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>sealed string Description { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder Sealed
        {
            get
            {
                SetAccessModifier(MyProperty.AccessModifiers.Sealed);
                return this;
            }
        }

        /// <summary>
        /// Property with virtual modifier.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Public.Virtual.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>public virtual string Description { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder Virtual
        {
            get
            {
                SetAccessModifier(MyProperty.AccessModifiers.Virtual);
                return this;
            }
        }

        /// <summary>
        /// Set name of property. REQUIRED
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// myPropertyBuilder.Name("System").ToString();
        /// </code>
        /// result:
        /// <para><c>object System { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder Name(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Propery name");
            }
            _myProperty.PropertyName = name;
            return this;
        }

        /// <summary>
        /// Set type name of property.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// myPropertyBuilder.Type("int").Name("Count").ToString();
        /// </code>
        /// result:
        /// <para><c>int Count { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder Type(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentNullException("Propery name");
            }
            _myProperty.PropertyTypeName = typeName;
            return this;
        }

        /// <summary>
        /// Update type name to type array name.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// var countProperty = myPropertyBuilder.Name("Lines").Array;
        /// </code>
        /// result:
        /// <para><c>object[] Lines { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder Array
        {
            get
            {
                if (string.IsNullOrEmpty(_myProperty.PropertyTypeName))
                {
                    throw new ArgumentNullException("Propery type is null");
                }
                _myProperty.PropertyTypeName += "[]";
                return this;
            }
        }

        /// <summary>
        /// Update type name to List of type.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// var countProperty = myPropertyBuilder.Type("string").Name("Lines").List;
        /// </code>
        /// result:
        /// <para><c> List>string> Lines { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder List
        {
            get
            {
                if (string.IsNullOrEmpty(_myProperty.PropertyTypeName))
                {
                    throw new ArgumentNullException("Propery type is null");
                }
                _myProperty.PropertyTypeName = $"List<{_myProperty.PropertyTypeName}>";
                return this;
            }
        }

        /// <summary>
        /// Property with static modifier.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Static.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>static string Description { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder Static
        {
            get
            {
                SetAccessModifier(MyProperty.AccessModifiers.Static);
                return this;
            }

        }

        internal void SetAccessModifier(MyProperty.AccessModifiers modifier)
        {
            CheckAccessModifier(modifier);
            _myProperty.AccessModifiersList.Add(modifier);
        }

        private void CheckAbstractConflict(MyProperty.AccessModifiers newModifier)
        {
            if (newModifier != MyProperty.AccessModifiers.Abstract)
            {
                return;
            }

            var sealedOrStatic = _myProperty.AccessModifiersList
                .FirstOrDefault(o => o == MyProperty.AccessModifiers.Sealed
                       || o == MyProperty.AccessModifiers.Static);

            if (sealedOrStatic != null)
            {
                throw new ArgumentException($"Failed to add access modifier '{newModifier}' for {sealedOrStatic} property");
            }
        }

        private void CheckAccessConflict(MyProperty.AccessModifiers newModifier)
        {
            MyProperty.AccessModifiers? classAccess = _myProperty.AccessModifiersList
                .FirstOrDefault(o => o == MyProperty.AccessModifiers.Private
                       || o == MyProperty.AccessModifiers.Internal
                       || o == MyProperty.AccessModifiers.Public);

            if (classAccess != null && classAccess != newModifier)
            {
                if (newModifier == MyProperty.AccessModifiers.Private
                       || newModifier == MyProperty.AccessModifiers.Internal
                       || newModifier == MyProperty.AccessModifiers.Public)
                {
                    throw new ArgumentException($"Property already contains access modifier '{classAccess}'. Failed to add '{newModifier}'");
                }
            }

            if (newModifier == MyProperty.AccessModifiers.Protected)
            {
                if (_myProperty.AccessModifiersList.Any(o => o == MyProperty.AccessModifiers.Public))
                {
                    throw new ArgumentException($"Property already contains access modifier 'public' and can`t contain '{newModifier}'");
                }
            }
        }

        private void CheckSealedConflict(MyProperty.AccessModifiers newModifier)
        {
            if (newModifier != MyProperty.AccessModifiers.Sealed)
            {
                return;
            }

            var abstractOrStatic = _myProperty.AccessModifiersList
                .FirstOrDefault(o => o == MyProperty.AccessModifiers.Abstract
                       || o == MyProperty.AccessModifiers.Static);

            if (abstractOrStatic != null)
            {
                throw new ArgumentException($"Failed to add access modifier '{newModifier}' for {abstractOrStatic} property");
            }
        }

        private void CheckStaticConflict(MyProperty.AccessModifiers newModifier)
        {
            if (newModifier != MyProperty.AccessModifiers.Static)
            {
                return;
            }

            var abstractOrSealed = _myProperty.AccessModifiersList
                .FirstOrDefault(o => o == MyProperty.AccessModifiers.Abstract
                       || o == MyProperty.AccessModifiers.Sealed);

            if (abstractOrSealed != null)
            {
                throw new ArgumentException($"Failed to add access modifier '{newModifier}' for {abstractOrSealed} property");
            }
        }

        public override string? ToString()
        {
            return ToString(MyDefaultPropertyDeclarationFormatter.Formatter);
        }

        public string? ToString(IFormatter<MyProperty> formatter)
        {
            return formatter?.ToString(Build());
        }
    }
}
