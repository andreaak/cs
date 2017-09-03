using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using _04_SlowService.Model;

namespace _04_SlowService
{
    public class NewsController : ApiController
    {
        public NewsModel GetHeadline()
        {
            Thread.Sleep(2000);
            return new NewsModel()
            {
                Headline = "Microsoft Office: Exploring the New JavaScript API for Office",
                Time = DateTime.Now.ToLongTimeString()
            };
        }
    }
}
