using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace SourceCodeBuilder.CodeExpressions
{
    /// <summary>
    /// If statement builder
    /// </summary>
    public class IfCodeBlockIfExpressionBuilder : IExpressionBuilder
    {
        MyCodeExpression _myCode;
        internal readonly IfExpressionBuilder IfExpressionBuilder;
        internal string Tabs = string.Empty;

        public IfCodeBlockIfExpressionBuilder(IfExpressionBuilder source, MyCodeExpression ifExpression)
        {
            _myCode = ifExpression;
            IfExpressionBuilder = source;
            Tabs += source.Tabs;
        }

        /// <summary>
        /// Add else block in if statement
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .If (...)
        ///   ...
        ///   .Else
        ///   ...
        /// </code>
        /// result:
        /// <code>
        /// if (...) 
        /// {
        ///     ...
        /// }
        /// else
        /// {
        ///     ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
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

        /// <summary>
        /// Add else if block in if statement
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .If (...)
        ///   ...
        ///   .ElseIf_.Condition("condition")
        ///   ...
        /// </code>
        /// result:
        /// <code>
        /// if (...) 
        /// {
        ///     ...
        /// }
        /// else if (condition)
        /// {
        ///     ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
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

        /// <summary>
        /// Add else if block in if statement
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .If (...)
        ///   ...
        ///   .ElseIf("condition")
        ///   ...
        /// </code>
        /// result:
        /// <code>
        /// if (...) 
        /// {
        ///     ...
        /// }
        /// else if (condition)
        /// {
        ///     ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public IfConditionExpressionBuilder ElseIf(string condition)
        {

            _myCode
                .NewLine.Add(Tabs).FinishCodeBlock
                .NewLine.Add(Tabs).Else._.If._.This();
            _myCode.OpenBracket.Add(condition);
            IfConditionExpressionBuilder builder = new IfConditionExpressionBuilder(IfExpressionBuilder, _myCode);
            return builder;
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
        public IfCodeBlockIfExpressionBuilder AddLine(string codeLine)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(codeLine);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public IfCodeBlockIfExpressionBuilder AddCode(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expressionBuilder"></param>
        /// <returns></returns>
        public IfCodeBlockIfExpressionBuilder AddCode(MyCodeExpressionBuilder expressionBuilder)
        {
            _myCode.Add(expressionBuilder);
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public IfCodeBlockIfExpressionBuilder AddVariable(MyField field)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(field);
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="fieldBuilder"></param>
        /// <returns></returns>
        public IfCodeBlockIfExpressionBuilder AddVariable(MyFieldBuilder fieldBuilder)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            return this;
        }
        public IfCodeBlockIfExpressionBuilder AddLines(IEnumerable<string> codeLines)
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
        public IfCodeBlockIfExpressionBuilder AddCodes(IEnumerable<MyCodeExpression> expressions)
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
        public IfCodeBlockIfExpressionBuilder AddCodes(IEnumerable<MyCodeExpressionBuilder> expressionBuilders)
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
        public IfCodeBlockIfExpressionBuilder AddVariables(IEnumerable<MyField> fields)
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
        public IfCodeBlockIfExpressionBuilder AddVariables(IEnumerable<MyFieldBuilder> fieldBuilders)
        {
            foreach (var fieldBuilder in fieldBuilders)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            }
            return this;
        }


    }

    
}
