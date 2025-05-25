using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public class MyMethodBuilder
    {
        MyMethod _myMethod = new();
        public MyMethod Build()
        {
            return _myMethod;
        }

        /// <summary>
        /// Method with internal access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Internal.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>internal string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder Internal
        {
            get
            {
                MyMethodBuilder myMethodBuilder = new MyMethodBuilder();
                myMethodBuilder.SetAccessModifier(MyMethod.AccessModifiers.Internal);
                return myMethodBuilder;
            }
        }

        /// <summary>
        /// Method with private access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Private.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>private string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder Private
        {
            get
            {
                MyMethodBuilder myMethodBuilder = new MyMethodBuilder();
                myMethodBuilder.SetAccessModifier(MyMethod.AccessModifiers.Private);
                return myMethodBuilder;
            }
        }

        /// <summary>
        /// Method with protected access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Protected.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>protected string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyMethodBuilder Protected
        {
            get
            {
                SetAccessModifier(MyMethod.AccessModifiers.Protected);
                return this;
            }
        }

        /// <summary>
        /// Method with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Public.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>public string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder Public
        {
            get
            {
                MyMethodBuilder myMethodBuilder = new MyMethodBuilder();
                myMethodBuilder.SetAccessModifier(MyMethod.AccessModifiers.Public);
                return myMethodBuilder;
            }
        }

        /// <summary>
        /// Method with abstruct modifier. Use only in abstract class
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Abstract.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>abstract string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyMethodBuilder Abstract
        {
            get
            {
                SetAccessModifier(MyMethod.AccessModifiers.Abstract);
                return this;
            }
        }

        public virtual void CheckAccessModifier(MyMethod.AccessModifiers newModifier)
        {
            if (_myMethod.AccessModifiersList.Contains(newModifier))
            {
                throw new ArgumentException($"Method already contains access modifier '{newModifier}'");
            }
            CheckAccessConflict(newModifier);
            CheckStaticConflict(newModifier);
            CheckSealedConflict(newModifier);
            CheckAbstractConflict(newModifier);
        }

        /// <summary>
        /// Method with sealed modifier.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Sealed.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>sealed string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyMethodBuilder Sealed
        {
            get
            {
                SetAccessModifier(MyMethod.AccessModifiers.Sealed);
                return this;
            }
        }

        /// <summary>
        /// Method with virtual modifier.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Public.Virtual.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>public virtual string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyMethodBuilder Virtual
        {
            get
            {
                SetAccessModifier(MyMethod.AccessModifiers.Virtual);
                return this;
            }
        }

        /// <summary>
        /// Set name of method. REQUIRED
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// myMethodBuilder.Name("System").ToString();
        /// </code>
        /// result:
        /// <para><c>object System() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyMethodBuilder Name(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Method name");
            }
            _myMethod.MethodName = name;
            return this;
        }

        /// <summary>
        /// Set type name of method.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// myMethodBuilder.Type("int").Name("Count").ToString();
        /// </code>
        /// result:
        /// <para><c>int Count() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyMethodBuilder Type(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentNullException("Method name");
            }
            _myMethod.MethodReturnTypeName = typeName;
            return this;
        }

        /// <summary>
        /// Set parameter of method.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// myMethodBuilder.Type("int").Name("Count").Parameter("int[]", "values", isRef : true).ToString();
        /// </code>
        /// result:
        /// <para><c>int Count(ref int[] values) { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyMethodBuilder Parameter(string typeName, string name, string defaultValue = null, bool isRef = false, bool isIn = false, bool isOut = false, bool isParams = false)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentNullException("Parameter type name");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Parameter name");
            }
            StringBuilder stringBuilder = new();
            if (isParams)
            {
                stringBuilder.Append("param ");
            }
            if (isRef)
            {
                stringBuilder.Append("ref ");
            }
            if (isOut)
            {
                stringBuilder.Append("out ");
            }
            if (isIn)
            {
                stringBuilder.Append("in ");
            }
            stringBuilder.Append(typeName);
            stringBuilder.Append(" ");
            stringBuilder.Append(name);
            if (!string.IsNullOrEmpty(defaultValue))
            {
                stringBuilder.Append(" " + defaultValue);
            }
            _myMethod.AddParameter(stringBuilder.ToString());
            
            return this;
        }

        /// <summary>
        /// Set parameter of method.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// myMethodBuilder.Type("bool").Name("Checked").Parameter("string userName").ToString();
        /// </code>
        /// result:
        /// <para><c>bool Checked(string userName) { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyMethodBuilder Parameter(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentNullException("Parameter");
            }
            _myMethod.AddParameter(parameter);

            return this;
        }

        /// <summary>
        /// Add line to method body.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// myMethodBuilder.Type("bool").Name("Checked")
        /// .AddLine("    checked = !checked;")
        /// .AddLine("    return checked;").ToString();
        /// </code>
        /// result:
        /// <code>
        /// bool Checked()
        /// {
        ///     checked = !checked;
        ///     return false;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyMethodBuilder AddLine(string methodLine)
        {
            if (string.IsNullOrEmpty(methodLine))
            {
                throw new ArgumentNullException("Method line");
            }
            _myMethod.MyCode.NewLine.Tab.Add(methodLine);

            return this;
        }

        /// <summary>
        /// Add line to method body.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// myMethodBuilder.Type("bool").Name("Checked")
        /// .AddCode(MyCodeExpressionBuilder.Start(1).Add("return false;").Build())
        /// </code>
        /// result:
        /// <code>
        /// bool Checked()
        /// {
        ///     return false;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyMethodBuilder AddCode(MyCodeExpression methodExpression)
        {
            _myMethod.MyCode.Add(methodExpression);

            return this;
        }

        /// <summary>
        /// Add line to method body.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// myMethodBuilder.Type("bool").Name("Checked")
        /// .AddCode(MyCodeExpressionBuilder.Start(1).Add("return false;"))
        /// </code>
        /// result:
        /// <code>
        /// bool Checked()
        /// {
        ///     return false;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyMethodBuilder AddCode(MyCodeExpressionBuilder methodExpressionBuilder)
        {
            _myMethod.MyCode.Add(methodExpressionBuilder);

            return this;
        }

        /// <summary>
        /// Add variable code.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// myMethodBuilder.Type("bool").Name("Checked")
        /// .AddCode(MyField.Boolean("calculated").Init("true").Finish)
        /// .AddCode(MyCodeExpressionBuilder.Start(1).Add("return false;"))
        /// </code>
        /// result:
        /// <code>
        /// bool Checked()
        /// {
        ///     bool calculated = true;
        ///     return false;
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyMethodBuilder AddVariable(MyFieldBuilder fieldBuilder)
        {
            _myMethod.MyCode.NewLine.Tab.Add(fieldBuilder);

            return this;
        }

        /// <summary>
        /// Update type name to type array name.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// var countMethod = myMethodBuilder.Name("Lines").Array;
        /// </code>
        /// result:
        /// <para><c>object[] Lines() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyMethodBuilder Array
        {
            get
            {
                if (string.IsNullOrEmpty(_myMethod.MethodReturnTypeName))
                {
                    throw new ArgumentNullException("Method type is null");
                }
                _myMethod.MethodReturnTypeName += "[]";
                return this;
            }
        }

        /// <summary>
        /// Update type name to List of type.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// var countMethod = myMethodBuilder.Type("string").Name("Lines").List;
        /// </code>
        /// result:
        /// <para><c> List>string> Lines() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyMethodBuilder List
        {
            get
            {
                if (string.IsNullOrEmpty(_myMethod.MethodReturnTypeName))
                {
                    throw new ArgumentNullException("Method type is null");
                }
                _myMethod.MethodReturnTypeName = $"List<{_myMethod.MethodReturnTypeName}>";
                return this;
            }
        }

        /// <summary>
        /// Method with static modifier.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Static.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>static string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyMethodBuilder Static
        {
            get
            {
                SetAccessModifier(MyMethod.AccessModifiers.Static);
                return this;
            }

        }

        internal void SetAccessModifier(MyMethod.AccessModifiers modifier)
        {
            CheckAccessModifier(modifier);
            _myMethod.AccessModifiersList.Add(modifier);
        }

        private void CheckAbstractConflict(MyMethod.AccessModifiers newModifier)
        {
            if (newModifier != MyMethod.AccessModifiers.Abstract)
            {
                return;
            }

            var sealedOrStatic = _myMethod.AccessModifiersList
                .FirstOrDefault(o => o == MyMethod.AccessModifiers.Sealed
                       || o == MyMethod.AccessModifiers.Static);

            if (sealedOrStatic != null)
            {
                throw new ArgumentException($"Failed to add access modifier '{newModifier}' for {sealedOrStatic} method");
            }
        }

        private void CheckAccessConflict(MyMethod.AccessModifiers newModifier)
        {
            MyMethod.AccessModifiers? classAccess = _myMethod.AccessModifiersList
                .FirstOrDefault(o => o == MyMethod.AccessModifiers.Private
                       || o == MyMethod.AccessModifiers.Internal
                       || o == MyMethod.AccessModifiers.Public);

            if (classAccess != null && classAccess != newModifier)
            {
                if (newModifier == MyMethod.AccessModifiers.Private
                       || newModifier == MyMethod.AccessModifiers.Internal
                       || newModifier == MyMethod.AccessModifiers.Public)
                {
                    throw new ArgumentException($"Method already contains access modifier '{classAccess}'. Failed to add '{newModifier}'");
                }
            }

            if (newModifier == MyMethod.AccessModifiers.Protected)
            {
                if (_myMethod.AccessModifiersList.Any(o => o == MyMethod.AccessModifiers.Public))
                {
                    throw new ArgumentException($"Method already contains access modifier 'public' and can`t contain '{newModifier}'");
                }
            }
        }

        private void CheckSealedConflict(MyMethod.AccessModifiers newModifier)
        {
            if (newModifier != MyMethod.AccessModifiers.Sealed)
            {
                return;
            }

            var abstractOrStatic = _myMethod.AccessModifiersList
                .FirstOrDefault(o => o == MyMethod.AccessModifiers.Abstract
                       || o == MyMethod.AccessModifiers.Static);

            if (abstractOrStatic != null)
            {
                throw new ArgumentException($"Failed to add access modifier '{newModifier}' for {abstractOrStatic} method");
            }
        }

        private void CheckStaticConflict(MyMethod.AccessModifiers newModifier)
        {
            if (newModifier != MyMethod.AccessModifiers.Static)
            {
                return;
            }

            var abstractOrSealed = _myMethod.AccessModifiersList
                .FirstOrDefault(o => o == MyMethod.AccessModifiers.Abstract
                       || o == MyMethod.AccessModifiers.Sealed);

            if (abstractOrSealed != null)
            {
                throw new ArgumentException($"Failed to add access modifier '{newModifier}' for {abstractOrSealed} method");
            }
        }


        /// <summary>
        /// Convert this method to result code string with default formatter.
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            return ToString(new MyDefaultMethodDeclarationFormatter());
        }

        /// <summary>
        /// Convert this method to result code string with assigned formatter.
        /// </summary>
        /// <returns></returns>
        public string? ToString(IFormatter<MyMethod> formatter)
        {
            return formatter?.ToString(Build());
        }
    }
}
