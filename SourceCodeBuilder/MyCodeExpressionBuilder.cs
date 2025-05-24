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
                tagBuilder.Append(MyCodeExpression.TabCodeBlockExpression.StringExpression);
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

        public MyCodeExpressionBuilder NewLine => Add(MyCodeExpression.NewLineCodeBlockExpression);

        private MyCodeExpressionBuilder Add(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }
        private MyCodeExpressionBuilder Add(MyCodeExpressionBuilder expressionBuilder)
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
