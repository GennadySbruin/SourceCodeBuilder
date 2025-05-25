using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder.CodeExpressions
{
    public class MethodCodeBlockExpressionBuilder : IExpressionBuilder
    {
        MyCodeExpression _myCode;
        internal readonly MyClassBuilder ClassBuilder;
        internal string Tabs = string.Empty;
        public MyCodeExpression Build()
        {
            return _myCode;
        }

        public MethodCodeBlockExpressionBuilder(MyClassBuilder source, MyCodeExpression myCodeExpression)
        {
            ClassBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }

        public MethodCodeBlockExpressionBuilder AddLine(string codeLine)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(codeLine);
            return this;
        }

        public MethodCodeBlockExpressionBuilder AddCode(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }

        public MyClassBuilder EndMethod
        {
            get
            {
                _myCode.NewLine.Add(Tabs).FinishCodeBlock.This();
                return ClassBuilder;
            }
        }
    }

    
}
