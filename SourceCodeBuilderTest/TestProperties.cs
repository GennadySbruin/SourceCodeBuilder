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

        [TestMethod]
        public void TestProperties2()
        {
            string PropertyName = "PropertyName";
            Assert.IsTrue(Test(MyProperty.String(PropertyName).Init("12fg"), "string PropertyName { get; set; } = \"12fg\";"));
            Assert.IsTrue(Test(MyProperty.Int(PropertyName).Init(100), "int PropertyName { get; set; } = 100;"));
            Assert.IsTrue(Test(MyProperty.Decimal(PropertyName).Init(100), "decimal PropertyName { get; set; } = 100;"));
            Assert.IsTrue(Test(MyProperty.Double(PropertyName).Init(100), "double PropertyName { get; set; } = 100;"));
            Assert.IsTrue(Test(MyProperty.Float(PropertyName).Init(100), "float PropertyName { get; set; } = 100;"));
            Assert.IsTrue(Test(MyProperty.Boolean(PropertyName).Init(false), "bool PropertyName { get; set; } = false;"));

            Assert.IsTrue(
                Test(
                    MyProperty.Int(PropertyName).Init(MyCodeExpressionBuilder.Start()
                    .Add(3)._.Multiply._.Add(100))
                    , "int PropertyName { get; set; } = 3 * 100;"));

            TestContext.Write(_stringBuilder.ToString());
        }


        [TestMethod]
        public void TestProperties3()
        {
            string PropertyName = "PropertyName";

            Assert.IsTrue(
                Test(
                    MyProperty.Int(PropertyName).GetOnly
                    , "int PropertyName { get; }"));
            Assert.IsTrue(
                Test(
                    MyProperty.Int(PropertyName).SetOnly
                    , "int PropertyName { set; }"));

            Assert.IsTrue(
                 Test(
                     MyProperty.Int(PropertyName).GetOnly.GetterExpression(" get /*cats*/ { return _count;}")
                     , "int PropertyName { get /*cats*/ { return _count;} }"));
            Assert.IsTrue(
                 Test(
                     MyProperty.Int(PropertyName).SetOnly.SetterExpression(" set /*cats*/ { _count = value;}")
                     , "int PropertyName { set /*cats*/ { _count = value;} }"));
            Assert.IsTrue(
                 Test(
                     MyProperty.Int(PropertyName)
                     .GetterExpression(" get /*cats*/ { return _count;}")
                     .SetterExpression(" set /*cats*/ { _count = value;}")
                     , "int PropertyName { get /*cats*/ { return _count;} set /*cats*/ { _count = value;} }"));
            Assert.IsTrue(
                 Test(
                     MyProperty.Int(PropertyName)
                     .GetterExpression(MyCodeExpressionBuilder.Start()
                     .NewLine.Tab.Add("get")
                         .NewLine.Tab.CodeBlock
                            .NewLine.Tab.Tab.Add(" return _count;")
                         .NewLine.Tab.FinishCodeBlock
                     .NewLine)
                     .SetterExpression(" set /*cats*/ { _count = value;}")
                     , "int PropertyName {\r\n    get\r\n    {\r\n         return _count;\r\n    }\r\n set /*cats*/ { _count = value;} }"));
            Assert.IsTrue(
                 Test(
                     MyProperty.Int(PropertyName)
                     .ExpandGetterSetter(
                         "return _count;",
                         "_count = 1value;\r\n        _count++;")
                     , "int PropertyName\r\n{\r\n    get\r\n    {\r\n        return _count;\r\n    }\r\n    set\r\n    {\r\n        _count = 1value;\r\n        _count++;\r\n    }\r\n}"));
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


    }
}
