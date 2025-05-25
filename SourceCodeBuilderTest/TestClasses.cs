using SourceCodeBuilder;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace SourceCodeBuilderTest
{
    [TestClass]
    public sealed class TestClasses
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
        public void TestClasses1()
        {
            string ClassName = "ClassName";

            Assert.IsTrue(Test(MyClass.PublicClass("Service", "IService"), "public class Service : IService\r\n{\r\n}"));
            Assert.IsTrue(Test(MyClass.InternalClass("Service", "IService"), "internal class Service : IService\r\n{\r\n}"));

            Assert.IsTrue(Test(MyClass.PrivateNested.Name("Service"), "private class Service\r\n{\r\n}"));
            Assert.IsTrue(Test(MyClass.ProtectedNested.Name("Service"), "protected class Service\r\n{\r\n}"));
            Assert.IsTrue(Test(MyClass.Internal.Name("Service"), "internal class Service\r\n{\r\n}"));

            Assert.IsTrue(Test(MyClass.Public.Sealed.Name("Service"), "public sealed class Service\r\n{\r\n}"));
            Assert.IsTrue(Test(MyClass.Public.Static.Name("Service"), "public static class Service\r\n{\r\n}"));
            Assert.IsTrue(Test(MyClass.Public.Abstract.Name("Service"), "public abstract class Service\r\n{\r\n}"));

            Assert.IsTrue(Test(MyClass.Public.Name("Service").BaseInterface, "public class Service : IService\r\n{\r\n}"));

            Assert.IsTrue(Test(MyClass.Public.Name("Service").Generic("T"), "public class Service<T>\r\n{\r\n}"));
            Assert.IsTrue(Test(MyClass.Public.Name("Service").Generic("T").Generics("TValue"), "public class Service<T, TValue>\r\n{\r\n}"));
            Assert.IsTrue(Test(MyClass.Public.Name("Service").Generics("T1, T2"), "public class Service<T1, T2>\r\n{\r\n}"));
            Assert.IsTrue(Test(MyClass.Public.Name("Service").Generics("T1", "T2"), "public class Service<T1, T2>\r\n{\r\n}"));
            Assert.IsTrue(Test(MyClass.Public.Name("Service").Generic_T, "public class Service<T>\r\n{\r\n}"));
            Assert.IsTrue(Test(MyClass.Public.Name("Service").Generic_TKey_TValue, "public class Service<TKey, TValue>\r\n{\r\n}"));

            TestContext.Write(_stringBuilder.ToString());
        }
        
        [TestMethod]
        public void TestClasses2()
        {
            var newClass =
                MyClass.PublicClass("Service", "IService")
                    .AddField(MyField.String("Name"))
                    .AddProperty(MyProperty.Int("Value").Static.Init(100))
                    .AddMethod(MyMethod.PublicInt("Calc").Static
                        .Parameter("int", "number")
                        .AddVariable(MyField.Int("constInt").Const.Init(100))
                        .AddLine("return constInt * Value;"));

            Assert.IsTrue(
                Test(
                    newClass
                    , "public class Service : IService\r\n{\r\n    string Name;\r\n    static int Value { get; set; } = 100;\r\n    public static int Calc(int number)\r\n    {\r\n        const int constInt = 100;\r\n        return constInt * Value;\r\n    }\r\n}"
                    ));
            TestContext.Write(_stringBuilder.ToString());
        }

        private bool Test(MyClassBuilder builder, string code)
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
