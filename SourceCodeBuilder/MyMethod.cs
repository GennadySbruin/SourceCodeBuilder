using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public class MyMethod
    {
        /// <summary>
        /// Method name
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// private string MethodName();
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public string? MethodName { get; set; }

        /// <summary>
        /// Return type
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// private MethodReturnTypeName Method();
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public string? MethodReturnTypeName { get; set; }

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


        public List<string> Comments { get; set; } = [];
        public List<string> Attributes { get; set; } = [];
        public bool AutoGenerateComments { get; set; } = false;
        public Dictionary<string, string>? GenericConstraintDictionary { get; set; }
        public List<string>? GenericList { get; set; }
        public string GenericWhere { get; set; }
        public bool Async { get; set; }

        public MyCodeExpression MyCode = new();
        public List<AccessModifiers?> AccessModifiersList { get; set; } = [];
        public enum AccessModifiers
        {
            Abstract,
            Private,
            Internal,
            Public,
            Sealed,
            Static,
            Protected,
            Override,
            Virtual
        }

        /// <summary>
        /// void method
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Void("Start").ToString();
        /// </code>
        /// result:
        /// <para><c>string Start() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder Void(string methodName) =>
            new MyMethodBuilder().Type("void").Name(methodName);

        /// <summary>
        /// String method
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.String("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder String(string methodName) =>
            new MyMethodBuilder().Type("string").Name(methodName);

        /// <summary>
        /// int method
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Int("Count").ToString();
        /// </code>
        /// result:
        /// <para><c>int Count() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder Int(string methodName) =>
            new MyMethodBuilder().Type("int").Name(methodName);

        /// <summary>
        /// Guid method
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Guid("RecordId").ToString();
        /// </code>
        /// result:
        /// <para><c>Guid RecordId() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder Guid(string methodName) =>
            new MyMethodBuilder().Type("Guid").Name(methodName);

        /// <summary>
        /// decimal method
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Decimal("Amount").ToString();
        /// </code>
        /// result:
        /// <para><c>decimal Amount() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder Decimal(string methodName) =>
            new MyMethodBuilder().Type("decimal").Name(methodName);

        /// <summary>
        /// DateTime method
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.DateTime("LastUpdateDateTime").ToString();
        /// </code>
        /// result:
        /// <para><c>DateTime LastUpdateDateTime() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder DateTime(string methodName) =>
            new MyMethodBuilder().Type("DateTime").Name(methodName);

        /// <summary>
        /// double method
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Double("Total").ToString();
        /// </code>
        /// result:
        /// <para><c>double Total() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder Double(string methodName) =>
            new MyMethodBuilder().Type("double").Name(methodName);


        /// <summary>
        /// float method
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Float("Value").ToString();
        /// </code>
        /// result:
        /// <para><c>float Value() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder Float(string methodName) =>
            new MyMethodBuilder().Type("float").Name(methodName);

        /// <summary>
        /// boolean method
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Boolean("IsFinished").ToString();
        /// </code>
        /// result:
        /// <para><c>bool IsFinished() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder Boolean(string methodName) =>
            new MyMethodBuilder().Type("bool").Name(methodName);

        /// <summary>
        /// byty method
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.Byte("IsFinished").ToString();
        /// </code>
        /// result:
        /// <para><c>byte IsFinished() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder Byte(string methodName) =>
            new MyMethodBuilder().Type("byte").Name(methodName);


        /// <summary>
        /// Void method with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.PublicVoid("Start").ToString();
        /// </code>
        /// result:
        /// <para><c>public void Start() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder PublicVoid(string methodName) =>
            MyMethodBuilder.Public.Type("string").Name(methodName);

        /// <summary>
        /// String method with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.PublicString("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>public string Description() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder PublicString(string methodName) =>
            MyMethodBuilder.Public.Type("string").Name(methodName);

        /// <summary>
        /// int method with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.PublicInt("Count").ToString();
        /// </code>
        /// result:
        /// <para><c>public int Count() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder PublicInt(string methodName) =>
            MyMethodBuilder.Public.Type("int").Name(methodName);

        /// <summary>
        /// Guid method with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.PublicGuid("RecordId").ToString();
        /// </code>
        /// result:
        /// <para><c>public Guid RecordId() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder PublicGuid(string methodName) =>
            MyMethodBuilder.Public.Type("Guid").Name(methodName);

        /// <summary>
        /// decimal method with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.PublicDecimal("Amount").ToString();
        /// </code>
        /// result:
        /// <para><c>public decimal Amount() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder PublicDecimal(string methodName) =>
            MyMethodBuilder.Public.Type("decimal").Name(methodName);

        /// <summary>
        /// DateTime method with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.PublicDateTime("LastUpdateDateTime").ToString();
        /// </code>
        /// result:
        /// <para><c>public DateTime LastUpdateDateTime() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder PublicDateTime(string methodName) =>
            MyMethodBuilder.Public.Type("DateTime").Name(methodName);

        /// <summary>
        /// double method with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.PublicDouble("Total").ToString();
        /// </code>
        /// result:
        /// <para><c>public double Total() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder PublicDouble(string methodName) =>
            MyMethodBuilder.Public.Type("double").Name(methodName);


        /// <summary>
        /// float method with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.PublicFloat("Value").ToString();
        /// </code>
        /// result:
        /// <para><c>public float Value() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder PublicFloat(string methodName) =>
            MyMethodBuilder.Public.Type("float").Name(methodName);

        /// <summary>
        /// boolean method with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.PublicBoolean("IsFinished").ToString();
        /// </code>
        /// result:
        /// <para><c>public bool IsFinished() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder PublicBoolean(string methodName) =>
            MyMethodBuilder.Public.Type("bool").Name(methodName);

        /// <summary>
        /// byty method with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyMethodBuilder.PublicByte("IsFinished").ToString();
        /// </code>
        /// result:
        /// <para><c>public byte IsFinished() { }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyMethodBuilder PublicByte(string methodName) =>
            MyMethodBuilder.Public.Type("byte").Name(methodName);

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
            return ToString(new MyMethodWriter());
        }

        public string? ToString(ICodeWriter<MyMethod> formatter)
        {
            return formatter?.WriteCode(this);
        }


    }
}
