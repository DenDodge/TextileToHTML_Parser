using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextileToHTML_Parser.AppData;

namespace TextileToHTML_Parser.Tests
{
    [TestClass]
    public class TextileToHTMLTests
    {
        [TestMethod]
        [TestCategory("������ �����.")]
        public void BoldString()
        {
            var testString = "*������*\r\n";
            Parser parser = new Parser(testString);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span style=\\\"font-weight:bold;\\\">������</span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("��������� �����.")]
        public void ItalicString()
        {
            var testString = "_���������_\r\n";
            Parser parser = new Parser(testString);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span style=\\\"font-style:italic;\\\">���������</span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }
    }
}
