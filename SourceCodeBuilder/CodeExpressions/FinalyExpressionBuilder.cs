using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace SourceCodeBuilder.CodeExpressions
{
    /// <summary>
    /// Try finaly statement builder
    /// </summary>
    public class FinalyExpressionBuilder
    {
        MyCodeExpression _myCode = new();
        internal readonly TryExpressionBuilder ParentExpressionBuilder;
        internal string Tabs = string.Empty;

        public FinalyExpressionBuilder(TryExpressionBuilder source, MyCodeExpression myCodeExpression)
        {
            ParentExpressionBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }

        /// <summary>
        /// Close finaly statement with expression '}'
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .Try
        ///   .Finaly
        ///   ...
        ///   .EndTry
        /// </code>
        /// result:
        /// <code>
        /// try 
        /// {
        ///     ...
        /// }
        /// finaly
        /// {
        ///     ...
        /// }   
        /// </code>
        /// </example>
        /// </summary>

        public MyCodeExpressionBuilder EndTry
        {
            get
            {
                _myCode.NewLine.Add(Tabs).FinishCodeBlock.This();
                return ParentExpressionBuilder.ParentExpressionBuilder;
            }
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="codeLine"></param>
        /// <returns></returns>
        public FinalyExpressionBuilder AddLine(string codeLine)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(codeLine);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public FinalyExpressionBuilder AddCode(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expressionBuilder"></param>
        /// <returns></returns>
        public FinalyExpressionBuilder AddCode(MyCodeExpressionBuilder expressionBuilder)
        {
            _myCode.Add(expressionBuilder);
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public FinalyExpressionBuilder AddVariable(MyField field)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(field);
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="fieldBuilder"></param>
        /// <returns></returns>
        public FinalyExpressionBuilder AddVariable(MyFieldBuilder fieldBuilder)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="codeLines"></param>
        /// <returns></returns>
        public FinalyExpressionBuilder AddLines(IEnumerable<string> codeLines)
        {
            foreach (var codeLine in codeLines)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(codeLine);
            }
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public FinalyExpressionBuilder AddCodes(IEnumerable<MyCodeExpression> expressions)
        {
            foreach (var expression in expressions)
            {
                _myCode.Add(expression);
            }
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expressionBuilders"></param>
        /// <returns></returns>
        public FinalyExpressionBuilder AddCodes(IEnumerable<MyCodeExpressionBuilder> expressionBuilders)
        {
            foreach (var expression in expressionBuilders)
            {
                _myCode.Add(expression);
            }
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="fields"></param>
        /// <returns></returns>
        public FinalyExpressionBuilder AddVariables(IEnumerable<MyField> fields)
        {
            foreach (var field in fields)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(field);
            }
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="fieldBuilders"></param>
        /// <returns></returns>
        public FinalyExpressionBuilder AddVariables(IEnumerable<MyFieldBuilder> fieldBuilders)
        {
            foreach (var fieldBuilder in fieldBuilders)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            }
            return this;
        }
    }
}
