using SourceCodeBuilder.CodeExpressions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public class MyCodeExpressionBuilder: IExpressionBuilder
    {
        MyCodeExpression _myCode = new();
        internal string Tabs = string.Empty;
        internal readonly MyCodeExpressionBuilder ParentBuilder;

        public MyCodeExpressionBuilder(string? tabs = null)
        {
            if(tabs != null)
            {
                Tabs = tabs;
            }
        }

        public MyCodeExpression Build()
        {
            return _myCode;
        }

        public MyCodeExpressionBuilder(MyCodeExpressionBuilder source, MyCodeExpression myCodeExpression)
        {
            ParentBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }

        public static MyCodeExpressionBuilder Start(string? tabs = null)
        {
            return new MyCodeExpressionBuilder(tabs);
        }

        public static MyCodeExpressionBuilder Start(int tabsCount)
        {
            StringBuilder tagBuilder = new ();
            for (int i = 0; i < tabsCount; i++)
            {
                tagBuilder.Append(MyCodeExpression.TabExpression.StringExpression);
            }
            return new MyCodeExpressionBuilder(tagBuilder.ToString());
        }

        public IfExpressionBuilder If_
        {
            get
            {
                _myCode.Add(Tabs).If._.This();
                return new IfExpressionBuilder(this, _myCode);
            }
        }

        public IfConditionExpressionBuilder If(string condition)
        {
           
            _myCode.Add(Tabs).If._.This();
            _myCode.OpenBracket.Add(condition);
            IfExpressionBuilder ifExpressionBuilder = new IfExpressionBuilder(this, _myCode);
            IfConditionExpressionBuilder builder = new IfConditionExpressionBuilder(ifExpressionBuilder, _myCode);
            return builder;
            
        }

        public CaseExpressionBuilder Switch(string variable)
        {
            _myCode.Add(Tabs).Switch._.This();
            _myCode.OpenBracket.Add(variable).CloseBracket
                .NewLine.StartCodeBlock.This();
            CaseExpressionBuilder caseExpressionBuilder = new CaseExpressionBuilder(this, _myCode);
            return caseExpressionBuilder;
        }

        public CycleExpressionBuilder For(string forExpression)
        {
            _myCode.Add(Tabs)
                .For._.OpenBracket.Add(forExpression).CloseBracket
                .NewLine.StartCodeBlock.This();
            CycleExpressionBuilder caseExpressionBuilder = new CycleExpressionBuilder(this, _myCode);
            return caseExpressionBuilder;
        }

        public CycleExpressionBuilder Foreach(string forExpression)
        {
            _myCode.Add(Tabs)
                .Foreach._.OpenBracket.Add(forExpression).CloseBracket
                .NewLine.StartCodeBlock.This();
            CycleExpressionBuilder caseExpressionBuilder = new CycleExpressionBuilder(this, _myCode);
            return caseExpressionBuilder;
        }

        public CycleExpressionBuilder Foreach(string type, string name, string iEnumerable)
        {
            _myCode.Add(Tabs)
                .Foreach._
                    .OpenBracket
                        .Add(type)._
                        .Add(name)._
                        .Add("in")._
                        .Add(iEnumerable)
                    .CloseBracket
                .NewLine.StartCodeBlock.This();
            CycleExpressionBuilder caseExpressionBuilder = new CycleExpressionBuilder(this, _myCode);
            return caseExpressionBuilder;
        }

        public CycleExpressionBuilder While(string whileExpression)
        {
            _myCode.Add(Tabs)
                .While._.OpenBracket.Add(whileExpression).CloseBracket
                .NewLine.StartCodeBlock.This();
            CycleExpressionBuilder caseExpressionBuilder = new CycleExpressionBuilder(this, _myCode);
            return caseExpressionBuilder;
        }

        public TryExpressionBuilder Try
        {
            get
            {
                _myCode.Add(Tabs).Add("try")
                    .NewLine.Add(Tabs).StartCodeBlock.This();
                return new TryExpressionBuilder(this, _myCode);
            }
        }

        public DoCycleExpressionBuilder Do
        {
            get
            {
                _myCode.Add(Tabs).Add("do")
                    .NewLine.Add(Tabs).StartCodeBlock.This();
                return new DoCycleExpressionBuilder(this, _myCode);
            }
        }

        public MyCodeExpressionBuilder NewLine => Add(MyCodeExpression.NewLineExpression);
        public MyCodeExpressionBuilder CodeBlock => Add(MyCodeExpression.StartCodeBlockExpression);
        public MyCodeExpressionBuilder FinishCodeBlock => Add(MyCodeExpression.FinishCodeBlockExpression);
        public MyCodeExpressionBuilder _ => Add(MyCodeExpression.SpaceExpression);
        public MyCodeExpressionBuilder Tab => Add(MyCodeExpression.TabExpression);
        public MyCodeExpressionBuilder And => Add(MyCodeExpression.AndAndExpression);
        public MyCodeExpressionBuilder AndAnd => Add(MyCodeExpression.AndAndExpression);
        public MyCodeExpressionBuilder CloseBracket => Add(MyCodeExpression.CloseBracketExpression);
        public MyCodeExpressionBuilder Equals => Add(MyCodeExpression.EqualsExpression);
        public MyCodeExpressionBuilder OpenBracket => Add(MyCodeExpression.OpenBracketExpression);
        public MyCodeExpressionBuilder Or => Add(MyCodeExpression.OrExpression);
        public MyCodeExpressionBuilder OrOr => Add(MyCodeExpression.OrOrExpression);
        public MyCodeExpressionBuilder Set => Add(MyCodeExpression.SetExpression);

        public MyCodeExpressionBuilder Plus => Add(MyCodeExpression.PlusExpression);
        public MyCodeExpressionBuilder Minus => Add(MyCodeExpression.MinusExpression);
        public MyCodeExpressionBuilder Multiply => Add(MyCodeExpression.MultiplyExpression);
        public MyCodeExpressionBuilder Divide => Add(MyCodeExpression.DivideExpression);

        public MyCodeExpressionBuilder Finish => Add(MyCodeExpression.FinishLineExpression);
        public MyCodeExpressionBuilder Dot => Add(MyCodeExpression.DotExpression);

        public MyCodeExpressionBuilder Add(string expression)
        {
            _myCode.Add(expression);
            return this;
        }

        public MyCodeExpressionBuilder Add(int expression)
        {
            _myCode.Add(expression.ToString());
            return this;
        }

        public MyCodeExpressionBuilder Add(double expression)
        {
            _myCode.Add(expression.ToString());
            return this;
        }

        public MyCodeExpressionBuilder Add(decimal expression)
        {
            _myCode.Add(expression.ToString());
            return this;
        }

        public MyCodeExpressionBuilder Add(float expression)
        {
            _myCode.Add(expression.ToString());
            return this;
        }

        public MyCodeExpressionBuilder Add(bool expression)
        {
            _myCode.Add(expression ? "true" : "false");
            return this;
        }

        public MyCodeExpressionBuilder Add(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }
        public MyCodeExpressionBuilder Add(MyCodeExpressionBuilder expressionBuilder)
        {
            _myCode.Add(expressionBuilder);
            return this;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            _myCode.BuildCode(stringBuilder);
            return stringBuilder.ToString();
        }
    }
}
