using SourceCodeBuilder.Html.Expressions;
using System.Text;

namespace SourceCodeBuilderTest;

[TestClass]
public class TestRazor
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
    public void TestRazor1()
    {
        var html = HtmlBuilder
                .html.
                    @if.Attribute("", "true == true")
                        .a
                        .a_
                    .if_
                .html_;
        string template = "<html>\r\n  @if(true == true)\r\n  {\r\n    <a/>\r\n  }\r\n</html>\r\n";
        Assert.IsTrue(Test(html, template));
        TestContext.Write(_stringBuilder.ToString());
    }

    [TestMethod]
    public void TestRazor2()
    {
        var html = HtmlBuilder
                .html.
                    @If("", "true == true")
                        .a
                        .a_
                    .if_
                .html_;
        string template = "<html>\r\n  @if(true == true)\r\n  {\r\n    <a/>\r\n  }\r\n</html>\r\n";
        Assert.IsTrue(Test(html, template));
        TestContext.Write(_stringBuilder.ToString());
    }
    [TestMethod]
    public void TestRazor3()
    {
        var html = HtmlBuilder
                .html.
                    @If("  <a>\r\n  </a>", "true == true")
                    .if_
                .html_;
        string template = "<html>\r\n  @if(true == true)\r\n  {\r\n    <a>\r\n    </a>\r\n  }\r\n</html>\r\n";
        Assert.IsTrue(Test(html, template));
        TestContext.Write(_stringBuilder.ToString());
    }

    [TestMethod]
    public void TestRazor4()
    {
        var html = HtmlBuilder
                .html
                    .Foreach("", "var value in values")
                        .a
                        .a_
                    .foreach_
                .html_;
        string template = "<html>\r\n  @foreach(var value in values)\r\n  {\r\n    <a/>\r\n  }\r\n</html>\r\n";
        Assert.IsTrue(Test(html, template));
        TestContext.Write(_stringBuilder.ToString());
    }

    [TestMethod]
    public void TestRazor5()
    {
        var html = HtmlBuilder.@razorpage
                .page.InnerText("test").page_
                .html
                    .a
                    .a_
                .html_
            .razorpage_;
        string template = "@page test\r\n<html>\r\n  <a/>\r\n</html>\r\n\r\n";
        Assert.IsTrue(Test(html, template));
        TestContext.Write(_stringBuilder.ToString());
    }

    [TestMethod]
    public void TestRazor6()
    {
        var html = HtmlBuilder.@razorpage
                .page.InnerText("test").page_
                .html
                    .If("", "1 == 1")
                        .a
                        .a_
                    .if_
                    .@else
                        .div
                        .div_
                    .else_
                .html_
            .razorpage_;
        string template = "@page test\r\n<html>\r\n  @if(1 == 1)\r\n  {\r\n    <a/>\r\n  }\r\n  else\r\n  {\r\n    <div/>\r\n  }\r\n</html>\r\n\r\n";
        Assert.IsTrue(Test(html, template));
        TestContext.Write(_stringBuilder.ToString());
    }

    [TestMethod]
    public void TestRazor7()
    {
        var html = HtmlBuilder.@razorpage
                .page.InnerText("\"/admin/users\"").page_
                .code.InnerText("test code").code_
                .a.InnerText("test a").a_
                
                .razorcodeblock.InnerText("test code block").razorcodeblock_
            .razorpage_;
        string template = "@page \"/admin/users\"\r\n<code>test code</code>\r\n<a>test a</a>\r\n@code\r\n{\r\ntest code block\r\n}\r\n\r\n";
        Assert.IsTrue(Test(html, template));
        TestContext.Write(_stringBuilder.ToString());
    }
    private bool Test(HtmlBuilder builder, string code)
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
