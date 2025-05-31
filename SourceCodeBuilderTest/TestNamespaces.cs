using SourceCodeBuilder;
using System.Text;

namespace SourceCodeBuilderTest;

[TestClass]
public class TestNamespaces
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
    public void TestMethod1()
    {
        var newNamespace = MyNamespaceBuilder.FileName("Test.g.cs")
            .AddUsing("System")
            .AddUsing("System.Text")
            .AddClass(MyClassBuilder.Public.Name("MyClass").BaseInterface
                .AddField(MyField.PublicInt("Count"))
                .AddProperty(MyProperty.PublicInt("IntArr").Static.Array)
                .AddMethod(MyMethod.Int("Method1").Static.AddLine("return 1;")))
            .AddInterface(MyInterfaceBuilder.Public.Name("IMyClass"));

        Assert.IsTrue(
    Test(
        newNamespace
        , "\r\nusing System;\r\nusing System.Text;\r\n\r\nnamespace \r\n{\r\n    public partial interface IMyClass\r\n    {\r\n    }\r\n    public partial class MyClass : IMyClass\r\n    {\r\n        public int Count;\r\n        public static int[] IntArr { get; set; }\r\n        static int Method1()\r\n        {\r\n            return 1;\r\n        }\r\n    }\r\n}"
        ));
        TestContext.Write(_stringBuilder.ToString());
    }

    [TestMethod]
    public void TestMethod2()
    {
        var newNamespace = MyNamespaceBuilder.FileName(@"C:\Users\Геннадий\Source\Repos\SourceCodeBuilder\SourceCodeBuilderTest\NameSpaceResult.cs")
            .AddUsing("System")
            .AddUsing("System.Text")
            .WithName("MyNamespace.Library")
            .AddClass(MyClassBuilder.Public.Name("MyClass").BaseInterface
                .AddField(MyField.PublicInt("Count"))
                .AddProperty(MyProperty.PublicInt("IntArr").Static.Array.LambdaGetter(" => Method1();"))
                .AddMethod(MyMethod.Int("Method1").Array.Static.AddLine("return [1];")))
            .AddInterface(MyInterfaceBuilder.Public.Name("IMyClass"));

        Assert.IsTrue(newNamespace.Save());
    }

    private bool Test(MyNamespaceBuilder builder, string code)
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
