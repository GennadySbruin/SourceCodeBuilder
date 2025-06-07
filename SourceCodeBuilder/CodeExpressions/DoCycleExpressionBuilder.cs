using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace SourceCodeBuilder.CodeExpressions
{
    /// <summary>
    /// Do statement builder
    /// </summary>
    public class DoCycleExpressionBuilder
    {
        MyCodeExpression _myCode = new();
        internal readonly MyCodeExpressionBuilder ParentExpressionBuilder;
        internal string Tabs = string.Empty;

        public DoCycleExpressionBuilder(MyCodeExpressionBuilder source, MyCodeExpression myCodeExpression)
        {
            ParentExpressionBuilder = source;
            _myCode = myCodeExpression;
            Tabs += source.Tabs;
        }

        /// <summary>
        /// Add 'while' block to finish do statement
        /// <example>
        /// <para>Example:</para>
        /// <code>
        ///MyCodeExpressionBuilder.Start()
        ///   .Do
        ///   ...
        ///   .While("expression")
        ///   ...
        /// </code>
        /// result:
        /// <code>
        /// do 
        /// {
        /// 
        /// } while (expression);
        /// </code>
        /// </example>
        /// </summary>
        public MyCodeExpressionBuilder While(string whileExpression)
        {
            _myCode.NewLine.Add(Tabs).FinishCodeBlock._
                .While._.OpenBracket.Add(whileExpression).CloseBracket.Finish.This();
            return ParentExpressionBuilder;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="codeLine"></param>
        /// <returns></returns>
        public DoCycleExpressionBuilder AddLine(string codeLine)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(codeLine);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public DoCycleExpressionBuilder AddCode(MyCodeExpression expression)
        {
            _myCode.Add(expression);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="expressionBuilder"></param>
        /// <returns></returns>
        public DoCycleExpressionBuilder AddCode(MyCodeExpressionBuilder expressionBuilder)
        {
            _myCode.Add(expressionBuilder);
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public DoCycleExpressionBuilder AddVariable(MyField field)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(field);
            return this;
        }

        /// <summary>
        /// Add variable expression
        /// </summary>
        /// <param name="fieldBuilder"></param>
        /// <returns></returns>
        public DoCycleExpressionBuilder AddVariable(MyFieldBuilder fieldBuilder)
        {
            _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            return this;
        }

        /// <summary>
        /// Add code expression
        /// </summary>
        /// <param name="codeLines"></param>
        /// <returns></returns>
        public DoCycleExpressionBuilder AddLines(IEnumerable<string> codeLines)
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
        public DoCycleExpressionBuilder AddCodes(IEnumerable<MyCodeExpression> expressions)
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
        public DoCycleExpressionBuilder AddCodes(IEnumerable<MyCodeExpressionBuilder> expressionBuilders)
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
        public DoCycleExpressionBuilder AddVariables(IEnumerable<MyField> fields)
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
        public DoCycleExpressionBuilder AddVariables(IEnumerable<MyFieldBuilder> fieldBuilders)
        {
            foreach (var fieldBuilder in fieldBuilders)
            {
                _myCode.NewLine.Add(Tabs).Tab.Add(fieldBuilder);
            }
            return this;
        }
    }
}
