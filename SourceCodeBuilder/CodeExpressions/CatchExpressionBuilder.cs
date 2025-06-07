using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace SourceCodeBuilder.CodeExpressions
{
    /// <summary>
    /// Catch statement builder
    /// </summary>
    public class CatchExpressionBuilder
    {
        MyCodeExpression _myCode = new();
        internal readonly TryExpressionBuilder ParentExpressionBuilder;
        internal string Tabs = string.Empty;

        public CatchExpressionBuilder(TryExpressionBuilder source, MyCodeExpression myCodeExpression)
        {
            ParentExpressionBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }

        /// <summary>
        /// Add 'finaly' block to try statement
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .Try
        ///   ...
        ///   .Catch (...) 
        ///   ...
        ///   .Finaly
        ///   ...
        /// </code>
        /// result:
        /// <code>
        /// try 
        /// {
        /// 
        /// }
        /// catch (...)
        /// {
        ///    ...
        /// }
        /// finaly
        /// {
        ///    ...
        /// }
        /// </code>
        /// </example>
        /// </summary>
        public FinalyExpressionBuilder Finaly
        {
            get
            {
                _myCode
                    .NewLine.Add(Tabs).FinishCodeBlock
                    .NewLine.Add(Tabs).Finaly
                    .NewLine.Add(Tabs).StartCodeBlock.This();
                return new FinalyExpressionBuilder(ParentExpressionBuilder, _myCode);
            }
        }

        /// <summary>
        /// Add 'catch' block to try statement
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .Try
        ///   ...
        ///   .Catch("Exception", "ex")
        ///   ...
        /// </code>
        /// result:
        /// <code>
        /// try 
        /// {
        /// 
        /// }
        /// Catch(Exception ex)
        /// {
        ///     ...
        /// </code>
        /// </example>
        /// </summary>
        public CatchExpressionBuilder Catch(string exceptionType, string exceptionVariableName)
        {
            _myCode
                .NewLine.Add(Tabs).FinishCodeBlock
                .NewLine.Add(Tabs).Catch._
                    .OpenBracket
                        .Add(exceptionType)._.Add(exceptionVariableName)
                    .CloseBracket
                .NewLine.Add(Tabs).StartCodeBlock.This();
            CatchExpressionBuilder builder = new CatchExpressionBuilder(ParentExpressionBuilder, _myCode);
            return builder;
        }

        /// <summary>
        /// Close catch statement with expression '}'
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .Try
        ///   .Catch (...)
        ///   ...
        ///   .EndTry
        /// </code>
        /// result:
        /// <code>
        /// try 
        /// {
        ///     ...
        /// }
        /// catch (...)
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
        public CatchExpressionBuilder AddLine(string codeLine)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(codeLine);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public CatchExpressionBuilder AddCode(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expressionBuilder"></param>
        /// <returns></returns>
        public CatchExpressionBuilder AddCode(MyCodeExpressionBuilder expressionBuilder)
        {
            _myCode.Add(expressionBuilder);
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public CatchExpressionBuilder AddVariable(MyField field)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(field);
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="fieldBuilder"></param>
        /// <returns></returns>
        public CatchExpressionBuilder AddVariable(MyFieldBuilder fieldBuilder)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="codeLines"></param>
        /// <returns></returns>
        public CatchExpressionBuilder AddLines(IEnumerable<string> codeLines)
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
        public CatchExpressionBuilder AddCodes(IEnumerable<MyCodeExpression> expressions)
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
        public CatchExpressionBuilder AddCodes(IEnumerable<MyCodeExpressionBuilder> expressionBuilders)
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
        public CatchExpressionBuilder AddVariables(IEnumerable<MyField> fields)
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
        public CatchExpressionBuilder AddVariables(IEnumerable<MyFieldBuilder> fieldBuilders)
        {
            foreach (var fieldBuilder in fieldBuilders)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            }
            return this;
        }
    }
}
