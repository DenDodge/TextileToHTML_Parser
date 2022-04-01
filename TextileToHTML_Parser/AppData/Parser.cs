using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Drawing;
using System;
using System.IO;

namespace TextileToHTML_Parser.AppData
{
    public class Parser
    {
        #region Private Fields

        /// <summary>
        /// Исходныя строка.
        /// </summary>
        private string MainString;

        /// <summary>
        /// Результирующая строка.
        /// </summary>
        private string ResultString;

        /// <summary>
        /// Открытие тегов стандартного HTML.
        /// </summary>
        private Dictionary<string, string> StandartOpeningTags = new Dictionary<string, string>();

        /// <summary>
        /// Закрытие тегов стандартного HTML.
        /// </summary>
        private Dictionary<string, string> StandartClosingTags = new Dictionary<string, string>();

        /// <summary>
        /// Открытие тегов HTML Tessa.
        /// </summary>
        private Dictionary<string, string> TessaOpeningTags = new Dictionary<string, string>();

        /// <summary>
        /// Закрытие тегов HTML Tessa.
        /// </summary>
        private Dictionary<string, string> TessaClosingTags = new Dictionary<string, string>();

        #region Tags Names

        private readonly string BoldItalicTagName = "BoldItalicTag";
        private readonly string BoldTagName = "BoldTag";
        private readonly string ItalicTagName = "ItalicTag";
        private readonly string UnderlineTagName = "UnderlineTag";
        private readonly string CrossedOutTagName = "CrossedOutTag";
        private readonly string NumberedListTagName = "NumberedListTag";
        private readonly string UnNumberedListTagName = "UnNumberedListTag";
        private readonly string ListItemTagName = "ListItemTag";
        private readonly string ParagraphTagName = "ParagraphTag";
        private readonly string PreTagName = "PreTag";
        private readonly string CodeTagName = "CodeTag";

        #endregion

        #region Tags

        private string SpanClosedTag = "</span>";

        #region Standart Tags

        private readonly string StandartBoldItalicTag = "<strong><em>";
        private readonly string StandartBoldTag = "<strong>";
        private readonly string StandartItalicTag = "<em>";
        private readonly string StandartUnderlineTag = "<ins>";
        private readonly string StandartCrossedOutTag = "<del>";
        private readonly string StandartNumberedListTag = "<ol>";
        private readonly string StandartUnNumberedListTag = "<ul>";
        private readonly string StandartListItemTag = "<li>";
        private readonly string StandartParagraphTag = "<p>";
        private readonly string StandartPreTag = "<pre>";
        private readonly string StandartCodeTag = "<code>";

        private readonly string StandartBoldItalicClosedTag = "</strong></em>";
        private readonly string StandartBoldClosedTag = "</strong>";
        private readonly string StandartItalicClosedTag = "</em>";
        private readonly string StandartUnderlineClosedTag = "</ins>";
        private readonly string StandartCrossedOutClosedTag = "</del>";
        private readonly string StandartNumberedListClosedTag = "</ol>";
        private readonly string StandartUnNumberedListClosedTag = "</ul>";
        private readonly string StandartListItemClosedTag = "</li>";
        private readonly string StandartParagraptClosedTag = "</p>";
        private readonly string StandartPreClosedTag = "</pre>";
        private readonly string StandartCodeClosedTag = "</code>";

        private string StandartNewLineTag = "<br />";

        #endregion

        #region Tessa Tags

