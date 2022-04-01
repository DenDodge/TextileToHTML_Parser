using TextileToHTML_Parser.AppData;

namespace TextileToHTMLToHTML_Parser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string testString = "Ошибка из разряда \"не делайте так\" :)\r\n\r\n" +
                "Пусть есть карточка автомобиль (или любая другая с выпадающим списком, или кнопкой троеточия).\r\n\r\n" +
                "# Зайдите в поле списка, например, список владельцев и выберите одного владельца.\r\n" +
                "# Нажмите стребку влево, чтобы переместить курсов в начало поля. \r\n" +
                "# Введите произвольный текст (нажмите цифру 1).\r\n" +
                "# Нажмите левой кнопкой мыши на выбранное в пункте 1 справочное значение.\r\n" +
                "# Нажмите клавишу delete для удаление элемента списка.\r\n" +
                "# Нажмите delete еще раз\r\n\r\n" +
                "!1.png!\r\n\r\n" +
                "Ожидаемое поведение:\r\n" +
                "- Элемент списка выделяется полностью\r\n" +
                "- Элемент удаляется из поля\r\n" +
                "- Произвольный текс, введенный в пункте 3 стирается\r\n\r\n" +
                "Текущее поведение:\r\n" +
                "- Элемент списка выделяется без первой буквы\r\n" +
                "- Выводится сообщение об ошибке ArgumentOutOfRangeException \r\n" +
                "- После удаления элемента списка и повторном нажатии клавиши delete всегда будет выводиться сообщение об ошибке:\r\n\r\n" +
                "<pre>\r\n" +
                "System.ArgumentOutOfRangeException: Length cannot be less than zero.\r\n" +
                "Parameter name: length\r\n" +
                "   at System.String.Substring(Int32 startIndex, Int32 length)\r\n" +
                "   at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.FiniteElements(Int32& x)\r\n" +
                "   at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.InputText()\r\n" +
                "   at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.Erase(Int32 backspace)\r\n" +
                "   at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.PreviewKey(Key key, Boolean fromCommitChanges)\r\n" +
                "   at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.entityBox_PreviewKeyDown(Object sender, KeyEventArgs e)\r\n" +
                "   at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target)\r\n" +
                "   at System.Windows.RoutedEventHandlerInfo.InvokeHandler(Object target, RoutedEventArgs routedEventArgs)\r\n" +
                "   at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised)\r\n" +
                "   at System.Windows.UIElement.RaiseEventImpl(DependencyObject sender, RoutedEventArgs args)\r\n" +
                "   at System.Windows.UIElement.RaiseTrustedEvent(RoutedEventArgs args)\r\n" +
                "   at System.Windows.Input.InputManager.ProcessStagingArea()\r\n" +
                "   at System.Windows.Input.InputManager.ProcessInput(InputEventArgs input)\r\n" +
                "   at System.Windows.Input.InputProviderSite.ReportInput(InputReport inputReport)\r\n" +
                "   at System.Windows.Interop.HwndKeyboardInputProvider.ReportInput(IntPtr hwnd, InputMode mode, Int32 timestamp, RawKeyboardActions actions, Int32 scanCode, Boolean isExtendedKey, Boolean isSystemKey, Int32 virtualKey)\r\n" +
                "   at System.Windows.Interop.HwndKeyboardInputProvider.ProcessKeyAction(MSG& msg, Boolean& handled)\r\n   at System.Windows.Interop.HwndSource.CriticalTranslateAccelerator(MSG& msg, ModifierKeys modifiers)\r\n" +
                "   at System.Windows.Interop.HwndSource.OnPreprocessMessage(Object param)\r\n   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)\r\n" +
                "   at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)\r\n" +
                "</pre>\r\n\r\n\r\n" +
                "Если после ввода произвольного текста не выделять элемент, введенный в пункте 1, а поставить курсор в конец поля и нажать пробел, то выведется следующее сообщение об ошибке:\r\n\r\n" +
                "<pre>\r\n" +
                "System.ArgumentOutOfRangeException: Specified argument was out of the range of valid values.\r\n" +
                "Parameter name: startIndex\r\n   " +
                "at System.String.Insert(Int32 startIndex, String value)\r\n   " +
                "at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.PreviewKey(Key key, Boolean fromCommitChanges)\r\n   " +
                "at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.entityBox_PreviewKeyDown(Object sender, KeyEventArgs e)\r\n   " +
                "at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target)\r\n   " +
                "at System.Windows.RoutedEventHandlerInfo.InvokeHandler(Object target, RoutedEventArgs routedEventArgs)\r\n   " +
                "at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised)\r\n   " +
                "at System.Windows.UIElement.RaiseEventImpl(DependencyObject sender, RoutedEventArgs args)\r\n   " +
                "at System.Windows.UIElement.RaiseTrustedEvent(RoutedEventArgs args)\r\n   " +
                "at System.Windows.Input.InputManager.ProcessStagingArea()\r\n   " +
                "at System.Windows.Input.InputManager.ProcessInput(InputEventArgs input)\r\n   " +
                "at System.Windows.Input.InputProviderSite.ReportInput(InputReport inputReport)\r\n   " +
                "at System.Windows.Interop.HwndKeyboardInputProvider.ReportInput(IntPtr hwnd, InputMode mode, Int32 timestamp, RawKeyboardActions actions, Int32 scanCode, Boolean isExtendedKey, Boolean isSystemKey, Int32 virtualKey)\r\n   " +
                "at System.Windows.Interop.HwndKeyboardInputProvider.ProcessKeyAction(MSG& msg, Boolean& handled)\r\n   " +
                "at System.Windows.Interop.HwndSource.CriticalTranslateAccelerator(MSG& msg, ModifierKeys modifiers)\r\n   " +
                "at System.Windows.Interop.HwndSource.OnPreprocessMessage(Object param)\r\n   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)\r\n   " +
                "at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)\r\n" +
                "</pre>\r\n\r\n\r\n\r\n" +
                "Если позволите, могу предположить, что что-то не так с функцией <code>Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.InputText()</code>. На сколько я понял, она неверно определяет конечный индекс End <code>InternalAutoCompleteItem internalItem = new InternalAutoCompleteItem();</code>\r\n" +
                "Если свойство End - это индекс окончания введенной строки, а Home - это тачало, то после ввода одного символа в новосозданном internalItem'е End равен Home, который равен нулю. Это немного подозрительно. Возможно из-за этого все остальные функции обсчитываются на этот один символ (отсюда выделение элемента на один короче), так как функции думают, что один элемент короче на 1 символ. \r\n\r\n" +
                "!2.png!\r\n\r\n" +
                "Да пребудет с Вами сила!";


            //string testString = "Ошибка из разряда \"не делайте так\" :)\r\n\r\n" +
            //    "Пусть есть карточка автомобиль (или любая другая с выпадающим списком, или кнопкой троеточия).\r\n\r\n" +
            //    "# Зайдите в поле списка, например, список владельцев и выберите одного владельца.\r\n" +
            //    "# Нажмите стребку влево, чтобы переместить курсов в начало поля. \r\n" +
            //    "# Введите произвольный текст (нажмите цифру 1).\r\n" +
            //    "# Нажмите левой кнопкой мыши на выбранное в пункте 1 справочное значение.\r\n" +
            //    "# Нажмите клавишу delete для удаление элемента списка.\r\n" +
            //    "# Нажмите delete еще раз\r\n\r\n" +

            //    "Ожидаемое поведение:\r\n" +
            //    "- Элемент списка выделяется полностью\r\n" +
            //    "- Элемент удаляется из поля\r\n" +
            //    "- Произвольный текс, введенный в пункте 3 стирается\r\n\r\n" +
            //    "Текущее поведение:\r\n" +
            //    "- Элемент списка выделяется без первой буквы\r\n" +
            //    "- Выводится сообщение об ошибке ArgumentOutOfRangeException \r\n" +
            //    "- После удаления элемента списка и повторном нажатии клавиши delete всегда будет выводиться сообщение об ошибке:\r\n\r\n" +
            //    "<pre>\r\n" +
            //    "System.ArgumentOutOfRangeException: Length cannot be less than zero.\r\n" +
            //    "Parameter name: length\r\n" +
            //    "   at System.String.Substring(Int32 startIndex, Int32 length)\r\n" +
            //    "   at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.FiniteElements(Int32& x)\r\n" +
            //    "   at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.InputText()\r\n" +
            //    "   at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.Erase(Int32 backspace)\r\n" +
            //    "   at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.PreviewKey(Key key, Boolean fromCommitChanges)\r\n" +
            //    "   at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.entityBox_PreviewKeyDown(Object sender, KeyEventArgs e)\r\n" +
            //    "   at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target)\r\n" +
            //    "   at System.Windows.RoutedEventHandlerInfo.InvokeHandler(Object target, RoutedEventArgs routedEventArgs)\r\n" +
            //    "   at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised)\r\n" +
            //    "   at System.Windows.UIElement.RaiseEventImpl(DependencyObject sender, RoutedEventArgs args)\r\n" +
            //    "   at System.Windows.UIElement.RaiseTrustedEvent(RoutedEventArgs args)\r\n" +
            //    "   at System.Windows.Input.InputManager.ProcessStagingArea()\r\n" +
            //    "   at System.Windows.Input.InputManager.ProcessInput(InputEventArgs input)\r\n" +
            //    "   at System.Windows.Input.InputProviderSite.ReportInput(InputReport inputReport)\r\n" +
            //    "   at System.Windows.Interop.HwndKeyboardInputProvider.ReportInput(IntPtr hwnd, InputMode mode, Int32 timestamp, RawKeyboardActions actions, Int32 scanCode, Boolean isExtendedKey, Boolean isSystemKey, Int32 virtualKey)\r\n" +
            //    "   at System.Windows.Interop.HwndKeyboardInputProvider.ProcessKeyAction(MSG& msg, Boolean& handled)\r\n   at System.Windows.Interop.HwndSource.CriticalTranslateAccelerator(MSG& msg, ModifierKeys modifiers)\r\n" +
            //    "   at System.Windows.Interop.HwndSource.OnPreprocessMessage(Object param)\r\n   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)\r\n" +
            //    "   at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)\r\n" +
            //    "</pre>\r\n\r\n\r\n" +
            //    "Если после ввода произвольного текста не выделять элемент, введенный в пункте 1, а поставить курсор в конец поля и нажать пробел, то выведется следующее сообщение об ошибке:\r\n\r\n" +
            //    "<pre>\r\n" +
            //    "System.ArgumentOutOfRangeException: Specified argument was out of the range of valid values.\r\n" +
            //    "Parameter name: startIndex\r\n   " +
            //    "at System.String.Insert(Int32 startIndex, String value)\r\n   " +
            //    "at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.PreviewKey(Key key, Boolean fromCommitChanges)\r\n   " +
            //    "at Tessa.UI.Controls.AutoCompleteCtrl.AutoComplete.entityBox_PreviewKeyDown(Object sender, KeyEventArgs e)\r\n   " +
            //    "at System.Windows.RoutedEventArgs.InvokeHandler(Delegate handler, Object target)\r\n   " +
            //    "at System.Windows.RoutedEventHandlerInfo.InvokeHandler(Object target, RoutedEventArgs routedEventArgs)\r\n   " +
            //    "at System.Windows.EventRoute.InvokeHandlersImpl(Object source, RoutedEventArgs args, Boolean reRaised)\r\n   " +
            //    "at System.Windows.UIElement.RaiseEventImpl(DependencyObject sender, RoutedEventArgs args)\r\n   " +
            //    "at System.Windows.UIElement.RaiseTrustedEvent(RoutedEventArgs args)\r\n   " +
            //    "at System.Windows.Input.InputManager.ProcessStagingArea()\r\n   " +
            //    "at System.Windows.Input.InputManager.ProcessInput(InputEventArgs input)\r\n   " +
            //    "at System.Windows.Input.InputProviderSite.ReportInput(InputReport inputReport)\r\n   " +
            //    "at System.Windows.Interop.HwndKeyboardInputProvider.ReportInput(IntPtr hwnd, InputMode mode, Int32 timestamp, RawKeyboardActions actions, Int32 scanCode, Boolean isExtendedKey, Boolean isSystemKey, Int32 virtualKey)\r\n   " +
            //    "at System.Windows.Interop.HwndKeyboardInputProvider.ProcessKeyAction(MSG& msg, Boolean& handled)\r\n   " +
            //    "at System.Windows.Interop.HwndSource.CriticalTranslateAccelerator(MSG& msg, ModifierKeys modifiers)\r\n   " +
            //    "at System.Windows.Interop.HwndSource.OnPreprocessMessage(Object param)\r\n   at System.Windows.Threading.ExceptionWrapper.InternalRealCall(Delegate callback, Object args, Int32 numArgs)\r\n   " +
            //    "at System.Windows.Threading.ExceptionWrapper.TryCatchWhen(Object source, Delegate callback, Object args, Int32 numArgs, Delegate catchHandler)\r\n" +
            //    "</pre>\r\n\r\n\r\n\r\n" +
            //    "Если позволите, могу предположить, что что-то не так с функцией. На сколько я понял, она неверно определяет конечный индекс End \r\n" +
            //    "Если свойство End - это индекс окончания введенной строки, а Home - это тачало, то после ввода одного символа в новосозданном internalItem'е End равен Home, который равен нулю. Это немного подозрительно. Возможно из-за этого все остальные функции обсчитываются на этот один символ (отсюда выделение элемента на один короче), так как функции думают, что один элемент короче на 1 символ. \r\n\r\n" +

            //    "Да пребудет с Вами сила!";

            Parser parser = new Parser(testString);
            var result = parser.GetParsedString();
        }
    }
}
