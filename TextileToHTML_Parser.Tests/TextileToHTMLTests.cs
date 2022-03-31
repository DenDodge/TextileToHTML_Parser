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

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span></span><span style=\\\"font-weight:bold;\\\">������</span></span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("��������� �����.")]
        public void ItalicString()
        {
            var testString = "_���������_\r\n";
            Parser parser = new Parser(testString);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span></span><span style=\\\"font-style:italic;\\\">���������</span></span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("����������� �����.")]
        public void CrossOutString()
        {
            var testString = "-�����������-\r\n";
            Parser parser = new Parser(testString);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span></span><span style=\\\"text-decoration:line-through;\\\">�����������</span></span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("������������ �����.")]
        public void UnderlineString()
        {
            var testString = "+������������+\r\n";
            Parser parser = new Parser(testString);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span></span><span style=\\\"text-decoration:underline;\\\">������������</span></span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("������ ��������� �����.")]
        public void BoldItelicString()
        {
            var testString = "*_������ ������_*\r\n";
            Parser parser = new Parser(testString);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span></span><span style=\\\"font-weight:bold;font-style:italic;\\\">������ ������</em></strong></span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("��� ����� �� �������. ������ � ����� ������.")]
        public void AllStyleString()
        {
            var testString = "+������������+\r\n*������*\r\n-�����������-\r\n_������_\r\n";
            Parser parser = new Parser(testString);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span></span><span style=\\\"text-decoration:underline;\\\">������������</span></span></p><p><span></span><span style=\\\"font-weight:bold;\\\">������</span></span></p><p><span></span><span style=\\\"text-decoration:line-through;\\\">�����������</span></span></p><p><span></span><span style=\\\"font-style:italic;\\\">������</span></span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("������������ ������.")]
        public void NumberedListString()
        {
            var testString = "# ������� � ���� ������, ��������, ������ ���������� � �������� ������ ���������.\r\n" +
                             "# ������� ������� �����, ����� ����������� ������ � ������ ����. \r\n" +
                             "# ������� ������������ ����� (������� ����� 1).\r\n" +
                             "# ������� ����� ������� ���� �� ��������� � ������ 1 ���������� ��������.\r\n" +
                             "# ������� ������� delete ��� �������� �������� ������.\r\n" +
                             "# ������� delete ��� ���" +
                             "\r\n" +
                             "\r\n";

            Parser parser = new Parser(testString);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"></span>" +
                                "<ol class=\\\"forum-ol\\\">" +
                                "<li><p><span>������� � ���� ������, ��������, ������ ���������� � �������� ������ ���������.</span></p></li>" +
                                "<li><p><span>������� ������� �����, ����� ����������� ������ � ������ ����. </span></p></li>" +
                                "<li><p><span>������� ������������ ����� (������� ����� 1).</span></p></li>" +
                                "<li><p><span>������� ����� ������� ���� �� ��������� � ������ 1 ���������� ��������.</span></p></li>" +
                                "<li><p><span>������� ������� delete ��� �������� �������� ������.</span></p></li>" +
                                "<li><p><span>������� delete ��� ���</span></p></li></ol></div>\"}";

            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("� ���������� ������.")]
        public void WithHeaderString()
        {
            var testString = "�������� �������������� �������� ������ ������� �� �������� (������� ��������� ������)\r\n\r\nh1. ������������\r\n\r\n�������� ����������� � ����� ������ ������.\r\n���� �� ���������.\r\n����� ��������� � ������� ������� �� ��� ����.";

            Parser parser = new Parser(testString);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span>�������� �������������� �������� ������ ������� �� �������� (������� ��������� ������)</span></p><p><span style=\\\"font-weight:bold;\\\" data-custom-style=\\\"font-size:18;\\\">������������</span></p><p><span>�������� ����������� � ����� ������ ������.</span></p><p><span>���� �� ���������.</span></p><p><span>����� ��������� � ������� ������� �� ��� ����.</span></p></div>\"}";

            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }
    }
}
