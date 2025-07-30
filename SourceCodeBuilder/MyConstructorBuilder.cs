using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public class MyConstructorBuilder
    {
        MyConstructor _myConstructor = new();
        public MyConstructor Build()
        {
            return _myConstructor;
        }

        /// <summary>
        /// Method with internal access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyConstructorBuilder.Internal.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>internal string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyConstructorBuilder Internal
        {
            get
            {
                MyConstructorBuilder myMethodBuilder = new MyConstructorBuilder();
                myMethodBuilder.SetAccessModifier(MyConstructor.AccessModifiers.Internal);
                return myMethodBuilder;
            }
        }

        /// <summary>
        /// Method with private access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyConstructorBuilder.Private.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>private string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyConstructorBuilder Private
        {
            get
            {
                MyConstructorBuilder myMethodBuilder = new MyConstructorBuilder();
                myMethodBuilder.SetAccessModifier(MyConstructor.AccessModifiers.Private);
                return myMethodBuilder;
            }
        }

        

        /// <summary>
        /// Method with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyConstructorBuilder.Public.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>public string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyConstructorBuilder Public
        {
            get
            {
                MyConstructorBuilder myMethodBuilder = new MyConstructorBuilder();
                myMethodBuilder.SetAccessModifier(MyConstructor.AccessModifiers.Public);
                return myMethodBuilder;
            }
        }

        
        public virtual void CheckAccessModifier(MyConstructor.AccessModifiers newModifier)
        {
            if (_myConstructor.AccessModifiersList.Contains(newModifier))
            {
                throw new ArgumentException($"Constructor already contains access modifier '{newModifier}'");
            }
            CheckAccessConflict(newModifier);
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
        public MyConstructorBuilder Parameter(string typeName, string name, string defaultValue = null, bool isRef = false, bool isIn = false, bool isOut = false, bool isParams = false)
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
            _myConstructor.AddParameter(stringBuilder.ToString());
            
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
        public MyConstructorBuilder Parameter(string parameter)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentNullException("Parameter");
            }
            _myConstructor.AddParameter(parameter);

            return this;
        }

        /// <summary>
        /// Set base constructor.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public MyConstructorBuilder Base(string baseBody)
        {
            if (string.IsNullOrEmpty(baseBody))
            {
                throw new ArgumentNullException("baseBody");
            }
            _myConstructor.Base = baseBody;

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
        public MyConstructorBuilder AddLine(string methodLine)
        {
            if (string.IsNullOrEmpty(methodLine))
            {
                throw new ArgumentNullException("Method line");
            }
            _myConstructor.MyCode.NewLine.Tab.Add(methodLine);

            return this;
        }

        public MyConstructorBuilder WithGeneratedComments
        {
            get
            {
                _myConstructor.AutoGenerateComments = true; ;
                return this;
            }
        }

        public MyConstructorBuilder AddComment(string commentLine)
        {
            _myConstructor.Comments.Add(commentLine);
            return this;
        }

        public MyConstructorBuilder AddLines(IEnumerable<string> methodLines)
        {
            foreach (var methodLine in methodLines)
            {
                _myConstructor.MyCode.NewLine.Tab.Add(methodLine);
            }
            return this;
        }

        public MyConstructorBuilder AddComments(IEnumerable<string> commentLines)
        {
            foreach (var commentLine in commentLines)
            {
                _myConstructor.Comments.Add(commentLine);
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
        public MyConstructorBuilder AddCode(MyCodeExpression methodExpression)
        {
            _myConstructor.MyCode.Add(methodExpression);

            return this;
        }

        public MyConstructorBuilder AddCodes(IEnumerable<MyCodeExpression> methodExpressions)
        {
            foreach (var methodExpression in methodExpressions)
            {
                _myConstructor.MyCode.Add(methodExpression);
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
        public MyConstructorBuilder AddCode(MyCodeExpressionBuilder methodExpressionBuilder)
        {
            _myConstructor.MyCode.Add(methodExpressionBuilder);

            return this;
        }

        public MyConstructorBuilder AddCodes(IEnumerable<MyCodeExpressionBuilder> methodExpressionBuilders)
        {
            foreach (var methodExpressionBuilder in methodExpressionBuilders)
            {
                _myConstructor.MyCode.Add(methodExpressionBuilder);
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
        public MyConstructorBuilder AddVariable(MyFieldBuilder fieldBuilder)
        {
            _myConstructor.MyCode.NewLine.Tab.Add(fieldBuilder);

            return this;
        }

        public MyConstructorBuilder AddVariables(IEnumerable<MyFieldBuilder> fieldBuilders)
        {
            foreach(var fieldBuilder in fieldBuilders)
            {
                _myConstructor.MyCode.NewLine.Tab.Add(fieldBuilder);
            }
            return this;
        }
                

        /// <summary>
        /// Method with static modifier.
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyConstructorBuilder.Static.Type("string").Name("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>static string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public MyConstructorBuilder Static
        {
            get
            {
                SetAccessModifier(MyConstructor.AccessModifiers.Static);
                return this;
            }

        }

        internal void SetAccessModifier(MyConstructor.AccessModifiers modifier)
        {
            CheckAccessModifier(modifier);
            _myConstructor.AccessModifiersList.Add(modifier);
        }


        private void CheckAccessConflict(MyConstructor.AccessModifiers newModifier)
        {
            MyConstructor.AccessModifiers? classAccess = _myConstructor.AccessModifiersList
                .FirstOrDefault(o => o == MyConstructor.AccessModifiers.Private
                       || o == MyConstructor.AccessModifiers.Internal
                       || o == MyConstructor.AccessModifiers.Public);

            if (classAccess != null && classAccess != newModifier)
            {
                if (newModifier == MyConstructor.AccessModifiers.Private
                       || newModifier == MyConstructor.AccessModifiers.Internal
                       || newModifier == MyConstructor.AccessModifiers.Public)
                {
                    throw new ArgumentException($"Constructor already contains access modifier '{classAccess}'. Failed to add '{newModifier}'");
                }
            }

        }

        /// <summary>
        /// Convert this method to result code string with default formatter.
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            return ToString(new MyConstructorWriter());
        }

        /// <summary>
        /// Convert this method to result code string with assigned formatter.
        /// </summary>
        /// <returns></returns>
        public string? ToString(ICodeWriter<MyConstructor> formatter)
        {
            return formatter?.WriteCode(Build());
        }
    }
}
