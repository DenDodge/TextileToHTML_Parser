using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Drawing;
using System;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TextileToHTML
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
        /// Строка прикрепленных файлов к сообщению.
        /// </summary>
        private string AttachmentsString;

        /// <summary>
        /// Дирректория нахождения экспортированного инцидента.
        /// </summary>
        private string IssueDirectory;

        /// <summary>
        /// Флаг, обозначающий преобразование текста в топике.
        /// </summary>
        private bool IsTopic = false;

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

        /// <summary>
        /// Список прикрепленных файлов.
        /// </summary>
        private Dictionary<string, Guid> AttachmentsFiles = new Dictionary<string, Guid>();

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

        private static readonly string preCodeTagsTemplates = "<pre><code .*?>";


        private static readonly Regex _headerOpenTag = new Regex(headerOpenTagTemplate,
           RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex _headerCloseTag = new Regex(headerCloseTagTemplate,
           RegexOptions.Singleline | RegexOptions.Compiled);

        private static readonly Regex _imagesTag = new Regex(imagesTagTemplate,
            RegexOptions.Multiline);

        private static readonly Regex _preCodeTag = new Regex(preCodeTagsTemplates,
            RegexOptions.Singleline | RegexOptions.Compiled);

        #endregion

        #endregion

        #region Constructors

        public Parser(
            string mainString,
            string issueDirectory,
            Dictionary<string, Guid> attachmentsFiles,
            bool isTopic = false)
        {
            this.MainString = mainString;
            this.AttachmentsString = "";
            this.AttachmentsFiles = attachmentsFiles;
            this.IssueDirectory = issueDirectory;
            this.IsTopic = isTopic;

            this.TextileParseString();

            this.InitialStandartTags();
            this.InitialTessaTags();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Получить преобразованную строку.
        /// </summary>
        /// <returns>Преобразовання строка.</returns>
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

        /// <summary>
        /// Преобразовать строку, переданную в конструкторе.
        /// </summary>
        private void ParseString()
        {
            this.ParseSlashesSyblol();

            foreach (var tag in TessaOpeningTags)
            {
                this.ParseTag(tag);
            }

            // удаление лишних символов новой строки.
            this.RemoveSumbolNewString();
            // преобразуем тег <br /> в </p><p>.
            this.ParseNewLineTag();

            if (!IsTopic)
            {
                // преобразуем код "&#8220" и "&#8221" в символы "\\\"".
                this.ParseQuotesSymbol();
            }

            // преобразуем заголовки.
            this.ParseHeaderString();

            while (this.TryGetMathes(_imagesTag, out MatchCollection matches))
            {
                this.ParseAttachmentsImages(matches[0]);
            }

            if (IsTopic)
            {
                this.RemoveSlachSyblol();
            }

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
            this.ParsePreCodeTasg();
            // преобразуем символы "<" и ">" в символы "&lt;" и "&gt;".
            MainString = MainString.Replace("<", @"&lt;");
            MainString = MainString.Replace(">", @"&gt;");
            // преобразуем собсвенные в теги <code>.
            MainString = MainString.Replace("@code", "<code>");
            MainString = MainString.Replace("@/code", "</code>");
            // преобразуем строку в HTML.
            ResultString = TextileToHTML.TextileFormatter.FormatString(MainString);
            // подчищаем символ "&amp;", которые сгенирировался в процессе преобразования textile в HTML.
            ResultString = ResultString.Replace("&amp;", "&");
        }

        /// <summary>
        /// Установка начала и конца строки.
        /// </summary>
        private void SetPreAndPostString()
        {
            if (AttachmentsString != "" && AttachmentsString != "{")
            {
                var preAttachmentString = "{\"Attachments\":[";
                var postAttachmentString = "],";

                AttachmentsString = $"{preAttachmentString}{AttachmentsString}{postAttachmentString}";
            }
            else
            {
                AttachmentsString = "{";
            }

            var preString = $"{AttachmentsString}\"Text\":\"<div class=\\\"forum-div\\\">";
            var postString = "</div>\"}";

            if (IsTopic)
            {
                preString = "<div class=\"forum-div\">";
                postString = "</div>";
            }

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
        /// Преобзазовывает один символ "\" в два символа "\\".
        /// </summary>
        private void ParseSlashesSyblol()
        {
            ResultString = ResultString.Replace(@"\", @"\\");
        }

        private void RemoveSlachSyblol()
        {
            ResultString = ResultString.Replace("\\\"", "\"");
        }

        /// <summary>
        /// Преобразование тега "h[1-6]" в жирный 18 шрифт Tessa.
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

        /// <summary>
        /// Получить совпадения с шаблоном regex в в строке.
        /// </summary>
        /// <param name="_regex">Регулярное выражение.</param>
        /// <param name="matchCollection">Списко совпадений с шаблоном.</param>
        /// <returns>Истина - удалось получить.</returns>
        private bool TryGetMathes(Regex _regex, out MatchCollection matchCollection)
        {
            matchCollection = _regex.Matches(ResultString);
            if (matchCollection.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Преобразование строк с прикрепленными изображениями.
        /// </summary>
        /// <param name="matchImages">Совпадение с шаблоном регулярного выражения.</param>
        private void ParseAttachmentsImages(Match matchImages)
        {
            string fileName = matchImages.Groups[1].Value;
            var fileDirectory = $"{this.IssueDirectory}\\{fileName}";
            if (!IsTopic)
            {
                this.GenerateAttachemntsString(fileName);
            }
            this.ParseAttachmentImage(fileDirectory, fileName, matchImages);
        }

        /// <summary>
        /// Создает строку Attachments.
        /// </summary>
        /// <param name="fileName">Имя файла.</param>
        private void GenerateAttachemntsString(string fileName)
        {
            var id = this.AttachmentsFiles[fileName];
            var caption = id.ToString().Replace("-", "");
            var uri = $"https:\\\\{caption}";

            if (this.AttachmentsString != "")
            {
                this.AttachmentsString += ",";
            }

            this.AttachmentsString +=
                    $"{{\"Caption\":\"{caption}\"," +
                    $"\"FileName\":\"\"," +
                    $"\"Uri\":\"{uri}\"," +
                    $"\"ID::uid\":\"{id}\"," +
                    $"\"MessageID::uid\":\"00000000-0000-0000-0000-000000000000\"," +
                    $"\"StoreMode::int\":0," +
                    $"\"Type::int\":2}}";
        }

        /// <summary>
        /// Преобразуем строку с прикрепленными изображениями.
        /// </summary>
        /// <param name="fileDirectory">Расоложение файла.</param>
        /// <param name="fileName">Имя файла.</param>
        /// <param name="match">Совпадение с шаблоном регулярного выражения.</param>
        private void ParseAttachmentImage(
            string fileDirectory,
            string fileName,
            Match match)
        {
            var id = this.AttachmentsFiles[fileName];
            var caption = id.ToString().Replace("-", "");
            var mainImage = Image.FromFile(fileDirectory);
            var resizeImage = this.ResizeImage(mainImage, (int)(mainImage.Width * 0.3), (int)(mainImage.Height * 0.3));

            var base64FileString = this.GetBase64StringFromInage(resizeImage);

            var textString =
                $"<p><span><img data-custom-style=\\\"width:{resizeImage.Width};height:{resizeImage.Height};\\\" " +
                $"name=\\\"{caption}\\\" " +
                $"src=\\\"data:image/png;base64,{base64FileString}\\\"></span></p>";

            ResultString = ResultString.Remove(match.Index, match.Length);
            ResultString = ResultString.Insert(match.Index, textString);
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        /// <summary>
        /// Получить строку Base64 из изображения.
        /// </summary>
        /// <param name="image">Изображение.</param>
        /// <returns>Строка Base64.</returns>
        private string GetBase64StringFromInage(Image image)
        {
            string base64String = null;

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                base64String = Convert.ToBase64String(ms.ToArray());
            }

            return base64String;
        }

        /// <summary>
        /// Привести теги "pre code" и "/code /pre".
        /// </summary>
        private void ParsePreCodeTasg()
        {
            // для корректного преобразования символов сравнения используем собственные теги.
            while (Regex.IsMatch(MainString, preCodeTagsTemplates, RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline | RegexOptions.Compiled))
            {
                MainString = _preCodeTag.Replace(MainString, "@code");
            }
            MainString = MainString.Replace("</code></pre>", "@/code");
            MainString = MainString.Replace("<pre>", "@code");
            MainString = MainString.Replace("</pre>", "@/code");
        }

        #endregion
    }
}
