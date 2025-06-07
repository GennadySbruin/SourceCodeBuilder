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
                throw new ArgumentNullException("Property name");
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
                throw new ArgumentNullException("Property name");
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
                    throw new ArgumentNullException("Property type is null");
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
                    throw new ArgumentNullException("Property type is null");
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

        public MyPropertyBuilder AddComment(string commentLine)
        {
            _myProperty.Comments.Add(commentLine);
            return this;
        }
        public MyPropertyBuilder AddComments(IEnumerable<string> commentLines)
        {
            foreach (var commentLine in commentLines)
            {
                _myProperty.Comments.Add(commentLine);
            }
            return this;
        }

        /// <summary>
        /// Set initial expression from string.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Type("string").Name("Value").Init("Test").ToString();
        /// </code>
        /// result:
        /// <para><c>string Value { get; set; } = "Test";</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder Init(string expression)
        {
            _myProperty.InitialExpression = $"\"{expression}\"";
            return this;
        }

        /// <summary>
        /// Set initial expression from int.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Type("int").Name("Value").Init(2).ToString();
        /// </code>
        /// result:
        /// <para><c>int Value { get; set; } = 2;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder Init(int expression)
        {
            _myProperty.InitialExpression = expression.ToString();
            return this;
        }

        /// <summary>
        /// Set labmda getter expression.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Type("int").Name("Value").LambdaGetter(" => 4;").ToString();
        /// </code>
        /// result:
        /// <para><c>int Value => 4;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder LambdaGetter(string lambdaExpression)
        {
            _myProperty.LambdaExpression = lambdaExpression;
            return this;
        }

        /// <summary>
        /// Set initial expression from double.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Type("double").Name("Value").Init(2.3).ToString();
        /// </code>
        /// result:
        /// <para><c>double Value { get; set; } = 2.3;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder Init(double expression)
        {
            _myProperty.InitialExpression = expression.ToString();
            return this;
        }

        /// <summary>
        /// Set initial expression from decimal.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Type("decimal").Name("Value").Init(2.4M).ToString();
        /// </code>
        /// result:
        /// <para><c>deciaml Value { get; set; } = 2.4M;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder Init(decimal expression)
        {
            _myProperty.InitialExpression = expression.ToString();
            return this;
        }

        /// <summary>
        /// Set initial expression from float.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Type("float").Name("Value").Init(2.001f).ToString();
        /// </code>
        /// result:
        /// <para><c>float Value { get; set; } = 2.001f;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder Init(float expression)
        {
            _myProperty.InitialExpression = expression.ToString();
            return this;
        }

        /// <summary>
        /// Set initial expression from boolean.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Type("bool").Name("Value").Init(true).ToString();
        /// </code>
        /// result:
        /// <para><c>bool Value { get; set; } = true;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder Init(bool expression)
        {
            _myProperty.InitialExpression = expression ? "true" : "false";
            return this;
        }

        /// <summary>
        /// Set initial expression from ExpressionBuilder.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Type("float").Name("Value").Init(2.001f).ToString();
        /// </code>
        /// result:
        /// <para><c>float Value { get; set; } = 2.001f;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder Init(MyCodeExpressionBuilder expressionBuilder)
        {
            _myProperty.InitialExpression = expressionBuilder.ToString();
            return this;
        }

        /// <summary>
        /// Get only property.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyPublicFloat("Value").GetOnly.ToString();
        /// </code>
        /// result:
        /// <para><c>public Value { get ... ; };</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder GetOnly
        {
            get
            {
                _myProperty.WithSetter = false;
                _myProperty.WithGetter = true;
                return this;
            }
        }

        /// <summary>
        /// Set only property.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyPublicFloat("Value").SetOnly.ToString();
        /// </code>
        /// result:
        /// <para><c>public Value { set ... ; };</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyPropertyBuilder SetOnly
        {
            get
            {
                _myProperty.WithSetter = true;
                _myProperty.WithGetter = false;
                return this;
            }
        }

        /// <summary>
        /// Set getter code (default = get;)
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyProperty.PublicInt("Count").GetterExpression("{ return _count; }").GetOnly.ToString();
        /// </code>
        /// result:
        /// <para><c>int Count { get { return _count; } }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder GetterExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentNullException("Expression");
            }
            _myProperty.GetterExpression = expression;
            return this;
        }

        /// <summary>
        /// Set getter code (default = get;)
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyProperty.PublicInt("Count").GetterExpression("{ return _count; }").GetOnly.ToString();
        /// </code>
        /// result:
        /// <para><c>int Count { get { return _count; } }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder GetterExpression(MyCodeExpression expression)
        {
            _myProperty.GetterExpression = expression.ToString();
            return this;
        }

        /// <summary>
        /// Set getter code (default = get;)
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyProperty.PublicInt("Count").GetterExpression("{ return _count; }").GetOnly.ToString();
        /// </code>
        /// result:
        /// <para><c>int Count { get { return _count; } }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder GetterExpression(MyCodeExpressionBuilder expressionBuilder)
        {
            _myProperty.GetterExpression = expressionBuilder.ToString();
            return this;
        }

        /// <summary>
        /// Set getter code (default = get;)
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyProperty.PublicInt("Count").GetterExpression("{ return _count; }").GetOnly.ToString();
        /// </code>
        /// result:
        /// <para><c>int Count { set { _count = value; } }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder SetterExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentNullException("Expression");
            }
            _myProperty.SetterExpression = expression;
            return this;
        }

        /// <summary>
        /// Set getter code (default = get;)
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyProperty.PublicInt("Count").GetterExpression("{ return _count; }").GetOnly.ToString();
        /// </code>
        /// result:
        /// <para><c>int Count { set { _count = value; } }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder SetterExpression(MyCodeExpression expression)
        {
            _myProperty.SetterExpression = expression.ToString();
            return this;
        }

        /// <summary>
        /// Set getter code (default = get;)
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyProperty.PublicInt("Count").GetterExpression("{ return _count; }").GetOnly.ToString();
        /// </code>
        /// result:
        /// <para><c>int Count { set { _count = value; } }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder SetterExpression(MyCodeExpressionBuilder expressionBuilder)
        {
            _myProperty.SetterExpression = expressionBuilder.ToString();
            return this;
        }

        /// <summary>
        /// Set getter and setter code, expand code block
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyProperty.PublicInt("Count").ExpandGetterSetter("{ return _count; }").GetOnly.ToString();
        /// </code>
        /// result:
        /// <code>
        /// int Count
        /// {
        ///     get
        ///     {
        ///         return _count;
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder ExpandGetterSetter(string? getterExpression, string? setterExpression)
        {
            _myProperty.ExpandCodeBlock = true;
            if (!string.IsNullOrEmpty(getterExpression))
            {
                _myProperty.GetterExpression = getterExpression;
            }
            if (!string.IsNullOrEmpty(setterExpression))
            {
                _myProperty.SetterExpression = setterExpression;
            }
            return this;
        }

        /// <summary>
        /// Set getter and setter code, expand code block
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyProperty.PublicInt("Count").ExpandGetterSetter("{ return _count; }").GetOnly.ToString();
        /// </code>
        /// result:
        /// <code>
        /// int Count
        /// {
        ///     get
        ///     {
        ///         return _count;
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder ExpandGetterSetter(MyProperty? getterExpression, MyProperty? setterExpression)
        {
            _myProperty.ExpandCodeBlock = true;
            if (getterExpression != null)
            {
                _myProperty.GetterExpression = getterExpression.ToString();
            }
            if (setterExpression != null)
            {
                _myProperty.SetterExpression = setterExpression.ToString();
            }
            return this;
        }

        /// <summary>
        /// Set getter and setter code, expand code block
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyProperty.PublicInt("Count").ExpandGetterSetter("{ return _count; }").GetOnly.ToString();
        /// </code>
        /// result:
        /// <code>
        /// int Count
        /// {
        ///     get
        ///     {
        ///         return _count;
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyPropertyBuilder ExpandGetterSetter(MyPropertyBuilder? getterExpressionBuilder, MyPropertyBuilder? setterExpressionBuilder)
        {
            _myProperty.ExpandCodeBlock = true;
            if (getterExpressionBuilder != null)
            {
                _myProperty.GetterExpression = getterExpressionBuilder.ToString();
            }
            if (setterExpressionBuilder != null)
            {
                _myProperty.SetterExpression = setterExpressionBuilder.ToString();
            }
            return this;
        }

        public MyPropertyBuilder AddAttribute(string attributeLine)
        {
            _myProperty.Attributes.Add(attributeLine);
            return this;
        }
        public MyPropertyBuilder AddAttributes(IEnumerable<string> attributeLines)
        {
            foreach (var attributeLine in attributeLines)
            {
                _myProperty.Attributes.Add(attributeLine);
            }
            return this;
        }

        /// <summary>
        /// Convert this property to result code string with default formatter.
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            return ToString(new MyPropertyWriter());
        }

        /// <summary>
        /// Convert this property to result code string with assigned formatter.
        /// </summary>
        /// <returns></returns>
        public string? ToString(ICodeWriter<MyProperty> formatter)
        {
            return formatter?.WriteCode(Build());
        }
    }
}
