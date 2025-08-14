using SourceCodeBuilder;
using SourceCodeBuilder.Html.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCodeBuilderTest
{
    [TestClass]
    public sealed class TestHtml
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
        public void TestHtml1()
        {
            var html = HtmlBuilder
                .html
                .html_;
            string template = "<html>\r\n</html>\r\n";
            Assert.IsTrue(Test(html, template));
            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestHtml2()
        {
            var html = HtmlBuilder
                .html
                    .html
                    .html_
                .html_;
            string template = "<html>\r\n  <html>\r\n  </html>\r\n</html>\r\n";
            Assert.IsTrue(Test(html, template));
            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestHtml3()
        {
            var html = HtmlBuilder
                .html
                    .html
                        .a
                        .a_
                    .html_
                .html_;
            string template = "<html>\r\n  <html>\r\n    <a>\r\n    </a>\r\n  </html>\r\n</html>\r\n";
            Assert.IsTrue(Test(html, template));
            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestHtml4()
        {
            var html = HtmlBuilder
                .html
                    .A(innerText: "asd").Attribute("class", "testClass").a_
                .html_;
            string template = "<html>\r\n  <a class=\"testClass\">asd</a>\r\n</html>\r\n";
            Assert.IsTrue(Test(html, template));
            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestHtml5()
        {
            var html = HtmlBuilder.A(innerText: "TestInnerText").Attribute("class", "testClass").a_;

            string template = "<a class=\"testClass\">TestInnerText</a>\r\n";

            Assert.IsTrue(Test(html, template));
            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestHtml6()
        {
            var html = HtmlBuilder
                .html
                    .A(_class: "asd", _id: "345", innerText:"test").Attribute("attr1", "value1").a_
                    .div.Attribute("id", "id1").div_
                    .div
                        .a.InnerText("Test1").a_
                        .A("Test2").a_
                        .div.Attribute("id", "id1")
                            .div.Attribute("class", "class121")
                                .div.InnerText("asdas")
                                .div_
                            .div_
                        .div_
                    .div_
                    .div
                        .div.InnerTag(HtmlBuilder.div.div.div_.div_)
                    .div_
                .html_;
            string template = "<html>\r\n  <a class=\"asd\" id=\"345\" attr1=\"value1\">test</a>\r\n  <div id=\"id1\">\r\n  </div>\r\n  <div>\r\n    <a>Test1</a>\r\n    <a>Test2</a>\r\n    <div id=\"id1\">\r\n      <div class=\"class121\">\r\n        <div>asdas</div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n  <div>\r\n    <div>\r\n      <div>\r\n        <div>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</html>\r\n";
            Assert.IsTrue(Test(html, template));
            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestHtml7()
        {
            var innerTag = HtmlBuilder
                    .html
                        .a
                        .a_
                    .html_;

            var html = HtmlBuilder
                .html.InnerTag(innerTag);

            string template = "<html>\r\n  <html>\r\n    <a>\r\n    </a>\r\n  </html>\r\n</html>\r\n";
            Assert.IsTrue(Test(html, template));
            TestContext.Write(_stringBuilder.ToString());
        }

        [TestMethod]
        public void TestHtml8()
        {
            var html = HtmlBuilder
                .html
                    .html
                        .Tag("userTag")
                        .tag_
                    .html_
                .html_;
            string template = "<html>\r\n  <html>\r\n    <userTag>\r\n    </userTag>\r\n  </html>\r\n</html>\r\n";
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
}
