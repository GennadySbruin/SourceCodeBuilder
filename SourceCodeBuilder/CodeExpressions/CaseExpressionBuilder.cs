using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace SourceCodeBuilder.CodeExpressions
{
    public class CaseExpressionBuilder
    {
        MyCodeExpression _myCode = new();
        internal readonly MyCodeExpressionBuilder ParentExpressionBuilder;
        internal string Tabs = string.Empty;
        public MyCodeExpression Build()
        {
            return _myCode;
        }

        public CaseExpressionBuilder(MyCodeExpressionBuilder source, MyCodeExpression myCodeExpression)
        {
            ParentExpressionBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }
        public DefaultExpressionBuilder Default
        {
            get
            {
                _myCode
                    .NewLine.Add(Tabs).Tab.SwitchDefault.This();
                return new DefaultExpressionBuilder(this, _myCode);
            }
        }

        public CaseExpressionBuilder Case(string condition)
        {
            _myCode
                .NewLine.Add(Tabs).Tab.Case._.Add($"{condition}:");
            return this;
        }

        public MyCodeExpressionBuilder EndSwitch
        {
            get
            {
                _myCode.NewLine.Add(Tabs).FinishCodeBlock.This();
                return ParentExpressionBuilder;
            }
        }

        public CaseExpressionBuilder Break
        {
            get
            {
                _myCode.NewLine.Add(Tabs).Tab2.Add("break;")
                       .NewLine.This();
                return this;
            }
        }

        public CaseExpressionBuilder AddLine(string codeLine)
        {
            _myCode.NewLine.Add(Tabs).Tab2.Add(codeLine);
            return this;
        }
        public CaseExpressionBuilder AddCode(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }
        public CaseExpressionBuilder AddCode(MyCodeExpressionBuilder expressionBuilder)
        {
            _myCode.Add(expressionBuilder);
            return this;
        }
        public CaseExpressionBuilder AddVariable(MyField field)
        {
            _myCode.NewLine.Add(Tabs).Tab2.Add(field);
            return this;
        }
        public CaseExpressionBuilder AddVariable(MyFieldBuilder fieldBuilder)
        {
            _myCode.NewLine.Add(Tabs).Tab2.Add(fieldBuilder);
            return this;
        }
        public CaseExpressionBuilder AddLines(IEnumerable<string> codeLines)
        {
            foreach (var codeLine in codeLines)
            {
                _myCode.NewLine.Add(Tabs).Tab2.Add(codeLine);
            }
            return this;
        }
        public CaseExpressionBuilder AddCodes(IEnumerable<MyCodeExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                _myCode.Add(expression);
            }
            return this;
        }
        public CaseExpressionBuilder AddCodes(IEnumerable<MyCodeExpressionBuilder> expressionBuilders)
        {
            foreach (var expression in expressionBuilders)
            {
                _myCode.Add(expression);
            }
            return this;
        }
        public CaseExpressionBuilder AddVariables(IEnumerable<MyField> fields)
        {
            foreach (var field in fields)
            {
                _myCode.NewLine.Add(Tabs).Tab2.Add(field);
            }
            return this;
        }
        public CaseExpressionBuilder AddVariables(IEnumerable<MyFieldBuilder> fieldBuilders)
        {
            foreach (var fieldBuilder in fieldBuilders)
            {
                _myCode.NewLine.Add(Tabs).Tab2.Add(fieldBuilder);
            }
            return this;
        }
    }
}
