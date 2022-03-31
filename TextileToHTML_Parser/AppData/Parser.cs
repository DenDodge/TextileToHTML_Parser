using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// Стандартный HTML теги.
        /// </summary>
        private Dictionary<string, string> StandartTags = new Dictionary<string, string>();

        /// <summary>
        /// HTML теги в TESSA.
        /// </summary>
        private Dictionary<string, string> TessaTags = new Dictionary<string, string>();

        #region Tags Names

        private string BoldStringName = "BoldString";
        private string BoldStringCloseName = "BoldStringClose";

        private string ItalicStringName = "ItalicString";
        private string ItalicStringCloseName = "ItelicStringClose";

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
            this.ParseBoldTags();
            this.ParseItelicTags();

            this.SetPreAndPostString();
            this.RemoveSumbolNewString();

            return ResultString;
        }

        #endregion

        #region PrivateMethods

        #region Initial Methods

        private void InitialStandartTags()
        {
            StandartTags.Clear();
            StandartTags.Add(BoldStringName, "<strong>");
            StandartTags.Add(BoldStringCloseName, "</strong>");
            StandartTags.Add(ItalicStringName, "<em>");
            StandartTags.Add(ItalicStringCloseName, "</em>");
        }

        private void InitialTessaTags()
        {
            TessaTags.Clear();
            TessaTags.Add(BoldStringName, "<span style=\\\"font-weight:bold;\\\">");
            TessaTags.Add(BoldStringCloseName, "</span>");
            TessaTags.Add(ItalicStringName, "<span style=\\\"font-style:italic;\\\">");
            TessaTags.Add(ItalicStringCloseName, "</span>");
        }

        #endregion

        /// <summary>
        /// Обработка жирного текста.
        /// Обрабтка тега <strong>.
        /// </summary>
        private void ParseBoldTags()
        {
            string tagOpenName = BoldStringName;
            if (ResultString.Contains(StandartTags[tagOpenName]))
            {
                string tagCloseName = BoldStringCloseName;
                ResultString = ResultString.Replace(StandartTags[tagOpenName], TessaTags[tagOpenName]);
                ResultString = ResultString.Replace(StandartTags[tagCloseName], TessaTags[tagCloseName]);
            }
        }

        /// <summary>
        /// Обработка курсивного текста.
        /// Обрабтка тега <em>.
        /// </summary>
        private void ParseItelicTags()
        {
            string tagOpenName = ItalicStringName;
            if (ResultString.Contains(StandartTags[tagOpenName]))
            {
                string tagCloseName = ItalicStringCloseName;
                ResultString = ResultString.Replace(StandartTags[tagOpenName], TessaTags[tagOpenName]);
                ResultString = ResultString.Replace(StandartTags[tagCloseName], TessaTags[tagCloseName]);
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

        #endregion
    }
}
