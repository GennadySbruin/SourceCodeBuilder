using SourceCodeBuilder;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace SourceCodeBuilderTest
{
    [TestClass]
    public sealed class TestProperties
    {
        private TestContext testContextInstance;
        private StringBuilder _stringBuilder = new StringBuilder();
        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void TestProperties1()
        {
            string PropertyName = "PropertyName";
            string PropertyTypeName = "string";
            Assert.IsTrue(Test(MyProperty.String(PropertyName), "string PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.Int(PropertyName), "int PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.Double(PropertyName), "double PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.Float(PropertyName), "float PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.Boolean(PropertyName), "bool PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.Byte(PropertyName), "byte PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.DateTime(PropertyName), "DateTime PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.Decimal(PropertyName), "decimal PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.Guid(PropertyName), "Guid PropertyName { get; set; }"));

            Assert.IsTrue(Test(MyProperty.PublicString(PropertyName), "public string PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.PublicInt(PropertyName), "public int PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.PublicDouble(PropertyName), "public double PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.PublicFloat(PropertyName), "public float PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.PublicBoolean(PropertyName), "public bool PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.PublicByte(PropertyName), "public byte PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.PublicDateTime(PropertyName), "public DateTime PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.PublicDecimal(PropertyName), "public decimal PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyProperty.PublicGuid(PropertyName), "public Guid PropertyName { get; set; }"));

            Assert.IsTrue(Test(MyPropertyBuilder.Private.Type("int").Name(PropertyName), "private int PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyPropertyBuilder.Internal.Type("double").Name(PropertyName), "internal double PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyPropertyBuilder.Public.Type("Generic<byte>").Name(PropertyName), "public Generic<byte> PropertyName { get; set; }"));

            Assert.IsTrue(Test(MyPropertyBuilder.Public.Type("int").Name(PropertyName).Array, "public int[] PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyPropertyBuilder.Public.Type("int").Name(PropertyName).List, "public List<int> PropertyName { get; set; }"));

            Assert.IsTrue(Test(MyPropertyBuilder.Public.Type("int").Name(PropertyName).Static, "public static int PropertyName { get; set; }"));
            
            Assert.IsTrue(Test(MyPropertyBuilder.Public.Abstract.Type("int").Name(PropertyName), "public abstract int PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyPropertyBuilder.Internal.Protected.Type("int").Name(PropertyName), "internal protected int PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyPropertyBuilder.Internal.Sealed.Type("int").Name(PropertyName), "internal sealed int PropertyName { get; set; }"));
            Assert.IsTrue(Test(MyPropertyBuilder.Public.Virtual.Type("int").Name(PropertyName), "public virtual int PropertyName { get; set; }"));

            TestContext.Write(_stringBuilder.ToString());
        }

        private bool Test(MyPropertyBuilder builder, string code)
        {
            string result = builder.ToString().Trim();
            _stringBuilder.AppendLine(result);
            if (result == code)
            {
                return true;
            }
            else
            {
                TestContext.Write($"{result} != {code}");
                return false;
            }
            
        }

        private class TestClass
        {
            protected string s;
            //public virtual string s;
        }
    }
}
