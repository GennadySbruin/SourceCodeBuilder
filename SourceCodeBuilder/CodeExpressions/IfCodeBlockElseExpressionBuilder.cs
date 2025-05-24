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
