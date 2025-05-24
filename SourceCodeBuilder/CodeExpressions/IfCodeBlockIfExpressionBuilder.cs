using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace SourceCodeBuilder.CodeExpressions
{
    public class IfCodeBlockIfExpressionBuilder : IExpressionBuilder
    {
        MyCodeExpression _myCode;
        internal readonly IfExpressionBuilder IfExpressionBuilder;
        internal string Tabs = string.Empty;
        public MyCodeExpression Build()
        {
            return _myCode;
        }

        public IfCodeBlockIfExpressionBuilder(IfExpressionBuilder source, MyCodeExpression ifExpression)
        {
            _myCode = ifExpression;
            IfExpressionBuilder = source;
            Tabs += source.Tabs;
        }

        public IfCodeBlockIfExpressionBuilder AddLine(string codeLine)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(codeLine);
            return this;
        }

        public IfCodeBlockIfExpressionBuilder AddCode(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }

        public IfCodeBlockIfExpressionBuilder AddCode(MyCodeExpressionBuilder expressionBuilder)
        {
            _myCode.Add(expressionBuilder);
            return this;
        }

        public IfCodeBlockElseExpressionBuilder Else
        {
            get
            {
                _myCode
                    .NewLine.Add(Tabs).FinishCodeBlock
                    .NewLine.Add(Tabs).Else
                    .NewLine.Add(Tabs).StartCodeBlock.This();
                return new IfCodeBlockElseExpressionBuilder(IfExpressionBuilder, _myCode);
            }
        }

        public IfExpressionBuilder ElseIf_
        {
            get
            {
                _myCode
                    .NewLine.Add(Tabs).FinishCodeBlock
                    .NewLine.Add(Tabs).Else._.If._.This();
                return new IfExpressionBuilder(IfExpressionBuilder.ParentExpressionBuilder, _myCode);
            }
        }
        public IfConditionExpressionBuilder ElseIf(string condition)
        {

            _myCode
                .NewLine.Add(Tabs).FinishCodeBlock
                .NewLine.Add(Tabs).Else._.If._.This();
            _myCode.OpenBracket.Add(condition);
            IfConditionExpressionBuilder builder = new IfConditionExpressionBuilder(IfExpressionBuilder, _myCode);
            return builder;
        }

        public MyCodeExpressionBuilder EndIf
        {
            get
            {
                _myCode.NewLine.Add(Tabs).FinishCodeBlock.This();
                return IfExpressionBuilder.ParentExpressionBuilder;
            }
        }
    }

    
}
