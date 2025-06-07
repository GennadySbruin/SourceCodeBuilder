using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public MyMethodBuilder WithGeneratedComments
        {
            get
            {
                _myMethod.AutoGenerateComments = true; ;
                return this;
            }
        }

        public MyMethodBuilder AddComment(string commentLine)
        {
            _myMethod.Comments.Add(commentLine);
            return this;
        }

        public MyMethodBuilder AddLines(IEnumerable<string> methodLines)
        {
            foreach (var methodLine in methodLines)
            {
                _myMethod.MyCode.NewLine.Tab.Add(methodLine);
            }
            return this;
        }

        public MyMethodBuilder AddComments(IEnumerable<string> commentLines)
        {
            foreach (var commentLine in commentLines)
            {
                _myMethod.Comments.Add(commentLine);
            }
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

        public MyMethodBuilder AddCodes(IEnumerable<MyCodeExpression> methodExpressions)
        {
            foreach (var methodExpression in methodExpressions)
            {
                _myMethod.MyCode.Add(methodExpression);
            }
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

        public MyMethodBuilder AddCodes(IEnumerable<MyCodeExpressionBuilder> methodExpressionBuilders)
        {
            foreach (var methodExpressionBuilder in methodExpressionBuilders)
            {
                _myMethod.MyCode.Add(methodExpressionBuilder);
            }
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

        public MyMethodBuilder AddVariables(IEnumerable<MyFieldBuilder> fieldBuilders)
        {
            foreach(var fieldBuilder in fieldBuilders)
            {
                _myMethod.MyCode.NewLine.Tab.Add(fieldBuilder);
            }
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

        /// <summary>
        /// Method with async modifier.
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
        public MyMethodBuilder Async
        {
            get
            {
                _myMethod.Async = true;
                return this;
            }

        }

        internal void SetAccessModifier(MyMethod.AccessModifiers modifier)
        {
            CheckAccessModifier(modifier);
            _myMethod.AccessModifiersList.Add(modifier);
        }

        public MyMethodBuilder AddAttribute(string attributeLine)
        {
            _myMethod.Attributes.Add(attributeLine);
            return this;
        }
        public MyMethodBuilder AddAttributes(IEnumerable<string> attributeLines)
        {
            foreach (var attributeLine in attributeLines)
            {
                _myMethod.Attributes.Add(attributeLine);
            }
            return this;
        }

        /// <summary>
        /// Add generic type T for method
        /// </summary>
        /// <returns></returns>
        public MyMethodBuilder Generic_T
        {
            get
            {
                AddGeneric("T");
                return this;
            }
        }

        /// <summary>
        /// Add generic types Tkey, TValue for method
        
        /// </summary>
        /// <returns></returns>
        public MyMethodBuilder Generic_TKey_TValue
        {
            get
            {
                AddGeneric("TKey");
                AddGeneric("TValue");
                return this;
            }
        }

        /// <summary>
        /// Add generic type for method
        /// </summary>
        /// <returns></returns>
        public MyMethodBuilder Generic(string generycTypeName, string genericTypeConstraint = null)
        {
            AddGeneric(generycTypeName, genericTypeConstraint);
            return this;
        }

        /// <summary>
        /// Add generic Constraint for method
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException">When class not contain <param>generycTypeName</param></exception>
        public MyMethodBuilder GenericConstraint(string generycTypeName, string genericTypeConstraint)
        {
            AddGenericConstraint(generycTypeName, genericTypeConstraint);
            return this;
        }

        /// <summary>
        /// Add list of generic types for method
        /// </summary>
        /// <returns></returns>
        public MyMethodBuilder Generics(params string[] generycTypeNames)
        {
            foreach (var generycTypeName in generycTypeNames)
            {
                AddGeneric(generycTypeName);
            }

            return this;
        }

        /// <summary>
        /// Add list of generic types for method.
        /// </summary>
        /// <returns></returns>
        public MyMethodBuilder Generics(string generycTypeNames)
        {
            string[] arr = generycTypeNames.Split(',');
            foreach (var generycTypeName in arr)
            {
                AddGeneric(generycTypeName.Trim());
            }

            return this;
        }

        /// <summary>
        /// Add generic types Tkey, TValue for method
        /// </summary>
        /// <returns></returns>
        public MyMethodBuilder GenericWhere(string where)
        {
            _myMethod.GenericWhere = where;
            return this;
        }

        internal void AddGeneric(string genericType, string genericTypeConstraint = null)
        {
            if (string.IsNullOrEmpty(genericType))
            {
                throw new ArgumentNullException("Generic type name");
            }

            if (_myMethod.GenericList == null)
            {
                _myMethod.GenericList = [];
            }
            else if (_myMethod.GenericList.Any(o => o == genericType))
            {
                throw new ArgumentException($"Generic type '{genericType}' already exits in class");
            }

            _myMethod.GenericList.Add(genericType);

            if (!string.IsNullOrEmpty(genericTypeConstraint))
            {
                if (_myMethod.GenericConstraintDictionary == null)
                {
                    _myMethod.GenericConstraintDictionary = [];
                }
                _myMethod.GenericConstraintDictionary.Add(genericType, genericTypeConstraint);
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

            if (_myMethod.GenericList == null)
            {
                _myMethod.GenericList = [];
            }

            if (_myMethod.GenericConstraintDictionary == null)
            {
                _myMethod.GenericConstraintDictionary = [];
            }

            if (_myMethod.GenericConstraintDictionary.ContainsKey(genericType))
            {
                throw new ArgumentNullException($"Generic type constraint already exists for generic type {genericType}");
            }

            if (!_myMethod.GenericList.Any(o => o == genericType))
            {
                _myMethod.GenericList.Add(genericType);
            }

            _myMethod.GenericConstraintDictionary.Add(genericType, genericTypeConstraint);
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
            return ToString(new MyMethodWriter());
        }

        /// <summary>
        /// Convert this method to result code string with assigned formatter.
        /// </summary>
        /// <returns></returns>
        public string? ToString(ICodeWriter<MyMethod> formatter)
        {
            return formatter?.WriteCode(Build());
        }
    }
}
