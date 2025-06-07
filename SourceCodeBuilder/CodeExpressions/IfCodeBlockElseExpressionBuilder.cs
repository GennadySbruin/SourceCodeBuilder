using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder.CodeExpressions
{
    /// <summary>
    /// Else statement builder
    /// </summary>
    public class IfCodeBlockElseExpressionBuilder : IExpressionBuilder
    {
        MyCodeExpression _myCode;
        internal readonly IfExpressionBuilder IfExpressionBuilder;
        internal string Tabs = string.Empty;

        public IfCodeBlockElseExpressionBuilder(IfExpressionBuilder source, MyCodeExpression myCodeExpression)
        {
            IfExpressionBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }

        /// <summary>
        /// Close if statement with expression '}'
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .If (...)
        ///   ...
        ///   .EndIf
        /// </code>
        /// result:
        /// <code>
        /// if (...) 
        /// {
        ///     ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public MyCodeExpressionBuilder EndIf
        {
            get
            {
                _myCode.NewLine.Add(Tabs).FinishCodeBlock.This();
                return IfExpressionBuilder.ParentExpressionBuilder;
            }
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="codeLine"></param>
        /// <returns></returns>
        public IfCodeBlockElseExpressionBuilder AddLine(string codeLine)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(codeLine);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IfCodeBlockElseExpressionBuilder AddCode(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expressionBuilder"></param>
        /// <returns></returns>
        public IfCodeBlockElseExpressionBuilder AddCode(MyCodeExpressionBuilder expressionBuilder)
        {
            _myCode.Add(expressionBuilder);
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public IfCodeBlockElseExpressionBuilder AddVariable(MyField field)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(field);
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="fieldBuilder"></param>
        /// <returns></returns>
        public IfCodeBlockElseExpressionBuilder AddVariable(MyFieldBuilder fieldBuilder)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="codeLines"></param>
        /// <returns></returns>
        public IfCodeBlockElseExpressionBuilder AddLines(IEnumerable<string> codeLines)
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
        public IfCodeBlockElseExpressionBuilder AddCodes(IEnumerable<MyCodeExpression> expressions)
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
        public IfCodeBlockElseExpressionBuilder AddCodes(IEnumerable<MyCodeExpressionBuilder> expressionBuilders)
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
        public IfCodeBlockElseExpressionBuilder AddVariables(IEnumerable<MyField> fields)
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
        public IfCodeBlockElseExpressionBuilder AddVariables(IEnumerable<MyFieldBuilder> fieldBuilders)
        {
            foreach (var fieldBuilder in fieldBuilders)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            }
            return this;
        }


    }

    
}
