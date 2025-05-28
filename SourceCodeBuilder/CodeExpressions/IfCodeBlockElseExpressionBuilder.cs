using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder.CodeExpressions
{
    public class IfCodeBlockElseExpressionBuilder : IExpressionBuilder
    {
        MyCodeExpression _myCode;
        internal readonly IfExpressionBuilder IfExpressionBuilder;
        internal string Tabs = string.Empty;
        public MyCodeExpression Build()
        {
            return _myCode;
        }

        public IfCodeBlockElseExpressionBuilder(IfExpressionBuilder source, MyCodeExpression myCodeExpression)
        {
            IfExpressionBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }
        public MyCodeExpressionBuilder EndIf
        {
            get
            {
                _myCode.NewLine.Add(Tabs).FinishCodeBlock.This();
                return IfExpressionBuilder.ParentExpressionBuilder;
            }
        }

        public IfCodeBlockElseExpressionBuilder AddLine(string codeLine)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(codeLine);
            return this;
        }
        public IfCodeBlockElseExpressionBuilder AddCode(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }
        public IfCodeBlockElseExpressionBuilder AddCode(MyCodeExpressionBuilder expressionBuilder)
        {
            _myCode.Add(expressionBuilder);
            return this;
        }
        public IfCodeBlockElseExpressionBuilder AddVariable(MyField field)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(field);
            return this;
        }
        public IfCodeBlockElseExpressionBuilder AddVariable(MyFieldBuilder fieldBuilder)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            return this;
        }
        public IfCodeBlockElseExpressionBuilder AddLines(IEnumerable<string> codeLines)
        {
            foreach (var codeLine in codeLines)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(codeLine);
            }
            return this;
        }
        public IfCodeBlockElseExpressionBuilder AddCodes(IEnumerable<MyCodeExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                _myCode.Add(expression);
            }
            return this;
        }
        public IfCodeBlockElseExpressionBuilder AddCodes(IEnumerable<MyCodeExpressionBuilder> expressionBuilders)
        {
            foreach (var expression in expressionBuilders)
            {
                _myCode.Add(expression);
            }
            return this;
        }
        public IfCodeBlockElseExpressionBuilder AddVariables(IEnumerable<MyField> fields)
        {
            foreach (var field in fields)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(field);
            }
            return this;
        }
        public IfCodeBlockElseExpressionBuilder AddVariables(IEnumerable<MyFieldBuilder> fieldBuilders)
        {
            foreach (var fieldBuilder in fieldBuilders)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            }
            return this;
        }


    }

    
}
