using SourceCodeBuilder;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace SourceCodeBuilderTest
{
    [TestClass]
    public sealed class TestTryStatement
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
        public void TestTryStatement1()
        {
            string example = "try\r\n{\r\n}\r\ncatch (Exception ex)\r\n{\r\n}";

            var generated =
                MyCodeExpressionBuilder.Start()
                .Try
                .Catch(nameof(Exception), "ex")
                .EndTry;
            Assert.IsTrue(Test(generated, example));

            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestTryStatement2()
        {
            string example = "try\r\n{\r\n    //try block\r\n    int Value = 1 / 0;\r\n}\r\ncatch (Exception ex)\r\n{\r\n}";

            var generated =
                MyCodeExpressionBuilder.Start()
                .Try
                    .AddLine("//try block")
                    .AddVariable(MyField.Int("Value").Init("1 / 0"))
                .Catch(nameof(Exception), "ex")
                .EndTry;
            Assert.IsTrue(Test(generated, example));

            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestTryStatement3()
        {
            string example = "try\r\n{\r\n}\r\ncatch (Exception ex)\r\n{\r\n    //catch block\r\n    int Value = 1 / 0;\r\n}";

            var generated =
                MyCodeExpressionBuilder.Start()
                .Try
                .Catch(nameof(Exception), "ex")
                    .AddLine("//catch block")
                    .AddVariable(MyField.Int("Value").Init("1 / 0"))
                .EndTry;
            Assert.IsTrue(Test(generated, example));

            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestTryStatement4()
        {
            string example = "try\r\n{\r\n}\r\nfinaly\r\n{\r\n}";

            var generated =
                MyCodeExpressionBuilder.Start()
                .Try
                .Finaly
                .EndTry;
            Assert.IsTrue(Test(generated, example));

            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestTryStatement5()
        {
            string example = "try\r\n{\r\n}\r\nfinaly\r\n{\r\n    //finaly block\r\n    int Value = 1 / 0;\r\n}";

            var generated =
                MyCodeExpressionBuilder.Start()
                .Try
                .Finaly
                    .AddLine("//finaly block")
                    .AddVariable(MyField.Int("Value").Init("1 / 0"))
                .EndTry;
            Assert.IsTrue(Test(generated, example));

            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestTryStatement6()
        {
            string example = "try\r\n{\r\n    //try block\r\n    int Value = 1 / 0;\r\n}\r\ncatch (DivideByZeroException divideByZeroEx)\r\n{\r\n    //catch block DivideByZeroException\r\n}\r\ncatch (Exception ex)\r\n{\r\n    //catch block\r\n}\r\nfinaly\r\n{\r\n    //finaly block\r\n    int Value = 1 / 0;\r\n}";

            var generated =
                MyCodeExpressionBuilder.Start()
                .Try
                    .AddLine("//try block")
                    .AddVariable(MyField.Int("Value").Init("1 / 0"))
                .Catch(nameof(DivideByZeroException), "divideByZeroEx")
                    .AddLine("//catch block DivideByZeroException")
                .Catch(nameof(Exception), "ex")
                    .AddLine("//catch block")
                .Finaly
                    .AddLine("//finaly block")
                    .AddVariable(MyField.Int("Value").Init("1 / 0"))
                .EndTry;
            Assert.IsTrue(Test(generated, example));

            TestContext.Write(_stringBuilder.ToString());
        }

        private bool Test(IExpressionBuilder builder, string code)
        {
            string result = builder.ToString();
            _stringBuilder.AppendLine(result);
            _stringBuilder.AppendLine();
            if (result == code)
            {
                return true;
            }
            else
            {
                TestContext.Write($"{result} {Environment.NewLine}{Environment.NewLine}!={Environment.NewLine}{Environment.NewLine}{code}");
                return false;
            }
            
        }
    }
}
