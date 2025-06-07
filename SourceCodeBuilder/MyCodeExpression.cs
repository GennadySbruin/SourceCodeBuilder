using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        internal void BuildCode(StringBuilder builder, string _defaultTabs = "")
        {
            if (!string.IsNullOrEmpty(StringExpression))
            {
                if (StringExpression == Environment.NewLine)
                {
                    builder.Append(StringExpression);
                }
                else
                {
                    builder.Append(StringExpression.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs));
                }
                    
            }
            foreach (var expression in Expressions) 
            { 
                expression.BuildCode(builder, _defaultTabs);
            }
        }

        internal void BuildCode(TextWriter writer, string _defaultTabs = "", bool forComments = false)
        {
            if (!string.IsNullOrEmpty(StringExpression))
            {
                if(StringExpression == Environment.NewLine)
                {
                    writer.Write(StringExpression + _defaultTabs);
                }
                else
                {
                    if (forComments)
                    {
                        writer.Write(StringExpression.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs).Replace("<",">"));
                    }
                    else
                    {
                        writer.Write(StringExpression.Replace(Environment.NewLine, Environment.NewLine + _defaultTabs));
                    }
                }
                
            }
            
            foreach (var expression in Expressions)
            {
                expression.BuildCode(writer, _defaultTabs);
            }
        }

        /// <summary>
        /// Expression '+'
        /// <example>
        /// <code>
        /// +
        /// int i = 2 + 2;
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression PlusExpression { get; set; } = new MyCodeExpression("+");

        /// <summary>
        /// Expression '-'
        /// <example>
        /// <code>
        /// -
        /// int i = 2 - 2;
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression MinusExpression { get; set; } = new MyCodeExpression("-");

        /// <summary>
        /// Expression '*'
        /// <example>
        /// <code>
        /// *
        /// int i = 2 * 2;
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression MultiplyExpression { get; set; } = new MyCodeExpression("*");

        /// <summary>
        /// Expression '/'
        /// <example>
        /// <code>
        /// /
        /// double d = 2 / 20;
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression DivideExpression { get; set; } = new MyCodeExpression("/");
        
        /// <summary>
        /// Expression '='
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
        public static MyCodeExpression EqualsExpression { get; set; } = new MyCodeExpression("==");

        /// <summary>
        /// Expression '&'
        /// <example>
        /// <code>
        /// &
        /// if (bool1 & bool2);
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression AndExpression { get; set; } = new MyCodeExpression("&");

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
        /// Expression 'switch'
        /// <example>
        /// <code>
        /// switch (variable) 
        /// {
        ///     case x : ....
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression SwitchExpression { get; set; } = new MyCodeExpression("switch");

        /// <summary>
        /// Expression 'case'
        /// <example>
        /// <code>
        /// switch (variable) 
        /// {
        ///     case x : ....
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression CaseExpression { get; set; } = new MyCodeExpression("case");

        /// <summary>
        /// Expression 'default:'
        /// <example>
        /// <code>
        /// switch (variable) 
        /// {
        ///     case x : ....
        ///     default:
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression SwitchDefaultExpression { get; set; } = new MyCodeExpression("default:");

        /// <summary>
        /// Expression 'for'
        /// <example>
        /// <code>
        /// for (...) 
        /// {
        ///     ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression ForExpression { get; set; } = new MyCodeExpression("for");

        /// <summary>
        /// Expression 'foreach'
        /// <example>
        /// <code>
        /// foreach (...) 
        /// {
        ///     ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression ForeachExpression { get; set; } = new MyCodeExpression("foreach");

        /// <summary>
        /// Expression 'while'
        /// <example>
        /// <code>
        /// while (...) 
        /// {
        ///     ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression WhileExpression { get; set; } = new MyCodeExpression("while");

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
        /// Expression 'catch'
        /// <example>
        /// <code>
        /// catch (Exception ex)
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression CatchConditionExpression { get; set; } = new MyCodeExpression("catch");

        /// <summary>
        /// Expression 'try'
        /// <example>
        /// <code>
        /// try { }
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression TryConditionExpression { get; set; } = new MyCodeExpression("try");

        /// <summary>
        /// Expression 'finaly'
        /// <example>
        /// <code>
        /// finaly { }
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression FinalyConditionExpression { get; set; } = new MyCodeExpression("finaly");

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
        public static MyCodeExpression NewLineExpression { get; set; } = new MyCodeExpression(Environment.NewLine);

        /// <summary>
        /// Space expression ' '
        /// <example>
        /// <code>
        ///  ...
        /// 
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression SpaceExpression { get; set; } = new MyCodeExpression(" ");

        /// <summary>
        /// Tab expression '  '
        /// <example>
        /// <code>
        ///   ...
        /// 
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression Tab2Expression { get; set; } = new MyCodeExpression("  ");

        /// <summary>
        /// Tab expression '    '
        /// <example>
        /// <code>
        ///     ...
        /// 
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression TabExpression { get; set; } = new MyCodeExpression("    ");

        /// <summary>
        /// Finish line expression ';'
        /// <example>
        /// <code>
        /// ;
        /// ...;
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression FinishLineExpression { get; set; } = new MyCodeExpression(";");

        /// <summary>
        /// Dot '.'
        /// <example>
        /// <code>
        /// .
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression DotExpression { get; set; } = new MyCodeExpression(".");

        /// <summary>
        /// Comma ','
        /// <example>
        /// <code>
        /// ,
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression CommaExpression { get; set; } = new MyCodeExpression(",");

        /// <summary>
        /// DoubleQuote '"'
        /// <example>
        /// <code>
        /// "
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression DoubleQuoteExpression { get; set; } = new MyCodeExpression("\"");

        /// <summary>
        /// Quote '''
        /// <example>
        /// <code>
        /// '
        /// </code>
        /// </example>
        /// </summary>
        public static MyCodeExpression QuoteExpression { get; set; } = new MyCodeExpression("'");


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

        internal MyCodeExpression Add(MyField expression)
        {
            Add(expression.ToString());
            return this;
        }
        internal MyCodeExpression Add(MyFieldBuilder expressionBuilder)
        {
            Add(expressionBuilder.Build());
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
        internal MyCodeExpression Switch => Add(SwitchExpression);

        /// <summary>
        /// Expression 'case'
        /// <example>
        /// <code>
        /// switch (variable) 
        /// {
        ///     case x : ....
        /// }
        /// </code>
        /// </example>
        /// </summary>
        internal MyCodeExpression Case => Add(CaseExpression);

        /// <summary>
        /// Expression 'default:'
        /// <example>
        /// <code>
        /// switch (variable) 
        /// {
        ///     case x : ....
        ///     default:
        /// }
        /// </code>
        /// </example>
        /// </summary>
        internal MyCodeExpression SwitchDefault => Add(SwitchDefaultExpression);
        internal MyCodeExpression For => Add(ForExpression);
        internal MyCodeExpression Foreach => Add(ForeachExpression);
        internal MyCodeExpression While => Add(WhileExpression);
        internal MyCodeExpression OpenBracket => Add(OpenBracketExpression);
        internal MyCodeExpression CloseBracket => Add(CloseBracketExpression);
        internal MyCodeExpression Else => Add(ElseConditionExpression);
        internal MyCodeExpression Try => Add(TryConditionExpression);
        internal MyCodeExpression Catch => Add(CatchConditionExpression);
        internal MyCodeExpression Finaly => Add(FinalyConditionExpression);
        internal MyCodeExpression StartCodeBlock => Add(StartCodeBlockExpression);
        internal MyCodeExpression FinishCodeBlock => Add(FinishCodeBlockExpression);
        internal MyCodeExpression NewLine => Add(NewLineExpression);
        internal MyCodeExpression _ => Add(SpaceExpression);
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

        internal MyCodeExpression Finish => Add(FinishLineExpression);
        internal MyCodeExpression Dot => Add(DotExpression);
        internal MyCodeExpression This() => this;


        private MyCodeExpression AddTabs(int count)
        {
            for(int i = 0; i< count; i++)
            {
                Add(TabExpression);
            }
            return this;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            BuildCode(stringBuilder);
            return stringBuilder.ToString();
        }

        internal bool HasCode()
        {
            if (!string.IsNullOrEmpty(StringExpression))
            {
                return true;
            }

            foreach (MyCodeExpression e in Expressions ?? [])
            {
                if (e.HasCode())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
