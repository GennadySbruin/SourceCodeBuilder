using SourceCodeBuilder;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace SourceCodeBuilderTest
{
    [TestClass]
    public sealed class TestMethods
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
        public void TestMethods1()
        {
            string MethodName = "MethodName";
            string MethodTypeName = "string";
            Assert.IsTrue(Test(MyMethod.String(MethodName), "string MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.Int(MethodName), "int MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.Double(MethodName), "double MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.Float(MethodName), "float MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.Boolean(MethodName), "bool MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.Byte(MethodName), "byte MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.DateTime(MethodName), "DateTime MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.Decimal(MethodName), "decimal MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.Guid(MethodName), "Guid MethodName() { }"));

            Assert.IsTrue(Test(MyMethod.PublicString(MethodName), "public string MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.PublicInt(MethodName), "public int MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.PublicDouble(MethodName), "public double MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.PublicFloat(MethodName), "public float MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.PublicBoolean(MethodName), "public bool MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.PublicByte(MethodName), "public byte MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.PublicDateTime(MethodName), "public DateTime MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.PublicDecimal(MethodName), "public decimal MethodName() { }"));
            Assert.IsTrue(Test(MyMethod.PublicGuid(MethodName), "public Guid MethodName() { }"));

            Assert.IsTrue(Test(MyMethodBuilder.Private.Type("int").Name(MethodName), "private int MethodName() { }"));
            Assert.IsTrue(Test(MyMethodBuilder.Internal.Type("double").Name(MethodName), "internal double MethodName() { }"));
            Assert.IsTrue(Test(MyMethodBuilder.Public.Type("Generic<byte>").Name(MethodName), "public Generic<byte> MethodName() { }"));

            Assert.IsTrue(Test(MyMethodBuilder.Public.Type("int").Name(MethodName).Array, "public int[] MethodName() { }"));
            Assert.IsTrue(Test(MyMethodBuilder.Public.Type("int").Name(MethodName).List, "public List<int> MethodName() { }"));

            Assert.IsTrue(Test(MyMethodBuilder.Public.Type("int").Name(MethodName).Static, "public static int MethodName() { }"));
            
            Assert.IsTrue(Test(MyMethodBuilder.Public.Abstract.Type("int").Name(MethodName), "public abstract int MethodName() { }"));
            Assert.IsTrue(Test(MyMethodBuilder.Internal.Protected.Type("int").Name(MethodName), "internal protected int MethodName() { }"));
            Assert.IsTrue(Test(MyMethodBuilder.Internal.Sealed.Type("int").Name(MethodName), "internal sealed int MethodName() { }"));
            Assert.IsTrue(Test(MyMethodBuilder.Public.Virtual.Type("int").Name(MethodName), "public virtual int MethodName() { }"));

            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestMethods2()
        {
            string MethodName = "MethodName";
            Assert.IsTrue(Test(MyMethod.Int(MethodName).AddLine("return 12;")

                , "int MethodName()\r\n{\r\n    return 12;\r\n}"));

            Assert.IsTrue(Test(MyMethod.Int(MethodName)
                .AddLine("var calculated = true;")
                .AddLine("return 12;")

                , "int MethodName()\r\n{\r\n    var calculated = true;\r\n    return 12;\r\n}"));

            Assert.IsTrue(Test(MyMethod.Int(MethodName)
                .AddVariable(MyField.Boolean("calculated").Init(true))
                .AddLine("return 12;")

                , "int MethodName()\r\n{\r\n    bool calculated = true;\r\n    return 12;\r\n}"));

            Assert.IsTrue(Test(MyMethod.Int(MethodName)
                .AddCode(MyCodeExpressionBuilder.Start(0)
                    .NewLine.Tab.Add("return")._.Add(2)._.Multiply._.Add(4).Finish.Build())

                , "int MethodName()\r\n{\r\n    return 2 * 4;\r\n}"));

            TestContext.Write(_stringBuilder.ToString());
        }


        

        private bool Test(MyMethodBuilder builder, string code)
        {
            string result = builder.ToString();
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


    }
}
