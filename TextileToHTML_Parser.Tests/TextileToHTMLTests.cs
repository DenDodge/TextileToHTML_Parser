using Microsoft.VisualStudio.TestTools.UnitTesting;
using TextileToHTML_Parser.AppData;

namespace TextileToHTML_Parser.Tests
{
    [TestClass]
    public class TextileToHTMLTests
    {
        [TestMethod]
        [TestCategory("Жирный текст.")]
        public void BoldString()
        {
            var testString = "*Жирный*\r\n";
            Parser parser = new Parser(testString);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span style=\\\"font-weight:bold;\\\">Жирный</span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("Курсивный текст.")]
        public void ItalicString()
        {
            var testString = "_Курсивный_\r\n";
            Parser parser = new Parser(testString);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span style=\\\"font-style:italic;\\\">Курсивный</span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }
    }
}
