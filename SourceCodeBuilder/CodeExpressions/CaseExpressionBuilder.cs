using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace SourceCodeBuilder.CodeExpressions
{
    /// <summary>
    /// Case statement builder
    /// </summary>
    public class CaseExpressionBuilder
    {
        MyCodeExpression _myCode = new();
        internal readonly MyCodeExpressionBuilder ParentExpressionBuilder;
        internal string Tabs = string.Empty;

        public CaseExpressionBuilder(MyCodeExpressionBuilder source, MyCodeExpression myCodeExpression)
        {
            ParentExpressionBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }

        /// <summary>
        /// Expression 'default:'
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .Switch(...)
        ///      .Default
        ///         ....
        /// </code>
        /// result:
        /// <code>
        /// switch (...) 
        /// {
        ///     default:
        ///     ...
        /// </code>
        /// </example>
        /// </summary>
        public DefaultExpressionBuilder Default
        {
            get
            {
                _myCode
                    .NewLine.Add(Tabs).Tab.SwitchDefault.This();
                return new DefaultExpressionBuilder(this, _myCode);
            }
        }

        /// <summary>
        /// Add 'case' section in switch statement with case expression
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .Switch(...)
        ///      .Case("expression")
        ///      ...        
        /// </code>
        /// result:
        /// <code>
        /// switch (...) 
        /// {
        ///     case expression:
        ///     ...
        /// </code>
        /// </example>
        /// </summary>
        public CaseExpressionBuilder Case(string condition)
        {
            _myCode
                .NewLine.Add(Tabs).Tab.Case._.Add($"{condition}:");
            return this;
        }

        /// <summary>
        /// Close switch code block with expression '}'
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .Switch(...)
        ///           ...
        ///   .EndSwitch        
        /// </code>
        /// result:
        /// <code>
        /// switch (...) 
        /// {
        ///         ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public MyCodeExpressionBuilder EndSwitch
        {
            get
            {
                _myCode.NewLine.Add(Tabs).FinishCodeBlock.This();
                return ParentExpressionBuilder;
            }
        }

        /// <summary>
        /// Break case block in switch statement with expression 'break;'
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .Switch(...)
        ///       .Case ...
        ///       .Break
        ///       ...
        /// </code>
        /// result:
        /// <code>
        /// switch (...) 
        /// {
        ///     case ...:
        ///         break;
        /// </code>
        /// </example>
        /// </summary>
        public CaseExpressionBuilder Break
        {
            get
            {
                _myCode.NewLine.Add(Tabs).Tab2.Add("break;")
                       .NewLine.This();
                return this;
            }
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="codeLine"></param>
        /// <returns></returns>
        public CaseExpressionBuilder AddLine(string codeLine)
        {
            _myCode.NewLine.Add(Tabs).Tab2.Add(codeLine);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CaseExpressionBuilder AddCode(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expressionBuilder"></param>
        /// <returns></returns>
        public CaseExpressionBuilder AddCode(MyCodeExpressionBuilder expressionBuilder)
        {
            _myCode.Add(expressionBuilder);
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public CaseExpressionBuilder AddVariable(MyField field)
        {
            _myCode.NewLine.Add(Tabs).Tab2.Add(field);
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="fieldBuilder"></param>
        /// <returns></returns>
        public CaseExpressionBuilder AddVariable(MyFieldBuilder fieldBuilder)
        {
            _myCode.NewLine.Add(Tabs).Tab2.Add(fieldBuilder);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="codeLines"></param>
        /// <returns></returns>
        public CaseExpressionBuilder AddLines(IEnumerable<string> codeLines)
        {
            foreach (var codeLine in codeLines)
            {
                _myCode.NewLine.Add(Tabs).Tab2.Add(codeLine);
            }
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expressions"></param>
        /// <returns></returns>
        public CaseExpressionBuilder AddCodes(IEnumerable<MyCodeExpression> expressions)
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
        public CaseExpressionBuilder AddCodes(IEnumerable<MyCodeExpressionBuilder> expressionBuilders)
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
        public CaseExpressionBuilder AddVariables(IEnumerable<MyField> fields)
        {
            foreach (var field in fields)
            {
                _myCode.NewLine.Add(Tabs).Tab2.Add(field);
            }
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="fieldBuilders"></param>
        /// <returns></returns>
        public CaseExpressionBuilder AddVariables(IEnumerable<MyFieldBuilder> fieldBuilders)
        {
            foreach (var fieldBuilder in fieldBuilders)
            {
                _myCode.NewLine.Add(Tabs).Tab2.Add(fieldBuilder);
            }
            return this;
        }
    }
}
