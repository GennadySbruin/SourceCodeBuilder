using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder.CodeExpressions
{
    /// <summary>
    /// Base if statement builder
    /// </summary>
    public class IfExpressionBuilder: IExpressionBuilder//<T> where T : IExpressionBuilder
    {
        MyCodeExpression _myCode = new();
        internal readonly MyCodeExpressionBuilder ParentExpressionBuilder;
        internal string Tabs = string.Empty;

        public IfExpressionBuilder(MyCodeExpressionBuilder source, MyCodeExpression myCodeExpression)
        {
            ParentExpressionBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }

        /// <summary>
        /// Add condition to if statement
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyCodeExpressionBuilder.Start()
        ///   .If("value == 1")
        /// </code>
        /// result:
        /// <code> 
        /// if (value == 1)
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="condition"></param>
        public IfConditionExpressionBuilder Condition(string condition)
        {
            _myCode.OpenBracket.Add(condition);
            IfConditionExpressionBuilder builder = new IfConditionExpressionBuilder(this, _myCode);
            return builder;
        }

    }

    
}
