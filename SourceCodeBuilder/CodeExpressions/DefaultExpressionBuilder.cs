using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace SourceCodeBuilder.CodeExpressions
{
    public class DefaultExpressionBuilder
    {
        MyCodeExpression _myCode = new();
        internal readonly CaseExpressionBuilder ParentExpressionBuilder;
        internal string Tabs = string.Empty;
        public MyCodeExpression Build()
        {
            return _myCode;
        }

        public DefaultExpressionBuilder(CaseExpressionBuilder source, MyCodeExpression myCodeExpression)
        {
            ParentExpressionBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }

        public DefaultExpressionBuilder Break
        {
            get
            {
                _myCode.NewLine.Add(Tabs).Tab2.Add("break;");
                return this;
            }
        }

        public MyCodeExpressionBuilder EndSwitch
        {
            get
            {
                _myCode.NewLine.Add(Tabs).FinishCodeBlock.This();
                return ParentExpressionBuilder.ParentExpressionBuilder;
            }
        }

        public DefaultExpressionBuilder AddLine(string codeLine)
        {
            _myCode.NewLine.Add(Tabs).Tab2.Add(codeLine);
            return this;
        }
        public DefaultExpressionBuilder AddCode(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }
        public DefaultExpressionBuilder AddCode(MyCodeExpressionBuilder expressionBuilder)
        {
            _myCode.Add(expressionBuilder);
            return this;
        }
        public DefaultExpressionBuilder AddVariable(MyField field)
        {
            _myCode.NewLine.Add(Tabs).Tab2.Add(field);
            return this;
        }
        public DefaultExpressionBuilder AddVariable(MyFieldBuilder fieldBuilder)
        {
            _myCode.NewLine.Add(Tabs).Tab2.Add(fieldBuilder);
            return this;
        }
        public DefaultExpressionBuilder AddLines(IEnumerable<string> codeLines)
        {
            foreach (var codeLine in codeLines)
            {
                _myCode.NewLine.Add(Tabs).Tab2.Add(codeLine);
            }
            return this;
        }
        public DefaultExpressionBuilder AddCodes(IEnumerable<MyCodeExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                _myCode.Add(expression);
            }
            return this;
        }
        public DefaultExpressionBuilder AddCodes(IEnumerable<MyCodeExpressionBuilder> expressionBuilders)
        {
            foreach (var expression in expressionBuilders)
            {
                _myCode.Add(expression);
            }
            return this;
        }
        public DefaultExpressionBuilder AddVariables(IEnumerable<MyField> fields)
        {
            foreach (var field in fields)
            {
                _myCode.NewLine.Add(Tabs).Tab2.Add(field);
            }
            return this;
        }
        public DefaultExpressionBuilder AddVariables(IEnumerable<MyFieldBuilder> fieldBuilders)
        {
            foreach (var fieldBuilder in fieldBuilders)
            {
                _myCode.NewLine.Add(Tabs).Tab2.Add(fieldBuilder);
            }
            return this;
        }
    }
}
