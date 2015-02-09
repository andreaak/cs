using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using Patterns.Behavioral.Observer.Example1.News;
using Patterns.Behavioral.Observer.Example1.Widgets;

namespace Patterns.Behavioral.Observer.Example1
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void Test1()
        {
            var newsAggregator = new NewsAggregator();
            var twitterWidget = new TwitterWidget();
            var lentaWidget = new LentaWidget();
            var tvWidget = new TvWidget();

            newsAggregator.NewsChanged += twitterWidget.Update;
            newsAggregator.NewsChanged += lentaWidget.Update;
            newsAggregator.NewsChanged += tvWidget.Update;

            newsAggregator.NewNewsAvailable();
            Debug.WriteLine("");

            newsAggregator.NewsChanged -= twitterWidget.Update;
            newsAggregator.NewNewsAvailable();
        }
    }
}
