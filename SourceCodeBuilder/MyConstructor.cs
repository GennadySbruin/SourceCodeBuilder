using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public class MyConstructor
    {
        /// <summary>
        /// Class name
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// private string ClassName;
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public string? ClassName { get; set; }
                
        /// <summary>
        /// Pethod parameters
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// private void Test(int A, string B);
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public List<string>? Parametrs { get; set; }

        /// <summary>
        /// Method lambda getter expresion
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// public string Code(string value) => AnotherMethod(value);
        /// int Length(string value) => a + b + c;
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public string? LambdaExpression { get; set; }

        /// <summary>
        /// Base constructor expresion
        /// <example>
        /// <para>For example:</para>
        /// <code>
        //////// public string Code(string value) => AnotherMethod(value);
        /// int Length(string value) => a + b + c;
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public string? BaseConstructorExpression { get; set; }

        public List<string> Comments { get; set; } = [];
        public List<string> Attributes { get; set; } = [];

        public string? Base { get; set; }
        public bool AutoGenerateComments { get; set; } = false;
        public bool Async { get; set; }

        public MyCodeExpression MyCode = new();
        public List<AccessModifiers?> AccessModifiersList { get; set; } = [];
        public enum AccessModifiers
        {
            Private,
            Internal,
            Public,
            Static,
        }

        public void AddParameter(string parameter)
        {
            if (Parametrs == null)
            {
                Parametrs = [];
            }
            Parametrs.Add(parameter);
        }

        public override string? ToString()
        {
            return ToString(new MyConstructorWriter());
        }

        public string? ToString(ICodeWriter<MyConstructor> formatter)
        {
            return formatter?.WriteCode(this);
        }


    }
}
