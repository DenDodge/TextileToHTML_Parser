using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using TextileToHTML;

namespace TextileToHTML_Parser.Tests
{
    [TestClass]
    public class TextileToHTMLTests
    {
        Dictionary<string, Guid> attachemntsIds = new Dictionary<string, Guid>()
        {
            {"1.png", new Guid("1b69540c-d8c4-43e4-8daf-4e578fba9237") },
            {"2.png", new Guid("d3372bf8-4a3b-4a18-ada4-8c646c89c151") },
            {"06.11.12.png", new Guid("b5034117-9246-4997-bb6f-fb2a7131539e") }
        };

        string filesDirectory = $"D:\\WORK_SYNTELLECT\\OtherFiles\\Migration\\1072";

        [TestMethod]
        [TestCategory("Жирный текст.")]
        public void BoldString()
        {
            var testString = "*Жирный*\r\n";
            Parser parser = new Parser(testString, filesDirectory, attachemntsIds);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span></span><span style=\\\"font-weight:bold;\\\">Жирный</span></span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("Курсивный текст.")]
        public void ItalicString()
        {
            var testString = "_Курсивный_\r\n";
            Parser parser = new Parser(testString, filesDirectory, attachemntsIds);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span></span><span style=\\\"font-style:italic;\\\">Курсивный</span></span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("Зачеркнутый текст.")]
        public void CrossOutString()
        {
            var testString = "-Зачеркнутый-\r\n";
            Parser parser = new Parser(testString, filesDirectory, attachemntsIds);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span></span><span style=\\\"text-decoration:line-through;\\\">Зачеркнутый</span></span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("Подчеркнутый текст.")]
        public void UnderlineString()
        {
            var testString = "+Подчеркнутый+\r\n";
            Parser parser = new Parser(testString, filesDirectory, attachemntsIds);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span></span><span style=\\\"text-decoration:underline;\\\">Подчеркнутый</span></span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("Жирный курсивный текст.")]
        public void BoldItelicString()
        {
            var testString = "*_Жирный курсив_*\r\n";
            Parser parser = new Parser(testString, filesDirectory, attachemntsIds);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span></span><span style=\\\"font-weight:bold;font-style:italic;\\\">Жирный курсив</em></strong></span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("Все стили по порядку. Каждый с новой строки.")]
        public void AllStyleString()
        {
            var testString = "+Подчеркнутый+\r\n*Жирный*\r\n-Зачеркнутый-\r\n_Курсив_\r\n";
            Parser parser = new Parser(testString, filesDirectory, attachemntsIds);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span></span><span style=\\\"text-decoration:underline;\\\">Подчеркнутый</span></span></p><p><span></span><span style=\\\"font-weight:bold;\\\">Жирный</span></span></p><p><span></span><span style=\\\"text-decoration:line-through;\\\">Зачеркнутый</span></span></p><p><span></span><span style=\\\"font-style:italic;\\\">Курсив</span></span></p></div>\"}";
            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("Нумерованный список.")]
        public void NumberedListString()
        {
            var testString = "# Зайдите в поле списка, например, список владельцев и выберите одного владельца.\r\n" +
                             "# Нажмите стребку влево, чтобы переместить курсов в начало поля. \r\n" +
                             "# Введите произвольный текст (нажмите цифру 1).\r\n" +
                             "# Нажмите левой кнопкой мыши на выбранное в пункте 1 справочное значение.\r\n" +
                             "# Нажмите клавишу delete для удаление элемента списка.\r\n" +
                             "# Нажмите delete еще раз" +
                             "\r\n" +
                             "\r\n";

            Parser parser = new Parser(testString, filesDirectory, attachemntsIds);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"></span>" +
                                "<ol class=\\\"forum-ol\\\">" +
                                "<li><p><span>Зайдите в поле списка, например, список владельцев и выберите одного владельца.</span></p></li>" +
                                "<li><p><span>Нажмите стребку влево, чтобы переместить курсов в начало поля. </span></p></li>" +
                                "<li><p><span>Введите произвольный текст (нажмите цифру 1).</span></p></li>" +
                                "<li><p><span>Нажмите левой кнопкой мыши на выбранное в пункте 1 справочное значение.</span></p></li>" +
                                "<li><p><span>Нажмите клавишу delete для удаление элемента списка.</span></p></li>" +
                                "<li><p><span>Нажмите delete еще раз</span></p></li></ol></div>\"}";

            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }

        [TestMethod]
        [TestCategory("С заголовком список.")]
        public void WithHeaderString()
        {
            var testString = "Добавить дополнительный параметр поиска убытков по водителю (вкладка страховые данные)\r\n\r\nh1. Спецификация\r\n\r\nПараметр добавляется в общий диалог поиска.\r\nИщем на вхождение.\r\nНужно убедиться в наличии индекса на это поле.";

            Parser parser = new Parser(testString, filesDirectory, attachemntsIds);

            var compareString = "{\"Text\":\"<div class=\\\"forum-div\\\"><p><span>Добавить дополнительный параметр поиска убытков по водителю (вкладка страховые данные)</span></p><p><span style=\\\"font-weight:bold;\\\" data-custom-style=\\\"font-size:18;\\\">Спецификация</span></p><p><span>Параметр добавляется в общий диалог поиска.</span></p><p><span>Ищем на вхождение.</span></p><p><span>Нужно убедиться в наличии индекса на это поле.</span></p></div>\"}";

            var resultString = parser.GetParsedString();

            Assert.AreEqual(compareString, resultString);
        }
    }
}
