using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilder
{
    public class MyProperty
    {
        /// <summary>
        /// Property name
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// private string PropertyName { get ; set; };
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public string? PropertyName { get; set; }

        /// <summary>
        /// Name of property type
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// private PropertyTypeName Property { get ; set; };
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public string? PropertyTypeName { get; set; }

        /// <summary>
        /// Property initial expression
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// string Code { get ; set; } = "InitialExpression";
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public string? InitialExpression { get; set; }

        /// <summary>
        /// Property lambda getter expresion
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// public string Code => "LambdaExpression";
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public string? LambdaExpression { get; set; }

        /// <summary>
        /// Property starting tab
        /// <example>
        /// <para>Example 1: without tab</para>
        /// <code>
        /// string Code { get ; set; };
        /// </code>
        /// </example>
        /// <example>
        /// <para>Example 2: with tab</para>
        /// <code>
        ///     string Code { get ; set; };
        /// </code>
        /// </example>
        /// <example>
        /// <para>Example 3: with 2 tab</para>
        /// <code>
        ///         string Code { get ; set; };
        /// </code>
        /// </example>
        /// </summary>
        /// <returns></returns>
        //public string Tabs = string.Empty;

        public bool ExpandCodeBlock { get; set; } = false;
        public bool WithSetter { get; set; } = true;
        public bool WithGetter { get; set; } = true;

        public string? GetterExpression { get; set; }
        public string? SetterExpression { get; set; }
        

        public List<AccessModifiers?> AccessModifiersList { get; set; } = [];
        public List<string> Comments { get; set; } = [];
        public enum AccessModifiers
        {
            Abstract,
            Static,
            Private,
            Internal,
            Public,
            Sealed,
            Protected,
            Override,
            Virtual
        }


        /// <summary>
        /// String property
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.String("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>string Description { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder String(string propertyName) => 
            new MyPropertyBuilder().Type("string").Name(propertyName);

        /// <summary>
        /// int property
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Int("Count").ToString();
        /// </code>
        /// result:
        /// <para><c>int Count { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder Int(string propertyName) =>
            new MyPropertyBuilder().Type("int").Name(propertyName);

        /// <summary>
        /// Guid property
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Guid("RecordId").ToString();
        /// </code>
        /// result:
        /// <para><c>Guid RecordId { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder Guid(string propertyName) =>
            new MyPropertyBuilder().Type("Guid").Name(propertyName);

        /// <summary>
        /// decimal property
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Decimal("Amount").ToString();
        /// </code>
        /// result:
        /// <para><c>decimal Amount { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder Decimal(string propertyName) =>
            new MyPropertyBuilder().Type("decimal").Name(propertyName);

        /// <summary>
        /// DateTime property
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.DateTime("LastUpdateDateTime").ToString();
        /// </code>
        /// result:
        /// <para><c>DateTime LastUpdateDateTime { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder DateTime(string propertyName) =>
            new MyPropertyBuilder().Type("DateTime").Name(propertyName);

        /// <summary>
        /// double property
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Double("Total").ToString();
        /// </code>
        /// result:
        /// <para><c>double Total { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder Double(string propertyName) =>
            new MyPropertyBuilder().Type("double").Name(propertyName);


        /// <summary>
        /// float property
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Float("Value").ToString();
        /// </code>
        /// result:
        /// <para><c>float Value { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder Float(string propertyName) =>
            new MyPropertyBuilder().Type("float").Name(propertyName);

        /// <summary>
        /// boolean property
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Boolean("IsFinished").ToString();
        /// </code>
        /// result:
        /// <para><c>bool IsFinished { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder Boolean(string propertyName) =>
            new MyPropertyBuilder().Type("bool").Name(propertyName);

        /// <summary>
        /// byty property
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.Byte("IsFinished").ToString();
        /// </code>
        /// result:
        /// <para><c>byte IsFinished { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder Byte(string propertyName) =>
            new MyPropertyBuilder().Type("byte").Name(propertyName);




        /// <summary>
        /// String property with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.PublicString("Description").ToString();
        /// </code>
        /// result:
        /// <para><c>public string Description { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder PublicString(string propertyName) =>
            MyPropertyBuilder.Public.Type("string").Name(propertyName);

        /// <summary>
        /// int property with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.PublicInt("Count").ToString();
        /// </code>
        /// result:
        /// <para><c>public int Count { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder PublicInt(string propertyName) =>
            MyPropertyBuilder.Public.Type("int").Name(propertyName);

        /// <summary>
        /// Guid property with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.PublicGuid("RecordId").ToString();
        /// </code>
        /// result:
        /// <para><c>public Guid RecordId { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder PublicGuid(string propertyName) =>
            MyPropertyBuilder.Public.Type("Guid").Name(propertyName);

        /// <summary>
        /// decimal property with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.PublicDecimal("Amount").ToString();
        /// </code>
        /// result:
        /// <para><c>public decimal Amount { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder PublicDecimal(string propertyName) =>
            MyPropertyBuilder.Public.Type("decimal").Name(propertyName);

        /// <summary>
        /// DateTime property with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.PublicDateTime("LastUpdateDateTime").ToString();
        /// </code>
        /// result:
        /// <para><c>public DateTime LastUpdateDateTime { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder PublicDateTime(string propertyName) =>
            MyPropertyBuilder.Public.Type("DateTime").Name(propertyName);

        /// <summary>
        /// double property with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.PublicDouble("Total").ToString();
        /// </code>
        /// result:
        /// <para><c>public double Total { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder PublicDouble(string propertyName) =>
            MyPropertyBuilder.Public.Type("double").Name(propertyName);


        /// <summary>
        /// float property with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.PublicFloat("Value").ToString();
        /// </code>
        /// result:
        /// <para><c>public float Value { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder PublicFloat(string propertyName) =>
            MyPropertyBuilder.Public.Type("float").Name(propertyName);

        /// <summary>
        /// boolean property with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.PublicBoolean("IsFinished").ToString();
        /// </code>
        /// result:
        /// <para><c>public bool IsFinished { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder PublicBoolean(string propertyName) =>
            MyPropertyBuilder.Public.Type("bool").Name(propertyName);

        /// <summary>
        /// byty property with public access
        /// <example>
        /// <para>For example:</para>
        /// <code>
        /// MyPropertyBuilder.PublicByte("IsFinished").ToString();
        /// </code>
        /// result:
        /// <para><c>public byte IsFinished { get; set; }</c></para>
        /// </example>
        /// </summary>
        /// <returns></returns>
        public static MyPropertyBuilder PublicByte(string propertyName) =>
            MyPropertyBuilder.Public.Type("byte").Name(propertyName);


        public override string? ToString()
        {
            //return ToString(new MyDefaultPropertyDeclarationFormatter() { DefaultTabs = Tabs});
            return ToString(new MyPropertyWriter());
        }

        public string? ToString(ICodeWriter<MyProperty> formatter)
        {
            return formatter?.WriteCode(this);
        }
    }
}