        private readonly string TessaBoldItalicTag = "</span><span style=\\\"font-weight:bold;font-style:italic;\\\">";
        private readonly string TessaBoldTag = "</span><span style=\\\"font-weight:bold;\\\">";
        private readonly string TessaItalicTag = "</span><span style=\\\"font-style:italic;\\\">";
        private readonly string TessaUnderlineTag = "</span><span style=\\\"text-decoration:underline;\\\">";
        private readonly string TessaCrossedOutTag = "</span><span style=\\\"text-decoration:line-through;\\\">";
        private readonly string TessaNumberedListTag = "</span><ol class=\\\"forum-ol\\\">";
        private readonly string TessaUnNumberedListTag = "</span><ul class=\\\"forum-ul\\\">";
        private readonly string TessaListItemTag = "<li><p><span>";
        private readonly string TessaParagraphTag = "<p><span>";
        private readonly string TessaPreTag = "<div class=\\\"forum-block-monospace\\\"><p><span>";

        private readonly string TessaListItemClosedTag = "</span></p></li>";
        private readonly string TessaParagraphClosedTag = "</span></p>";
        private readonly string TessaPreCloseTag = "</span></p></div>";

        private readonly string TessaNewLineTag = "</span></p><p><span>";

        #endregion

        #endregion

        #region Regex Templates

        /// <summary>
        /// Шаблон регулярного выражения открытия тега заголовка.
        /// </summary>
        private static readonly string headerOpenTagTemplate = @"<h[1-6]>";

        /// <summary>
        /// Шаблон регулярного выражения открытия тега заголовка.
        /// </summary>
        private static readonly string headerCloseTagTemplate = @"<\/h[1-6]>";

        /// <summary>
        /// Шаблон регулярного выражения для тега изображения.
        /// </summary>
        private static readonly string imagesTagTemplate = "<img src=\"(.*?)\" alt=\"\" />";

