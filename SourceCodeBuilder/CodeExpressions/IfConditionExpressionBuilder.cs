using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder.CodeExpressions
{
    public class IfConditionExpressionBuilder : IExpressionBuilder
    {
        MyCodeExpression _myCode;
        internal readonly IfExpressionBuilder IfExpressionBuilder;
        internal string Tabs = string.Empty;
        public MyCodeExpression Build()
        {
            return _myCode;
        }

        public IfConditionExpressionBuilder(IfExpressionBuilder source, MyCodeExpression ifExpression)
        {
            _myCode = ifExpression;
            IfExpressionBuilder = source;
            Tabs += source.Tabs;
        }

        public IfConditionExpressionBuilder And(string condition)
        {
            _myCode._.And._.Add(condition);
            return this;
        }

        public IfConditionExpressionBuilder And2(string condition)
        {
            _myCode._.AndAnd._.Add(condition);
            return this;
        }

        public IfConditionExpressionBuilder Or(string condition)
        {
            _myCode._.Or._.Add(condition);
            return this;
        }

        public IfConditionExpressionBuilder Or2(string condition)
        {
            _myCode._.OrOr._.Add(condition);
            return this;
        }

        public IfCodeBlockIfExpressionBuilder CodeBlock
        {
            get
            {
                _myCode.CloseBracket.NewLine.Add(Tabs).StartCodeBlock.This();
                return new IfCodeBlockIfExpressionBuilder(IfExpressionBuilder, _myCode);
            }
        } 
    }
}
