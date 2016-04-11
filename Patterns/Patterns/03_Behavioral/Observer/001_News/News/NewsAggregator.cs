using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Patterns.Behavioral.Observer._001_News.Widgets;

namespace Patterns.Behavioral.Observer._001_News.News
{
    public class NewsEventArgs
    {
        public NewsEventArgs(string twitter, string lenta, string tv)
        {
            Twitter = twitter;
            Lenta = lenta;
            Tv = tv;
        }

        public string Twitter { get; private set; }
        public string Lenta { get; private set; }
        public string Tv { get; private set; }
    }

    public delegate void NewsChangedEventHandler(object sender, NewsEventArgs e);

    public class NewsAggregator
    {
        private static Random _random;

        public NewsAggregator()
        {
            _random = new Random();
        }

        public event NewsChangedEventHandler NewsChanged;

        public string GetTwitterNews()
        {
            var news = new List<string>
                           {
                               "Новость из Twitter номер 1",
                               "Новость из Twitter номер 2",
                               "Новость из Twitter номер 3"
                           };

            return news[_random.Next(3)];
        }

        public string GetLentaNews()
        {
            var news = new List<string>
                           {
                               "Новость из Lenta номер 1",
                               "Новость из Lenta номер 2",
                               "Новость из Lenta номер 3"
                           };

            return news[_random.Next(3)];
        }

        public string GetTvNews()
        {
            var news = new List<string>
                           {
                               "Новость из TV номер 1",
                               "Новость из TV номер 2",
                               "Новость из TV номер 3"
                           };

            return news[_random.Next(3)];
        }

        public void NewNewsAvailable()
        {
            string twitter = GetTwitterNews();
            string lenta = GetLentaNews();
            string tv = GetTvNews();

            if (NewsChanged != null)
                NewsChanged(this, new NewsEventArgs(twitter, lenta, tv));
        }
    }
}
