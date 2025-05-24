using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace SourceCodeBuilder
{
    public class MyCodeExpression
    {
        public string StringExpression { get; set; }
        public List<MyCodeExpression> Expressions { get; set; } = [];

        public MyCodeExpression() { }
        public MyCodeExpression(string stringExpression)
        {
            StringExpression = stringExpression;
        }

        internal void BuildCode(StringBuilder builder)
        {
            builder.Append(StringExpression);
            foreach (var expression in Expressions) 
            { 
                expression.BuildCode(builder);
            }
        }

        /// <summary>
        /// Expression with spaces '='
        /// <example>
        /// <code>
        /// =
        /// int i = 0;
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression SetExpression { get; set; } = new MyCodeExpression("=");

        /// <summary>
        /// Expression '=='
        /// <example>
        /// <code>
        /// ==
        /// i == 0;
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression EqExpression { get; set; } = new MyCodeExpression("==");

        /// <summary>
        /// Expression '&'
        /// <example>
        /// <code>
        /// &
        /// if (bool1 & bool2);
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression EqualsExpression { get; set; } = new MyCodeExpression("&");

        /// <summary>
        /// Expression '&&'
        /// <example>
        /// <code>
        /// &&
        /// if (bool1 && bool2);
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression AndAndExpression { get; set; } = new MyCodeExpression("&&");

        /// <summary>
        /// Expression '|'
        /// <example>
        /// <code>
        /// |
        /// if (bool1 | bool2);
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression OrExpression { get; set; } = new MyCodeExpression("|");

        /// <summary>
        /// Expression '||'
        /// <example>
        /// <code>
        /// ||
        /// if (bool1 || bool2);
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression OrOrExpression { get; set; } = new MyCodeExpression("||");

        /// <summary>
        /// Expression 'if'
        /// <example>
        /// <code>
        /// if ( ...
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression StartIfConditionExpression { get; set; } = new MyCodeExpression("if");

        /// <summary>
        /// Expression '('
        /// <example>
        /// <code>
        /// (...;
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression OpenBracketExpression { get; set; } = new MyCodeExpression("(");

        /// <summary>
        /// Expression ')'
        /// <example>
        /// <code>
        /// ...);
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression CloseBracketExpression { get; set; } = new MyCodeExpression(")");

        /// <summary>
        /// Expression 'else'
        /// <example>
        /// <code>
        ///    ...
        /// }
        /// else
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression ElseConditionExpression { get; set; } = new MyCodeExpression("else");

        /// <summary>
        /// Expression '{'
        /// <example>
        /// <code>
        /// ...
        /// {
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression StartCodeBlockExpression { get; set; } = new MyCodeExpression("{");

        /// <summary>
        /// Expression '}'
        /// <example>
        /// <code>
        ///    ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression FinishCodeBlockExpression { get; set; } = new MyCodeExpression("}");

        /// <summary>
        /// Expression = Environment.NewLine
        /// <example>
        /// <code>
        /// ...
        /// ...
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression NewLineCodeBlockExpression { get; set; } = new MyCodeExpression(Environment.NewLine);

        /// <summary>
        /// Space expression ' '
        /// <example>
        /// <code>
        ///  ...
        /// 
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression SpaceCodeBlockExpression { get; set; } = new MyCodeExpression(" ");

        /// <summary>
        /// Tab expression '  '
        /// <example>
        /// <code>
        ///   ...
        /// 
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression Tab2CodeBlockExpression { get; set; } = new MyCodeExpression("  ");

        /// <summary>
        /// Tab expression '    '
        /// <example>
        /// <code>
        ///     ...
        /// 
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression TabCodeBlockExpression { get; set; } = new MyCodeExpression("    ");

        internal MyCodeExpression Add(MyCodeExpression expression)
        {
            Expressions.Add(expression);
            return this;
        }
        internal MyCodeExpression Add(MyCodeExpressionBuilder expressionBuilder)
        {
            Expressions.Add(expressionBuilder.Build());
            return this;
        }
        internal MyCodeExpression Add(string expression)
        {
            Expressions.Add(new MyCodeExpression(expression));
            return this;
        }

        internal MyCodeExpression Set => Add(SetExpression);
        new internal MyCodeExpression Equals => Add(EqualsExpression);
        internal MyCodeExpression And => Add(AndAndExpression);
        internal MyCodeExpression AndAnd => Add(AndAndExpression);
        internal MyCodeExpression Or => Add(OrExpression);
        internal MyCodeExpression OrOr => Add(OrOrExpression);
        internal MyCodeExpression If => Add(StartIfConditionExpression);
        internal MyCodeExpression OpenBracket => Add(OpenBracketExpression);
        internal MyCodeExpression CloseBracket => Add(CloseBracketExpression);
        internal MyCodeExpression Else => Add(ElseConditionExpression);
        internal MyCodeExpression StartCodeBlock => Add(StartCodeBlockExpression);
        internal MyCodeExpression FinishCodeBlock => Add(FinishCodeBlockExpression);
        internal MyCodeExpression NewLine => Add(NewLineCodeBlockExpression);
        internal MyCodeExpression _ => Add(SpaceCodeBlockExpression);
        internal MyCodeExpression Tab => AddTabs(1);
        internal MyCodeExpression Tab2 => AddTabs(2);
        internal MyCodeExpression Tab3 => AddTabs(3);
        internal MyCodeExpression Tab4 => AddTabs(4);
        internal MyCodeExpression Tab5 => AddTabs(5);
        internal MyCodeExpression Tab6 => AddTabs(6);
        internal MyCodeExpression Tab7 => AddTabs(7);
        internal MyCodeExpression Tab8 => AddTabs(8);
        internal MyCodeExpression Tab9 => AddTabs(9);
        internal MyCodeExpression Tab10 => AddTabs(10);
        internal MyCodeExpression Tab11 => AddTabs(11);
        internal MyCodeExpression Tab12 => AddTabs(12);
        internal MyCodeExpression Tab13 => AddTabs(13);
        internal MyCodeExpression Tab14 => AddTabs(14);
        internal MyCodeExpression Tab15 => AddTabs(15);
        internal MyCodeExpression Tab16 => AddTabs(16);
        internal MyCodeExpression Tab17 => AddTabs(17);
        internal MyCodeExpression Tab18 => AddTabs(18);
        internal MyCodeExpression Tab19 => AddTabs(19);
        internal MyCodeExpression Tab20 => AddTabs(20);
        internal MyCodeExpression This() => this;


        private MyCodeExpression AddTabs(int count)
        {
            for(int i = 0; i< count; i++)
            {
                Add(TabCodeBlockExpression);
            }
            return this;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            BuildCode(stringBuilder);
            return stringBuilder.ToString();
        }
    }
}
