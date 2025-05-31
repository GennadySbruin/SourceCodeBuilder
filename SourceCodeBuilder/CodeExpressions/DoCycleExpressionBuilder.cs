using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace SourceCodeBuilder.CodeExpressions
{
    public class DoCycleExpressionBuilder
    {
        MyCodeExpression _myCode = new();
        internal readonly MyCodeExpressionBuilder ParentExpressionBuilder;
        internal string Tabs = string.Empty;
        public MyCodeExpression Build()
        {
            return _myCode;
        }

        public DoCycleExpressionBuilder(MyCodeExpressionBuilder source, MyCodeExpression myCodeExpression)
        {
            ParentExpressionBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }

        public MyCodeExpressionBuilder While(string whileExpression)
        {
            _myCode.NewLine.Add(Tabs).FinishCodeBlock._
                .While._.OpenBracket.Add(whileExpression).CloseBracket.Finish.This();
            return ParentExpressionBuilder;
        }

        public DoCycleExpressionBuilder AddLine(string codeLine)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(codeLine);
            return this;
        }
        public DoCycleExpressionBuilder AddCode(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }
        public DoCycleExpressionBuilder AddCode(MyCodeExpressionBuilder expressionBuilder)
        {
            _myCode.Add(expressionBuilder);
            return this;
        }
        public DoCycleExpressionBuilder AddVariable(MyField field)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(field);
            return this;
        }
        public DoCycleExpressionBuilder AddVariable(MyFieldBuilder fieldBuilder)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            return this;
        }
        public DoCycleExpressionBuilder AddLines(IEnumerable<string> codeLines)
        {
            foreach (var codeLine in codeLines)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(codeLine);
            }
            return this;
        }
        public DoCycleExpressionBuilder AddCodes(IEnumerable<MyCodeExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                _myCode.Add(expression);
            }
            return this;
        }
        public DoCycleExpressionBuilder AddCodes(IEnumerable<MyCodeExpressionBuilder> expressionBuilders)
        {
            foreach (var expression in expressionBuilders)
            {
                _myCode.Add(expression);
            }
            return this;
        }
        public DoCycleExpressionBuilder AddVariables(IEnumerable<MyField> fields)
        {
            foreach (var field in fields)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(field);
            }
            return this;
        }
        public DoCycleExpressionBuilder AddVariables(IEnumerable<MyFieldBuilder> fieldBuilders)
        {
            foreach (var fieldBuilder in fieldBuilders)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            }
            return this;
        }
    }
}
