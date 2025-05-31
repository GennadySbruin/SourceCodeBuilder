using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public class MyField
    {

        /// <summary>
        /// Field name
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// private string FieldName;
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public string? FieldName;

        /// <summary>
        /// Name of field type
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// private FieldTypeName Field;
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public string? FieldTypeName;

        /// <summary>
        /// Field initial expression
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// string Code = "InitialExpression";
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public string? InitialExpression;

        /// <summary>
        /// Field starting tab
        /// <example>
        /// <para>Example 1: without tab</para>
        /// <code>
        /// string Code;
        /// </code>
        /// </example>
        /// <example>
        /// <para>Example 2: with tab</para>
        /// <code>
        ///     string Code;
        /// </code>
        /// </example>
        /// <example>
        /// <para>Example 3: with 2 tab</para>
        /// <code>
        ///         string Code ;
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        //public string Tabs = string.Empty;
        public List<AccessModifiers?> AccessModifiersList { get; set; } = [];
        public List<string> Comments { get; set; } = [];
        public enum AccessModifiers
        {
            Static,
            Private,
            Internal,
            Public,
            Protected,
            Const
        }

        /// <summary>
        /// String Field
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.String("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>string Description;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder String(string FieldName) =>
            new MyFieldBuilder().Type("string").Name(FieldName);

        /// <summary>
        /// int Field
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Int("Count").ToString();
        /// </code>
        /// result:
        /// <para><c>int Count;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder Int(string FieldName) =>
            new MyFieldBuilder().Type("int").Name(FieldName);

        /// <summary>
        /// Guid Field
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Guid("RecordId").ToString();
        /// </code>
        /// result:
        /// <para><c>Guid RecordId;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder Guid(string FieldName) =>
            new MyFieldBuilder().Type("Guid").Name(FieldName);

        /// <summary>
        /// decimal Field
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Decimal("Amount").ToString();
        /// </code>
        /// result:
        /// <para><c>decimal Amount;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder Decimal(string FieldName) =>
            new MyFieldBuilder().Type("decimal").Name(FieldName);

        /// <summary>
        /// DateTime Field
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.DateTime("LastUpdateDateTime").ToString();
        /// </code>
        /// result:
        /// <para><c>DateTime LastUpdateDateTime;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder DateTime(string FieldName) =>
            new MyFieldBuilder().Type("DateTime").Name(FieldName);

        /// <summary>
        /// double Field
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Double("Total").ToString();
        /// </code>
        /// result:
        /// <para><c>double Total;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder Double(string FieldName) =>
            new MyFieldBuilder().Type("double").Name(FieldName);


        /// <summary>
        /// float Field
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Float("Value").ToString();
        /// </code>
        /// result:
        /// <para><c>float Value;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder Float(string FieldName) =>
            new MyFieldBuilder().Type("float").Name(FieldName);

        /// <summary>
        /// boolean Field
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Boolean("IsFinished").ToString();
        /// </code>
        /// result:
        /// <para><c>bool IsFinished;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder Boolean(string FieldName) =>
            new MyFieldBuilder().Type("bool").Name(FieldName);

        /// <summary>
        /// byty Field
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.Byte("IsFinished").ToString();
        /// </code>
        /// result:
        /// <para><c>byte IsFinished;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder Byte(string FieldName) =>
            new MyFieldBuilder().Type("byte").Name(FieldName);




        /// <summary>
        /// String Field with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.PublicString("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>public string Description;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder PublicString(string FieldName) =>
            MyFieldBuilder.Public.Type("string").Name(FieldName);

        /// <summary>
        /// int Field with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.PublicInt("Count").ToString();
        /// </code>
        /// result:
        /// <para><c>public int Count;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder PublicInt(string FieldName) =>
            MyFieldBuilder.Public.Type("int").Name(FieldName);

        /// <summary>
        /// Guid Field with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.PublicGuid("RecordId").ToString();
        /// </code>
        /// result:
        /// <para><c>public Guid RecordId;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder PublicGuid(string FieldName) =>
            MyFieldBuilder.Public.Type("Guid").Name(FieldName);

        /// <summary>
        /// decimal Field with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.PublicDecimal("Amount").ToString();
        /// </code>
        /// result:
        /// <para><c>public decimal Amount;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder PublicDecimal(string FieldName) =>
            MyFieldBuilder.Public.Type("decimal").Name(FieldName);

        /// <summary>
        /// DateTime Field with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.PublicDateTime("LastUpdateDateTime").ToString();
        /// </code>
        /// result:
        /// <para><c>public DateTime LastUpdateDateTime;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder PublicDateTime(string FieldName) =>
            MyFieldBuilder.Public.Type("DateTime").Name(FieldName);

        /// <summary>
        /// double Field with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.PublicDouble("Total").ToString();
        /// </code>
        /// result:
        /// <para><c>public double Total;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder PublicDouble(string FieldName) =>
            MyFieldBuilder.Public.Type("double").Name(FieldName);


        /// <summary>
        /// float Field with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.PublicFloat("Value").ToString();
        /// </code>
        /// result:
        /// <para><c>public float Value;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder PublicFloat(string FieldName) =>
            MyFieldBuilder.Public.Type("float").Name(FieldName);

        /// <summary>
        /// boolean Field with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.PublicBoolean("IsFinished").ToString();
        /// </code>
        /// result:
        /// <para><c>public bool IsFinished;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder PublicBoolean(string FieldName) =>
            MyFieldBuilder.Public.Type("bool").Name(FieldName);

        /// <summary>
        /// byty Field with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyFieldBuilder.PublicByte("IsFinished").ToString();
        /// </code>
        /// result:
        /// <para><c>public byte IsFinished;</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyFieldBuilder PublicByte(string FieldName) =>
            MyFieldBuilder.Public.Type("byte").Name(FieldName);


        public override string? ToString()
        {
            return ToString(new MyFieldWriter());
        }

        public string? ToString(ICodeWriter<MyField> formatter)
        {
            return formatter?.WriteCode(this);
        }

    }
}
