using Microsoft.VisualStudio.TestTools.UnitTesting;
using Patterns.Behavioral.Observer._001_News.News;
using Patterns.Behavioral.Observer._001_News.Widgets;
using System.Diagnostics;

namespace Patterns.Behavioral.Observer._001_News
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
