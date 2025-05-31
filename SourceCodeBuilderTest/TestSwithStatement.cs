using SourceCodeBuilder;
using System.Text;

namespace SourceCodeBuilderTest;

[TestClass]
public class TestSwithStatement
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
    public void TestSwitchMethod1()
    {
        string example = "switch (value)\r\n{\r\n    case option1:\r\n        Console.WriteLine(\"option 1\");\r\n        break;\r\n\r\n}";

        var generated =
            MyCodeExpressionBuilder.Start()
            .Switch("value")
                .Case("option1")
                    .AddLine("Console.WriteLine(\"option 1\");")
                    .Break
                .EndSwitch;

        Assert.IsTrue(Test(generated, example));
        TestContext.Write(_stringBuilder.ToString());
    }

    [TestMethod]
    public void TestSwitchMethod2()
    {
        string example = "switch (value)\r\n{\r\n    case option1:\r\n        Console.WriteLine(\"option 1_1\");\r\n        Console.WriteLine(\"option 1_2\");\r\n        break;\r\n\r\n    case option2:\r\n        Console.WriteLine(\"option 2\");\r\n        break;\r\n\r\n    default:\r\n        Console.WriteLine(\"default\");\r\n        break;\r\n}";

        var generated =
            MyCodeExpressionBuilder.Start()
            .Switch("value")
                .Case("option1")
                    .AddLine("Console.WriteLine(\"option 1_1\");")
                    .AddLine("Console.WriteLine(\"option 1_2\");")
                    .Break
                .Case("option2")
                    .AddLine("Console.WriteLine(\"option 2\");")
                    .Break
                .Default
                    .AddLine("Console.WriteLine(\"default\");")
                    .Break
                .EndSwitch;

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