        private static readonly Regex _headerOpenTag = new Regex(headerOpenTagTemplate,
           RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex _headerCloseTag = new Regex(headerCloseTagTemplate,
           RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex _imagesTag = new Regex(imagesTagTemplate,
            RegexOptions.Multiline);

        #endregion

        #endregion

        #region Constructors

        public Parser(string mainString)
        {
            this.MainString = mainString;

            this.TextileParseString();

            this.InitialStandartTags();
            this.InitialTessaTags();
        }

        #endregion

        #region Public Methods

        public string GetParsedString()
        {
            this.ParseString();

            return ResultString;
        }

        #endregion

        #region PrivateMethods

        #region Initial Methods

        /// <summary>
        /// Инициализация стандартных HTML тегов.
        /// </summary>
        private void InitialStandartTags()
        {
            StandartOpeningTags.Clear();
            StandartClosingTags.Clear();

            // тут важен порядок добавления.
            StandartOpeningTags.Add(ParagraphTagName, StandartParagraphTag);
            StandartOpeningTags.Add(BoldItalicTagName, StandartBoldItalicTag);
            StandartOpeningTags.Add(BoldTagName, StandartBoldTag);
            StandartOpeningTags.Add(ItalicTagName, StandartItalicTag);
            StandartOpeningTags.Add(UnderlineTagName, StandartUnderlineTag);
            StandartOpeningTags.Add(CrossedOutTagName, StandartCrossedOutTag);
            StandartOpeningTags.Add(NumberedListTagName, StandartNumberedListTag);
            StandartOpeningTags.Add(UnNumberedListTagName, StandartUnNumberedListTag);
            StandartOpeningTags.Add(ListItemTagName, StandartListItemTag);
            StandartOpeningTags.Add(PreTagName, StandartPreTag);
            StandartOpeningTags.Add(CodeTagName, StandartCodeTag);

            StandartClosingTags.Add(ParagraphTagName, StandartParagraptClosedTag);
            StandartClosingTags.Add(BoldItalicTagName, StandartBoldItalicClosedTag);
            StandartClosingTags.Add(BoldTagName, StandartBoldClosedTag);
            StandartClosingTags.Add(ItalicTagName, StandartItalicClosedTag);
            StandartClosingTags.Add(UnderlineTagName, StandartUnderlineClosedTag);
            StandartClosingTags.Add(CrossedOutTagName, StandartCrossedOutClosedTag);
            StandartClosingTags.Add(NumberedListTagName, StandartNumberedListClosedTag);
            StandartClosingTags.Add(UnNumberedListTagName, StandartUnNumberedListClosedTag);
            StandartClosingTags.Add(ListItemTagName, StandartListItemClosedTag);
            StandartClosingTags.Add(PreTagName, StandartPreClosedTag);
            StandartClosingTags.Add(CodeTagName, StandartCodeClosedTag);
        }

        /// <summary>
        /// Инициализация HTML тегов TESSA.
        /// </summary>
        private void InitialTessaTags()
        {
            TessaOpeningTags.Clear();
            TessaClosingTags.Clear();

            TessaOpeningTags.Add(ParagraphTagName, TessaParagraphTag);
            TessaOpeningTags.Add(BoldItalicTagName, TessaBoldItalicTag);
            TessaOpeningTags.Add(BoldTagName, TessaBoldTag);
            TessaOpeningTags.Add(ItalicTagName, TessaItalicTag);
            TessaOpeningTags.Add(UnderlineTagName, TessaUnderlineTag);
            TessaOpeningTags.Add(CrossedOutTagName, TessaCrossedOutTag);
            TessaOpeningTags.Add(NumberedListTagName, TessaNumberedListTag);
            TessaOpeningTags.Add(UnNumberedListTagName, TessaUnNumberedListTag);
            TessaOpeningTags.Add(ListItemTagName, TessaListItemTag);
            TessaOpeningTags.Add(PreTagName, TessaPreTag);
            TessaOpeningTags.Add(CodeTagName, TessaPreTag);

            TessaClosingTags.Add(ParagraphTagName, TessaParagraphClosedTag);
            TessaClosingTags.Add(BoldItalicTagName, SpanClosedTag);
            TessaClosingTags.Add(BoldTagName, SpanClosedTag);
            TessaClosingTags.Add(ItalicTagName, SpanClosedTag);
            TessaClosingTags.Add(UnderlineTagName, SpanClosedTag);
            TessaClosingTags.Add(CrossedOutTagName, SpanClosedTag);
            TessaClosingTags.Add(NumberedListTagName, StandartNumberedListClosedTag);
            TessaClosingTags.Add(UnNumberedListTagName, StandartUnNumberedListClosedTag);
            TessaClosingTags.Add(ListItemTagName, TessaListItemClosedTag);
            TessaClosingTags.Add(PreTagName, TessaPreCloseTag);
            TessaClosingTags.Add(CodeTagName, TessaPreCloseTag);
        }

        #endregion

        private void ParseString()
        {
            foreach (var tag in TessaOpeningTags)
            {
                this.ParseTag(tag);
            }


            // удаление лишних символов новой строки.
            this.RemoveSumbolNewString();
            // преобразуем тег <br /> в </p><p>.
            this.ParseNewLineTag();
            // преобразуем код "&#8220" и "&#8221" в символы "\\\"".
            this.ParseQuotesSymbol();
            // преобразуемзаголовки.
            this.ParseHeaderString();

            this.ParseAttachmentImages();

            // установка начала и конца строки.
            this.SetPreAndPostString();
        }

        /// <summary>
        /// Заменяет тег стандартного HTML на тег TESSA HTML.
        /// </summary>
        /// <param name="tag">Ключ и значение стандартного HTML тега.</param>
        private void ParseTag(KeyValuePair<string, string> tag)
        {
            string tagName = tag.Key;
            if (ResultString.Contains(StandartOpeningTags[tag.Key]))
            {
                ResultString = ResultString.Replace(StandartOpeningTags[tagName], TessaOpeningTags[tagName]);
                ResultString = ResultString.Replace(StandartClosingTags[tagName], TessaClosingTags[tagName]);
            }
        }

        /// <summary>
        /// Преобразование тега новой строки из стандартного HTML в TESSA HTML.
        /// </summary>
        private void ParseNewLineTag()
        {
            if (ResultString.Contains(StandartNewLineTag))
            {
                ResultString = ResultString.Replace(StandartNewLineTag, TessaNewLineTag);
            }
        }

        /// <summary>
        /// Преобразование исходной строки в стандартый HTML формат.
        /// </summary>
        private void TextileParseString()
        {
            ResultString = TextileToHTML.TextileFormatter.FormatString(MainString);
        }

        /// <summary>
        /// Установка начала и конца строки.
        /// </summary>
        private void SetPreAndPostString()
        {
            var preString = "{\"Text\":\"<div class=\\\"forum-div\\\">";
            var postString = "</div>\"}";

            ResultString = ResultString.Insert(0, preString);
            ResultString += postString;
        }

        /// <summary>
        /// Удаление лишних символов новой строки.
        /// </summary>
        private void RemoveSumbolNewString()
        {
            var symbolIndex = ResultString.IndexOf('\n');
            while (symbolIndex != -1)
            {
                ResultString = ResultString.Remove(symbolIndex, 1);
                symbolIndex = ResultString.IndexOf('\n');
            }
        }

        /// <summary>
        /// Преобразовать код "&#8220" и "&#8221" в символы "\\\"".
        /// </summary>
        private void ParseQuotesSymbol()
        {
            ResultString = ResultString.Replace("&#8220;", "\\\"");
            ResultString = ResultString.Replace("&#8221;", "\\\"");
        }

        /// <summary>
        /// Преобразование тега <h[1-6]> в жирный 18 шрифт Tessa.
        /// </summary>
        private void ParseHeaderString()
        {
            while (Regex.IsMatch(ResultString, headerOpenTagTemplate, RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled))
            {
                ResultString = _headerOpenTag.Replace(ResultString, "<p><span style=\\\"font-weight:bold;\\\" data-custom-style=\\\"font-size:18;\\\">");
            }
            while (Regex.IsMatch(ResultString, headerCloseTagTemplate, RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled))
            {
                ResultString = _headerCloseTag.Replace(ResultString, "</span></p>");
            }
        }

        private void ParseAttachmentImages()
        {
            var matches = _imagesTag.Matches(ResultString);
            if(matches.Count > 0)
            {
                var fileDirectory = $"D:\\WORK_SYNTELLECT\\OtherFiles\\Migration\\9910\\{matches[0].Groups[1]}";
                var image = Image.FromFile(fileDirectory);
                var resizeImg = (Image)(new Bitmap(image, new Size { Width = image.Width / 3, Height = image.Height / 3 }));

                var Id = new Guid("ea1edecf-0f7d-463f-a013-fde00e0cbc55");
                var caption = Id.ToString().Replace("-", "");
                var uri = $"https:\\\\{caption}";
                var width = image.Size.Width / 3;
                var height = image.Size.Height / 3;

                var startString =
                    "{\"Attachments\":[";

                var captionString =
                    $"{{\"Caption\":\"{caption}\"," +
                    $"\"FileName\":\"\"," +
                    $"\"Uri\":\"{uri}\"," +
                    $"\"ID::uid\":\"{Id}\"," +
                    $"\"MessageID::uid\":\"00000000-0000-0000-0000-000000000000\"," +
                    $"\"StoreMode::int\":0," +
                    $"\"Type::int\":2}}";

                var finishString = "],";

                var fileBytes = File.ReadAllBytes(fileDirectory);
                var base64FileString = Convert.ToBase64String(fileBytes);

                var textString =
                    $"<p><span><img data-custom-style=\\\"width:{width};height:{height};\\\" " +
                    $"name=\\\"{caption}\\\" " +
                    $"src=\\\"data:image/png;base64,{base64FileString}\\\"></span></p>";

                var preString = "\"Text\":\"<div class=\\\"forum-div\\\">";
                var postString = "</div>\"}";

                var resString = startString + captionString + finishString + preString + textString + postString;
            }
        }

        #endregion
    }
}
