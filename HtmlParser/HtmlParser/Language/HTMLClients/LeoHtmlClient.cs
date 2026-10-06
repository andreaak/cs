using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace HtmlParser.Language.HTMLClients
{
    public class LeoHtmlClient : IHtmlClient
    {
        public string GetUrl(string word)
        {
            string encodedWord = Uri.EscapeDataString(word);
            return "https://dict.leo.org/russisch-deutsch/" + encodedWord;
        }

        public string ReadHtml(string url, string cookie)
        {
            try
            {
                using (var leo = new LeoClient(false))
                {
                    LeoResult result = leo.GetWord(url); ;
                    return leo.GetHtml(url);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.ToString());
            }

            return "";
        }
    }

    public class LeoClient : IDisposable
    {
        private readonly LeoBrowser _browser;
        private readonly LeoParser _parser;

        public LeoClient(bool headless = false)
        {
            _browser = new LeoBrowser(headless);
            _parser = new LeoParser();
        }

        public LeoResult GetWord(string word)
        {
            string html = _browser.GetPage(word);
            LeoResult result =
                _parser.Parse(
                    html,
                    word,
                    _browser.CurrentUrl);

            return result;
        }

        public string GetHtml(string word)
        {
            return _browser.GetPage(word);
        }

        public void SaveScreenshot(string fileName)
        {
            _browser.SaveScreenshot(fileName);
        }

        public void Dispose()
        {
            if (_browser != null)
                _browser.Dispose();
        }
    }

    public class LeoBrowser : IDisposable
    {
        private readonly IWebDriver _driver;

        private readonly WebDriverWait _wait;

        public LeoBrowser(bool headless = false)
        {
            var options = new ChromeOptions();

            if (headless)
            {
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920,1080");
            }

            options.AddArgument("--start-maximized");

            options.AddArgument("--lang=en-US");

            // Не отключаем безопасность Chrome.
            // Chrome должен работать как обычный браузер.

            _driver = new ChromeDriver(options);

            _driver.Manage().Timeouts().PageLoad =
                TimeSpan.FromSeconds(30);

            _driver.Manage().Timeouts().ImplicitWait =
                TimeSpan.FromSeconds(2);

            _wait = new WebDriverWait(
                _driver,
                TimeSpan.FromSeconds(20));
        }

        public string GetPage(string word)
        {
            //if (string.IsNullOrWhiteSpace(word))
            //    throw new ArgumentException(
            //        "Word cannot be empty.",
            //        nameof(word));

            //string encodedWord =
            //    Uri.EscapeDataString(word);

            //string url =
            //    "https://dict.leo.org/russisch-deutsch/" +
            //    encodedWord;

            string url = word;

            _driver.Navigate().GoToUrl(url);

            WaitForPage();

            return _driver.PageSource;
        }

        private void WaitForPage()
        {
            _wait.Until(driver =>
            {
                try
                {
                    return driver.FindElements(
                        By.TagName("body")).Count > 0;
                }
                catch
                {
                    return false;
                }
            });
        }

        public string CurrentUrl
        {
            get
            {
                return _driver.Url;
            }
        }

        public string CurrentTitle
        {
            get
            {
                return _driver.Title;
            }
        }

        public void SaveScreenshot(string fileName)
        {
            var screenshot =
                ((ITakesScreenshot)_driver)
                .GetScreenshot();

            //screenshot.SaveAsFile(
            //    fileName,
            //    ScreenshotImageFormat.Png);
        }

        public void Dispose()
        {
            if (_driver != null)
            {
                try
                {
                    _driver.Quit();
                }
                catch
                {
                    // Игнорируем ошибку при закрытии Chrome.
                }

                try
                {
                    _driver.Dispose();
                }
                catch
                {
                }
            }
        }
    }

    public class LeoParser
    {
        public LeoResult Parse(
            string html,
            string word,
            string url)
        {
            if (html == null)
                throw new ArgumentNullException(nameof(html));

            var result = new LeoResult();

            result.Word = word;
            result.Url = url;

            // Пока сохраняем базовую информацию.
            //
            // Здесь позже можно добавить разбор:
            //
            // Deutsch
            // Russian
            // Wortart
            // Beispiele
            // Bedeutungen
            //
            // После получения реального HTML LEO
            // сюда добавляются соответствующие селекторы.

            return result;
        }

        public static string RemoveHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return string.Empty;

            string text =
                Regex.Replace(
                    html,
                    "<[^>]+>",
                    " ");

            text =
                WebUtility.HtmlDecode(text);

            text =
                Regex.Replace(
                    text,
                    @"\s+",
                    " ");

            return text.Trim();
        }
    }




    public class LeoResult
    {
        public string Word { get; set; }

        public string Url { get; set; }

        public List<LeoMeaning> Meanings { get; set; }

        public LeoResult()
        {
            Meanings = new List<LeoMeaning>();
        }
    }

    public class LeoMeaning
    {
        public string German { get; set; }

        public string Russian { get; set; }

        public string Grammar { get; set; }

        public List<LeoExample> Examples { get; set; }

        public LeoMeaning()
        {
            Examples = new List<LeoExample>();
        }

        public override string ToString()
        {
            return German + " -> " + Russian;
        }
    }

    public class LeoExample
    {
        public string German { get; set; }

        public string Russian { get; set; }

        public override string ToString()
        {
            return German + " -> " + Russian;
        }
    }
}