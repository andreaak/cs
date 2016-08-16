using System.Diagnostics;
using NUnit.Framework;
using Patterns._03_Behavioral.Observer._001_News.News;
using Patterns._03_Behavioral.Observer._001_News.Widgets;

namespace Patterns._03_Behavioral.Observer._001_News
{
    [TestFixture]
    public class Test
    {
        [Test]
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
