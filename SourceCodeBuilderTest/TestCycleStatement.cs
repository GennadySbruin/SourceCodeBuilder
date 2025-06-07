using SourceCodeBuilder;
using System.Text;

namespace SourceCodeBuilderTest;

[TestClass]
public class TestCycleStatement
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
    public void TestCycleMethod1()
    {
        string example = "for (int i = 0; i < 100; i++)\r\n{\r\n    Console.WriteLine(\"i\");\r\n}";

        var generated =
            MyCodeExpressionBuilder.Start()
            .For("int i = 0; i < 100; i++")
                .AddLine("Console.WriteLine(\"i\");")
            .EndCycle;

        Assert.IsTrue(Test(generated, example));
        TestContext.Write(_stringBuilder.ToString());
    }

    [TestMethod]
    public void TestCycleMethod2()
    {
        string example = "foreach (var name in names)\r\n{\r\n    Console.WriteLine(\"name\");\r\n}";

        var generated =
            MyCodeExpressionBuilder.Start()
            .Foreach("var name in names")
                .AddLine("Console.WriteLine(\"name\");")
            .EndCycle;

        Assert.IsTrue(Test(generated, example));
        TestContext.Write(_stringBuilder.ToString());
    }

    [TestMethod]
    public void TestCycleMethod3()
    {
        string example = "foreach (var name in names)\r\n{\r\n    Console.WriteLine(\"name\");\r\n}";

        var generated =
            MyCodeExpressionBuilder.Start()
            .Foreach("var", "name", "names")
                .AddLine("Console.WriteLine(\"name\");")
            .EndCycle;

        Assert.IsTrue(Test(generated, example));
        TestContext.Write(_stringBuilder.ToString());
    }

    [TestMethod]
    public void TestCycleMethod4()
    {
        string example = "while (true)\r\n{\r\n    Console.WriteLine(\"name\");\r\n}";

        var generated =
            MyCodeExpressionBuilder.Start()
            .While("true")
                .AddLine("Console.WriteLine(\"name\");")
            .EndCycle;

        Assert.IsTrue(Test(generated, example));
        TestContext.Write(_stringBuilder.ToString());
    }

    [TestMethod]
    public void TestCycleMethod5()
    {
        string example = "do\r\n{\r\n    Console.WriteLine(\"x\");\r\n} while (true == true);";

        var generated =
            MyCodeExpressionBuilder.Start()
            .Do
                .AddLine("Console.WriteLine(\"x\");")
                .While("true == true");

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
