using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder.CodeExpressions
{
    public class IfExpressionBuilder: IExpressionBuilder//<T> where T : IExpressionBuilder
    {
        MyCodeExpression _myCode = new();
        internal readonly MyCodeExpressionBuilder ParentExpressionBuilder;
        internal string Tabs = string.Empty;
        public MyCodeExpression Build()
        {
            return _myCode;
        }

        public IfExpressionBuilder(MyCodeExpressionBuilder source, MyCodeExpression myCodeExpression)
        {
            ParentExpressionBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }

        public IfConditionExpressionBuilder Condition(string condition)
        {
            _myCode.OpenBracket.Add(condition);
            IfConditionExpressionBuilder builder = new IfConditionExpressionBuilder(this, _myCode);
            return builder;
        }

    }

    
}
