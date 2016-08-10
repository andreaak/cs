using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace CSTest._08_String._01_RegularExpression
{
    [TestFixture]
    public class TestCookBook
    {
        /*
        Номер социальной страховки
        Проверка корректности этого номера не составляет труда. Номер состоит из де вяти цифр, возможно, разделенных дефисами
        */
        [Test]
        public void TestRegExpSSN()
        {
            Regex regex = new Regex(@"^\d{3}\-?\d{2}\-?\d{4}$");

            string userString = "123456789";
            PrintResult(regex, userString);

            userString = "123-45-6789";
            PrintResult(regex, userString);

            userString = "111-11-1111";
            PrintResult(regex, userString);

            userString = "123-45.678";
            PrintResult(regex, userString);

            userString = "123.45.6789";
            PrintResult(regex, userString);

            userString = "12.123.4444";
            PrintResult(regex, userString);

            userString = "123.45.67890";
            PrintResult(regex, userString);

            userString = "123.a5.6789";
            PrintResult(regex, userString);

            userString = "just random text";
            PrintResult(regex, userString);

            /*
            123456789 ? ok
            123-45-6789 ? ok
            111-11-1111 ? ok
            123-45.678 ? bad
            123.45.6789 ? bad
            12.123.4444 ? bad
            123.45.67890 ? bad
            123.a5.6789 ? bad
            just random text ? bad
            */
        }

        /*
        Номер телефона
        Телефонные номера тоже часто подвергаются проверке на корректность. 
        В следующем примере проверяется стандартный 10-значный телефонный номер в США
        */
        [Test]
        public void TestRegExpPhone()
        {
            Regex regex = new Regex(
                //xxx.xxx.xxxx и xxx-xxx-xxxx
            @"^((\d{3}[\-\.]?\d{3}[\-\.]?\d{4})|" +
                //XX.XX.XXX.XXX И xx-xx-xxx-xxx
            @"(\d{2}[\-\.]?\d{2}[\-\.]?\d{3}[\-\.]?\d{3}))" +
            "$");

            string userString = "123.456.7890";
            PrintResult(regex, userString);

            userString = "123-456-7890";
            PrintResult(regex, userString);

            userString = "1234567890";
            PrintResult(regex, userString);

            userString = "123.4567890";
            PrintResult(regex, userString);


            userString = "12.34.567.890";
            PrintResult(regex, userString);

            userString = "123.456.78900";
            PrintResult(regex, userString);

            userString = "123-456";
            PrintResult(regex, userString);

            userString = "123-abc-7890";
            PrintResult(regex, userString);
            /*
            123.456.7890 ? ok
            123-456-7890 ? ok
            1234567890 ? ok
            123.4567890 ? ok
            12.34.567.890 ? ok
            123.456.78900 ? bad
            123-456 ? bad
            123-abc-7890 ? bad
            */
        }

        /*
        Почтовый индекс
        Почтовый индекс содержит 5 или 9 цифр, возможно, разделенных дефисами   
        */
        [Test]
        public void TestRegExpPostIndex()
        {
            Regex regex = new Regex(@"^\d{5}(-?\d{4})?$");

            string userString = "12345";
            PrintResult(regex, userString);

            userString = "12345-6789";
            PrintResult(regex, userString);

            userString = "123456789";
            PrintResult(regex, userString);

            userString = "12345-";
            PrintResult(regex, userString);

            userString = "1234";
            PrintResult(regex, userString);

            userString = "1234-6789";
            PrintResult(regex, userString);

            userString = "a234";
            PrintResult(regex, userString);

            userString = "123456";
            PrintResult(regex, userString);

            userString = "1234567890";
            PrintResult(regex, userString);

            /*
            12345 ? ok
            12345-6789 ? ok
            123456789 ? ok
            12345- ? bad
            1234 ? bad
            1234-6789 ? bad
            a234 ? bad
            123456 ? bad
            1234567890 ? bad
            */
        }

        /*
        Извлечение пар "имя = значение" (по одной в строке)
        Обратите внимание на применение в начале директивы (?m);
        */
        [Test]
        public void TestRegExpParseKeyValue()
        {
            string r = @"(?m)^\s*(?'name'\w+)\s*=\s*(?'value'.*)\s*(?=\r?$)";
            string text =
            @"id = 3 secure = true timeout = 30";
            foreach (Match m in Regex.Matches(text, r))
            {
                Debug.WriteLine(m.Groups["name"] + " is " + m.Groups["value"]);
            }
            /*
            id is 3 secure = true timeout = 30
            */
        }

        /*
        Проверка сильных паролей
        Следующий код проверяет, что пароль состоит, по меньшей мере, 
        из шести символов и включает цифру, символ или знак пунктуации
        */
        [Test]
        public void TestRegExpPassword()
        {
            string r = @"(?x)^(?=.*(\d|\p{P}|\p{S})).{6,}";
            Debug.WriteLine(Regex.IsMatch("abc12", r)); // False
            Debug.WriteLine(Regex.IsMatch("abcdef", r)); // False
            Debug.WriteLine(Regex.IsMatch("ab88yz", r)); // True
            /*
            */
        }

        //Строки, содержащие, по крайней мере, 80 символов
        [Test]
        public void TestRegExpSinbolsMore()
        {
            string r = @"(?m)^.{80,}(?=\r?$)";
            string fifty = new string('x', 50);
            string eighty = new string('x', 80);
            string text = eighty + "\r\n" + fifty + "\r\n" + eighty;
            Debug.WriteLine(Regex.Matches(text, r).Count); // 2
            /*

            */
        }

        /*
        Дата
        Проведенное здесь регулярное выражение служит для проверки корректности даты, 
        записанной в формате ММ/ДД/ГГГГ, который принят в США
        */
        [Test]
        public void TestRegExpDate()
        {
            Regex regex = new Regex(@"(0[1—9]|1[012])/([1-9]|0[1-9]|[12][0-9]|3[01])/\d{4}");

            string userString = "12/25/2009";
            PrintResult(regex, userString);

            userString = "01/25/2009";
            PrintResult(regex, userString);

            userString = "1/2/2009";
            PrintResult(regex, userString);

            userString = "25/12/2009";
            PrintResult(regex, userString);

            userString = "2009/12/25";
            PrintResult(regex, userString);

            userString = "13/25/2009";
            PrintResult(regex, userString);

            userString = "12/25/09";
            PrintResult(regex, userString);

            /*
            12/25/2009 ? ok
            01/25/2009 ? ok
            1/2/2009 ? bad
            25/12/2009 ? bad
            2009/12/25 ? bad
            13/25/2009 ? bad
            12/25/09 ? bad
            */
        }

        /*
        Разбор даты /времени
        Это выражение поддерживает разнообразные числовые форматы даты и работает независимо от того, указан год в начале или конце. 
        Директива (?х) улучшает читабельность, разрешая применение пробельных символов; 
        директива (?i) отключает чувствительность к регистру символов (для необязательного указателя АМ/РМ). 
        Затем к компонентам совпадения можно обращаться через коллекцию Groups
        */
        [Test]
        public void TestRegExpDateTime()
        {
            string r = @"(?x)(?i)
                    (\d{1,4}) [./-]
                    (\d{1,2}) [./-]
                    (\d{1,4}) [\sT]
                    (\d+):(\d+):(\d+) \s? (A\.?M\.?|P\.?M\.?)?";
            string text = "01/02/2008 5:20:50 PM";
            foreach (Group g in Regex.Match(text, r).Groups)
            {
                Debug.WriteLine(g.Value + " ");
            }
            //Разумеется, это не проверяет правильность даты/времени.
            /*
            01/02/2008 5:20:50 PM 
            01 
            02 
            2008 
            5 
            20 
            50 
            PM 
            */
        }

        //Соответствие  римским  числам
        [Test]
        public void TestRegExpRoman()
        {
            string r = @"(?i)\bm*" +
                        @"(d?c{0,3}|с[dm])" +
                        @"(l?х{0,3}|x[lc])" +
                        @"(v?i{0,3}|i[vx])" +
                        @"\b";
            Debug.WriteLine(Regex.IsMatch("MCMLXXXIV", r));  //True

            /*
            */
        }

        /*
        Удаление  повторяющихся  слов.
        Здесь мы захватываем именованную группу dupe:
        */
        [Test]
        public void TestRegExpDuplicatedWords()
        {
            string r = @"(?'dupe'\w+)\W\k'dupe'";
            string text = "In the the beginning...";
            Debug.WriteLine(Regex.Replace(text, r, "${dupe}"));
            /*
            In the beginning...
            */
        }

        /*
        Подсчет  слов
        */
        [Test]
        public void TestRegExpWordsCount()
        {
            string r = @"\b(\w|[-'])+\b";
            string text = "It's all mumbo-jumbo to me";
            Debug.WriteLine(Regex.Matches(text, r).Count);  // 5
            /*
            */
        }

        /*
        Соответствие GUID
        */
        [Test]
        public void TestRegExpGuid()
        {
            string r = @"(?i)\b" +
                @"[0-9a-fA-F]{8}\-" +
                @"[0-9a-fA-F]{4}\-" +
                @"[0-9a-fA-F]{4}\-" +
                @"[0-9a-fA-F]{4}\-" +
                @"[0-9a-fA-F]{12}" +
                @"\b";
            string text = "Its key is {3F2504E0-4F89-11D3-9A0C-0305E82C3301}.";
            Debug.WriteLine(Regex.Match(text, r).Index);  // 12
            /*
            */
        }

        /*
        Разбор дескриптора XML/HTML
        Класс Regex удобен при разборе фрагментов HTML-разметки - особенно, 
        когда документ может быть сформирован некорректно:
        */
        [Test]
        public void TestRegExpXML_HTML()
        {
            string r = @"<(?'tag'\w+?).*>" +   // соответствует первому дескриптору; назовем группу 'tag'
                        @"(?'text'.*?)" + // соответствует текстовому содержимому; назовем группу 'text'
                        @"</\k'tag'>";  // соответствует последнему дескриптору,  отмеченному 'tag'
            string text = "<h1>hello</h1>";
            Match m = Regex.Match(text, r);
            Debug.WriteLine(m.Groups["tag"]); // h1
            Debug.WriteLine(m.Groups["text"]); // hello
            /*
            */
        }

        /*
        Разделение  на  слова  в  верблюжьем  стиле
        Это требует положительного просмотра вперед,
        чтобы включить разделители в верхнем регистре
        */
        [Test]
        public void TestRegExpCamelCase()
        {
            string r = @"(?=[A-Z])";
            foreach (string s in Regex.Split("oneTwoThree", r))
            {
                Debug.Write(s + " ");  // one Two Three
            }
            /*
            one Two Three 
            */
        }

        /*
        Получение допустимого имени файла
        */
        [Test]
        public void TestRegExpFileName()
        {
            string input = "My \"good\" <recipes>.txt";
            char[] invalidChars = System.IO.Path.GetInvalidPathChars();
            string invalidstring = Regex.Escape(new string(invalidChars));
            string valid = Regex.Replace(input, "[" + invalidstring + "]", "");
            Debug.WriteLine(valid);
            /*
            My good recipes.txt
            */
        }

        /*
        Преобразование символов в строке запроса HTTP
        */
        [Test]
        public void TestRegExHTTP()
        {
            string sample = "C%23 rocks";
            string result = Regex.Replace(
                sample,
                @"%[0-9a-f][0-9a-f]",
                m => ((char)Convert.ToByte(m.Value.Substring(1), 16)).ToString(),
                RegexOptions.IgnoreCase
            );
            Debug.WriteLine(result); // C# rocks

            /*
            */
        }

        /*
        Разбор поисковых терминов Google из журнала веб-статистики
        Это должно использоваться в сочетании с предыдущим примером преобразования символов в строке запроса
        */
        [Test]
        public void TestRegExpGoogle()
        {
            string sample = "http://google.com/search?hl=en&q=greedy+quantifiers+regex&btnG=Search";
            Match m = Regex.Match(sample, @"(?<=google\..+search\?.*q=).+?(?=(&|$))");
            string[] keywords = m.Value.Split(new[] { '+' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string keyword in keywords)
            {
                Debug.Write(keyword + " ");  // greedy quantifiers regex
            }
            /*
            greedy quantifiers regex
            */
        }

        private static void PrintResult(Regex regex, string userString)
        {
            bool isMatch = regex.IsMatch(userString);
            Debug.WriteLine("{0} ? {1}", userString, isMatch ? "ok" : "bad");
        }
    }
}
