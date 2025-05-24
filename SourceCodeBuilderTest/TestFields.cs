using SourceCodeBuilder;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace SourceCodeBuilderTest
{
    [TestClass]
    public sealed class TestFields
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
        public void TestFields1()
        {
            string FieldName = "FieldName";
            Assert.IsTrue(Test(MyField.String(FieldName), "string FieldName;"));
            Assert.IsTrue(Test(MyField.Int(FieldName), "int FieldName;"));
            Assert.IsTrue(Test(MyField.Double(FieldName), "double FieldName;"));
            Assert.IsTrue(Test(MyField.Float(FieldName), "float FieldName;"));
            Assert.IsTrue(Test(MyField.Boolean(FieldName), "bool FieldName;"));
            Assert.IsTrue(Test(MyField.Byte(FieldName), "byte FieldName;"));
            Assert.IsTrue(Test(MyField.DateTime(FieldName), "DateTime FieldName;"));
            Assert.IsTrue(Test(MyField.Decimal(FieldName), "decimal FieldName;"));
            Assert.IsTrue(Test(MyField.Guid(FieldName), "Guid FieldName;"));

            Assert.IsTrue(Test(MyField.PublicString(FieldName), "public string FieldName;"));
            Assert.IsTrue(Test(MyField.PublicInt(FieldName), "public int FieldName;"));
            Assert.IsTrue(Test(MyField.PublicDouble(FieldName), "public double FieldName;"));
            Assert.IsTrue(Test(MyField.PublicFloat(FieldName), "public float FieldName;"));
            Assert.IsTrue(Test(MyField.PublicBoolean(FieldName), "public bool FieldName;"));
            Assert.IsTrue(Test(MyField.PublicByte(FieldName), "public byte FieldName;"));
            Assert.IsTrue(Test(MyField.PublicDateTime(FieldName), "public DateTime FieldName;"));
            Assert.IsTrue(Test(MyField.PublicDecimal(FieldName), "public decimal FieldName;"));
            Assert.IsTrue(Test(MyField.PublicGuid(FieldName), "public Guid FieldName;"));

            Assert.IsTrue(Test(MyFieldBuilder.Private.Type("int").Name(FieldName), "private int FieldName;"));
            Assert.IsTrue(Test(MyFieldBuilder.Internal.Type("double").Name(FieldName), "internal double FieldName;"));
            Assert.IsTrue(Test(MyFieldBuilder.Public.Type("Generic<byte>").Name(FieldName), "public Generic<byte> FieldName;"));

            Assert.IsTrue(Test(MyFieldBuilder.Public.Type("int").Name(FieldName).Array, "public int[] FieldName;"));
            Assert.IsTrue(Test(MyFieldBuilder.Public.Type("int").Name(FieldName).List, "public List<int> FieldName;"));

            Assert.IsTrue(Test(MyFieldBuilder.Public.Type("int").Name(FieldName).Static, "public static int FieldName;"));
            
            Assert.IsTrue(Test(MyFieldBuilder.Internal.Protected.Type("int").Name(FieldName), "internal protected int FieldName;"));

            Assert.IsTrue(Test(MyFieldBuilder.Private.Const.Type("int").Name(FieldName).Init(10), "private const int FieldName = 10;"));


            TestContext.Write(_stringBuilder.ToString());
        }
        
        [TestMethod]
        public void TestFields2()
        {
            string FieldName = "FieldName";
            Assert.IsTrue(Test(MyField.String(FieldName).Init("12fg"), "string FieldName = \"12fg\";"));
            Assert.IsTrue(Test(MyField.Int(FieldName).Init(100), "int FieldName = 100;"));
            Assert.IsTrue(Test(MyField.Decimal(FieldName).Init(100), "decimal FieldName = 100;"));
            Assert.IsTrue(Test(MyField.Double(FieldName).Init(100), "double FieldName = 100;"));
            Assert.IsTrue(Test(MyField.Float(FieldName).Init(100), "float FieldName = 100;"));
            Assert.IsTrue(Test(MyField.Boolean(FieldName).Init(false), "bool FieldName = false;"));

            Assert.IsTrue(Test(MyField.Boolean(FieldName).Init(MyCodeExpressionBuilder.Start().), "bool FieldName = false;"));

            TestContext.Write(_stringBuilder.ToString());
            
        }

        private bool Test(MyFieldBuilder builder, string code)
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

        private sealed class TestClass
        {
            
        }
    }
}
