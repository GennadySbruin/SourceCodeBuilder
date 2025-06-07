using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder.CodeExpressions
{
    /// <summary>
    /// If condition statement builder
    /// </summary>
    public class IfConditionExpressionBuilder : IExpressionBuilder
    {
        MyCodeExpression _myCode;
        internal readonly IfExpressionBuilder IfExpressionBuilder;
        internal string Tabs = string.Empty;

        public IfConditionExpressionBuilder(IfExpressionBuilder source, MyCodeExpression ifExpression)
        {
            _myCode = ifExpression;
            IfExpressionBuilder = source;
            Tabs += source.Tabs;
        }

        /// <summary>
        /// Add 'and' condition to if statement
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyCodeExpressionBuilder.Start()
        ///   .If(...).And("expression")
        /// </code>
        /// result:
        /// <code> 
        /// if ( ... and expression)
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="condition"></param>
        public IfConditionExpressionBuilder And(string condition)
        {
            _myCode._.And._.Add(condition);
            return this;
        }

        /// <summary>
        /// Add double 'and' condition to if statement
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyCodeExpressionBuilder.Start()
        ///   .If(...).And2("expression")
        /// </code>
        /// result:
        /// <code> 
        /// if ( ... and_and expression)
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="condition"></param>
        public IfConditionExpressionBuilder And2(string condition)
        {
            _myCode._.AndAnd._.Add(condition);
            return this;
        }

        /// <summary>
        /// Add | condition to if statement
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyCodeExpressionBuilder.Start()
        ///   .If(...).Or("expression")
        /// </code>
        /// result:
        /// <code> 
        /// if ( ... | expression)
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="condition"></param>
        public IfConditionExpressionBuilder Or(string condition)
        {
            _myCode._.Or._.Add(condition);
            return this;
        }

        /// <summary>
        /// Add || condition to if statement
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyCodeExpressionBuilder.Start()
        ///   .If(...).Or2("expression")
        /// </code>
        /// result:
        /// <code> 
        /// if ( ... || expression)
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="condition"></param>
        public IfConditionExpressionBuilder Or2(string condition)
        {
            _myCode._.OrOr._.Add(condition);
            return this;
        }

        /// <summary>
        /// Start code block for if statement
        /// <example>
        /// <para>Example:</para>
        /// <code>
        /// MyCodeExpressionBuilder.Start()
        ///   .If(...)
        ///   .CodeBlock
        /// </code>
        /// result:
        /// <code> 
        /// if ( ... )
        /// {
        ///    ...
        /// </code>
        /// </example>
        /// </summary>

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
