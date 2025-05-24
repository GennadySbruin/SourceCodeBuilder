using SourceCodeBuilder;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace SourceCodeBuilderTest
{
    [TestClass]
    public sealed class TestIfStatement
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
        public void TestIfStatement1()
        {
            string example = "if (1 == 2)\r\n{\r\n}";

            var generated =
                MyCodeExpressionBuilder.Start()
                .If("1 == 2")
                    .CodeBlock
                    .EndIf;

            Assert.IsTrue(Test(generated, example));


            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestIfStatement2()
        {
            string example = "if (1 == 2)\r\n{\r\n    int c = 4;\r\n}";

            var generated =
                MyCodeExpressionBuilder.Start()
                .If("1 == 2")
                    .CodeBlock
                        .AddLine("int c = 4;")
                    .EndIf;

            Assert.IsTrue(Test(generated, example));


            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestIfStatement3()
        {
            string example = "if (33 == 44 && 44 == 44)\r\n{\r\n}\r\nelse if (1 == 1)\r\n{\r\n}";

            var generated =
                MyCodeExpressionBuilder.Start(0)
                .If_.Condition("33 == 44").And("44 == 44")
                    .CodeBlock
                .ElseIf("1 == 1")
                    .CodeBlock
                .EndIf;

            Assert.IsTrue(Test(generated, example));
            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestIfStatement4()
        {
            string? example =
                MyCodeExpressionBuilder.Start(0)
                .If_.Condition("33 == 44").And("44 == 44")
                    .CodeBlock
                .ElseIf_.Condition("1 == 1")
                    .CodeBlock
                .EndIf.ToString();

            var generated =
                MyCodeExpressionBuilder.Start(0)
                .If_.Condition("33 == 44").And("44 == 44")
                .CodeBlock
                .ElseIf("1 == 1")
                .CodeBlock
                .EndIf;

            Assert.IsTrue(Test(generated, example));
            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestIfStatement5()
        {
            string example = "if (1 == 2 && 4 == 5)\r\n{\r\n    if (5 == 1)\r\n    { }\r\n}\r\nif (1 == 2 && 4 == 5)\r\n{\r\n    if (5 == 1)\r\n    { }\r\n    if (3==3)\r\n    {\r\n    }\r\n}\r\nelse if (3 == 5 || 45 != 56)\r\n{\r\n    var c = 67\r\n}\r\nelse if (1 == 15)\r\n{\r\n    var c = 67\r\n}\r\nelse\r\n{\r\n    int c = 4;\r\n}";
            var generated =
                MyCodeExpressionBuilder.Start(0)
                .If("1 == 2").And("4 == 5")
                    .CodeBlock
                        .AddLine("if (5 == 1)")
                        .AddLine("{ }")
                    .EndIf
                .NewLine
                .If_.Condition("1 == 2").And("4 == 5")
                    .CodeBlock
                        .AddLine("if (5 == 1)")
                        .AddLine("{ }")
                        .AddCode(MyCodeExpressionBuilder.Start(1)
                            .NewLine.If("3==3")
                                .CodeBlock
                                .EndIf)
                    .ElseIf_.Condition("3 == 5").Or2("45 != 56")
                    .CodeBlock
                        .AddLine("var c = 67")
                    .ElseIf("1 == 15")
                    .CodeBlock
                        .AddLine("var c = 67")
                    .Else
                        .AddLine("int c = 4;")
                    .EndIf;
            
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
