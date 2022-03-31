using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        private readonly string StandartBoldItalicClosedTag = "</strong></em>";
        private readonly string StandartBoldClosedTag = "</strong>";
        private readonly string StandartItalicClosedTag = "</em>";
        private readonly string StandartUnderlineClosedTag = "</ins>";
        private readonly string StandartCrossedOutClosedTag = "</del>";
        private readonly string StandartNumberedListClosedTag = "</ol>";
        private readonly string StandartUnNumberedListClosedTag = "</ul>";
        private readonly string StandartListItemClosedTag = "</li>";
        private readonly string StandartParagraptClosedTag = "</p>";

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

        private readonly string TessaListItemClosedTag = "</span></p></li>";
        private readonly string TessaParagraphClosedTag = "</span></p>";

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

        private static readonly Regex _headerOpenTag = new Regex(headerOpenTagTemplate,
           RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex _headerCloseTag = new Regex(headerCloseTagTemplate,
           RegexOptions.Singleline | RegexOptions.Compiled);

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

            StandartClosingTags.Add(ParagraphTagName, StandartParagraptClosedTag);
            StandartClosingTags.Add(BoldItalicTagName, StandartBoldItalicClosedTag);
            StandartClosingTags.Add(BoldTagName, StandartBoldClosedTag);
            StandartClosingTags.Add(ItalicTagName, StandartItalicClosedTag);
            StandartClosingTags.Add(UnderlineTagName, StandartUnderlineClosedTag);
            StandartClosingTags.Add(CrossedOutTagName, StandartCrossedOutClosedTag);
            StandartClosingTags.Add(NumberedListTagName, StandartNumberedListClosedTag);
            StandartClosingTags.Add(UnNumberedListTagName, StandartUnNumberedListClosedTag);
            StandartClosingTags.Add(ListItemTagName, StandartListItemClosedTag);
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

            TessaClosingTags.Add(ParagraphTagName, TessaParagraphClosedTag);
            TessaClosingTags.Add(BoldItalicTagName, SpanClosedTag);
            TessaClosingTags.Add(BoldTagName, SpanClosedTag);
            TessaClosingTags.Add(ItalicTagName, SpanClosedTag);
            TessaClosingTags.Add(UnderlineTagName, SpanClosedTag);
            TessaClosingTags.Add(CrossedOutTagName, SpanClosedTag);
            TessaClosingTags.Add(NumberedListTagName, StandartNumberedListClosedTag);
            TessaClosingTags.Add(UnNumberedListTagName, StandartUnNumberedListClosedTag);
            TessaClosingTags.Add(ListItemTagName, TessaListItemClosedTag);
        }

        #endregion

        private void ParseString()
        {
            foreach(var tag in TessaOpeningTags)
            {
                this.ParseTag(tag);
            }

            // установка начала и конца строки.
            this.SetPreAndPostString();
            // удаление лишних символов новой строки.
            this.RemoveSumbolNewString();
            // преобразуем тег <br /> в </p><p>.
            this.ParseNewLineTag();
            // преобразуем код "&#8220" и "&#8221" в символы "\\\"".
            this.ParseQuotesSymbol();

            this.ParseHeaderString();
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
            if(ResultString.Contains(StandartNewLineTag))
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
            while(symbolIndex != -1)
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
            while(Regex.IsMatch(ResultString, headerCloseTagTemplate, RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled))
            {
                ResultString = _headerCloseTag.Replace(ResultString, "</span></p>");
            }
        }

        #endregion
    }
}
