using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public class MyFieldBuilder
    {
        MyField _myField = new MyField();

        public MyField Build()
        {
            return _myField;
        }

        /// <summary>
        /// Field with internal access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Internal.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>internal string Description;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder Internal
        {
            get
            {
                MyFieldBuilder myFieldBuilder = new MyFieldBuilder();
                myFieldBuilder.SetAccessModifier(MyField.AccessModifiers.Internal);
                return myFieldBuilder;
            }
        }

        /// <summary>
        /// Field with private access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Private.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>private string Description;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder Private
        {
            get
            {
                MyFieldBuilder myFieldBuilder = new MyFieldBuilder();
                myFieldBuilder.SetAccessModifier(MyField.AccessModifiers.Private);
                return myFieldBuilder;
            }
        }

        /// <summary>
        /// Field with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Public.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>public string Description;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder Public
        {
            get
            {
                MyFieldBuilder myFieldBuilder = new MyFieldBuilder();
                myFieldBuilder.SetAccessModifier(MyField.AccessModifiers.Public);
                return myFieldBuilder;
            }
        }
                

        public virtual void CheckAccessModifier(MyField.AccessModifiers newModifier)
        {
            if (_myField.AccessModifiersList.Contains(newModifier))
            {
                throw new ArgumentException($"Field already contains access modifier '{newModifier}'");
            }
            CheckAccessConflict(newModifier);
        }
                

        /// <summary>
        /// Set name of Field. REQUIRED
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// myFieldBuilder.Name("System").ToString();
        /// </code>
        /// result:
        /// <para><c>object System;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyFieldBuilder Name(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("Propery name");
            }
            _myField.FieldName = name;
            return this;
        }

        /// <summary>
        /// Set type name of Field.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// myFieldBuilder.Type("int").Name("Count").ToString();
        /// </code>
        /// result:
        /// <para><c>int Count;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyFieldBuilder Type(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
            {
                throw new ArgumentNullException("Propery name");
            }
            _myField.FieldTypeName = typeName;
            return this;
        }

        /// <summary>
        /// Update type name to type array name.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// var countField = myFieldBuilder.Name("Lines").Array;
        /// </code>
        /// result:
        /// <para><c>object[] Lines;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyFieldBuilder Array
        {
            get
            {
                if (string.IsNullOrEmpty(_myField.FieldTypeName))
                {
                    throw new ArgumentNullException("Propery type is null");
                }
                _myField.FieldTypeName += "[]";
                return this;
            }
        }

        /// <summary>
        /// Update type name to List of type.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// var countField = myFieldBuilder.Type("string").Name("Lines").List;
        /// </code>
        /// result:
        /// <para><c> List>string> Lines;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyFieldBuilder List
        {
            get
            {
                if (string.IsNullOrEmpty(_myField.FieldTypeName))
                {
                    throw new ArgumentNullException("Propery type is null");
                }
                _myField.FieldTypeName = $"List<{_myField.FieldTypeName}>";
                return this;
            }
        }

        /// <summary>
        /// Field with static modifier.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Static.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>static string Description;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyFieldBuilder Static
        {
            get
            {
                SetAccessModifier(MyField.AccessModifiers.Static);
                return this;
            }

        }

        /// <summary>
        /// Field with const modifier.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Const.Type("string").Name("Text").Init("\"Value\"").ToString();
        /// </code>
        /// result:
        /// <para><c>const string Text = "Value";</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyFieldBuilder Const
        {
            get
            {
                SetAccessModifier(MyField.AccessModifiers.Const);
                return this;
            }

        }

        /// <summary>
        /// Field with protected modifier.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Protected.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>protected string Description;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyFieldBuilder Protected
        {
            get
            {
                SetAccessModifier(MyField.AccessModifiers.Protected);
                return this;
            }

        }

        internal void SetAccessModifier(MyField.AccessModifiers modifier)
        {
            CheckAccessModifier(modifier);
            _myField.AccessModifiersList.Add(modifier);
        }

        private void CheckAccessConflict(MyField.AccessModifiers newModifier)
        {
            MyField.AccessModifiers? classAccess = _myField.AccessModifiersList
                .FirstOrDefault(o => o == MyField.AccessModifiers.Private
                       || o == MyField.AccessModifiers.Internal
                       || o == MyField.AccessModifiers.Public);

            if (classAccess != null && classAccess != newModifier)
            {
                if (newModifier == MyField.AccessModifiers.Private
                       || newModifier == MyField.AccessModifiers.Internal
                       || newModifier == MyField.AccessModifiers.Public)
                {
                    throw new ArgumentException($"Class already contains access modifier '{classAccess}'. Failed to add '{newModifier}'");
                }
            }

            if (newModifier == MyField.AccessModifiers.Protected)
            {
                if (_myField.AccessModifiersList.Any(o => o == MyField.AccessModifiers.Public))
                {
                    throw new ArgumentException($"Class already contains access modifier 'public' and can`t contain '{newModifier}'");
                }
            }
        }

        public MyFieldBuilder AddComment(string commentLine)
        {
            _myField.Comments.Add(commentLine);
            return this;
        }
        public MyFieldBuilder AddComments(IEnumerable<string> commentLines)
        {
            foreach (var commentLine in commentLines)
            {
                _myField.Comments.Add(commentLine);
            }
            return this;
        }

        public MyFieldBuilder AddAttribute(string attributeLine)
        {
            _myField.Attributes.Add(attributeLine);
            return this;
        }
        public MyFieldBuilder AddAttributes(IEnumerable<string> attributeLines)
        {
            foreach (var attributeLine in attributeLines)
            {
                _myField.Attributes.Add(attributeLine);
            }
            return this;
        }

        /// <summary>
        /// Set initial expression from string.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Type("string").Name("Value").Init("\"Test\"").ToString();
        /// </code>
        /// result:
        /// <para><c>string Value = "Test";</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyFieldBuilder Init(string expression)
        {
            _myField.InitialExpression = $"{expression}";
            return this;
        }

        /// <summary>
        /// Set initial expression from string.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Type("string").Name("Value").InitString("Test").ToString();
        /// </code>
        /// result:
        /// <para><c>string Value = "Test";</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyFieldBuilder InitString(string expression)
        {
            _myField.InitialExpression = $"\"{expression}\"";
            return this;
        }

        /// <summary>
        /// Set initial expression from int.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Type("int").Name("Value").Init(2).ToString();
        /// </code>
        /// result:
        /// <para><c>int Value = 2;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyFieldBuilder Init(int expression)
        {
            _myField.InitialExpression = expression.ToString();
            return this;
        }

        /// <summary>
        /// Set initial expression from double.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Type("double").Name("Value").Init(2.3).ToString();
        /// </code>
        /// result:
        /// <para><c>double Value = 2.3;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyFieldBuilder Init(double expression)
        {
            _myField.InitialExpression = expression.ToString();
            return this;
        }
        
        /// <summary>
        /// Set initial expression from decimal.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Type("decimal").Name("Value").Init(2.4M).ToString();
        /// </code>
        /// result:
        /// <para><c>deciaml Value = 2.4M;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyFieldBuilder Init(decimal expression)
        {
            _myField.InitialExpression = expression.ToString();
            return this;
        }

        /// <summary>
        /// Set initial expression from float.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Type("float").Name("Value").Init(2.001f).ToString();
        /// </code>
        /// result:
        /// <para><c>float Value = 2.001f;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyFieldBuilder Init(float expression)
        {
            _myField.InitialExpression = expression.ToString();
            return this;
        }

        /// <summary>
        /// Set initial expression from boolean.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Type("bool").Name("Value").Init(true).ToString();
        /// </code>
        /// result:
        /// <para><c>bool Value = true;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyFieldBuilder Init(bool expression)
        {
            _myField.InitialExpression = expression ? "true" : "false";
            return this;
        }

        /// <summary>
        /// Set initial expression from ExpressionBuilder.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Type("float").Name("Value").Init(2.001f).ToString();
        /// </code>
        /// result:
        /// <para><c>float Value = 2.001f;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyFieldBuilder Init(MyCodeExpressionBuilder expressionBuilder)
        {
            _myField.InitialExpression = expressionBuilder.ToString();
            return this;
        }

        public override string? ToString()
        {
            return ToString(new MyFieldWriter());
        }

        public string? ToString(ICodeWriter<MyField> formatter)
        {
            return formatter?.WriteCode(Build());
        }
    }
}
