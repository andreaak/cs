using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CSTest._02_Object._01_Lazy;
using Microsoft.VisualStudio.TestTools.UnitTesting;




namespace CSTest._02_Object._01_Lazy
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestLazy1()
        {
            Debug.WriteLine("Before init");
            LazyNET<TestForLazy> lazy = new LazyNET<TestForLazy>(() =>
            {
                Debug.WriteLine("Factory Method");

                TestForLazy res = new TestForLazy();
                res.Name = "Test";
                return res;
            }, false);
            Debug.WriteLine("Before using");
            TestForLazy test = lazy.Value;
            Debug.WriteLine(test.Name);
            /*
            Before init
            Before using
            Create value
            Factory Method
            Test
            */
        }
    }

    class TestForLazy
    {
        public string Name { get; set; }
    }

    class HttpContentManager
    {
        public string Name { get; set; }
        public Action<string> ResourceLoadFailed { get; internal set; }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    class Game
    {
        public string Name { get; set; }
    }


    class SpriteHttpLoader
    {
        private readonly Game _game;
        private Lazy<HttpContentManager> _contentManager;

        private string _spriteLoadFaildName;

        private void ContentManagerOnResourceLoadFailed(string name)
        {
            _spriteLoadFaildName = name;
        }

        public SpriteHttpLoader(Game game, Func<HttpContentManager> contentManagerFunc)
        {
            _game = game;
            _contentManager = new Lazy<HttpContentManager>(() => GetInstance(contentManagerFunc), false);
        }


        private HttpContentManager GetInstance(Func<HttpContentManager> contentManagerFunc)
        {
            HttpContentManager contentManager = contentManagerFunc();
            contentManager.ResourceLoadFailed += ContentManagerOnResourceLoadFailed;
            return contentManager;
        }
        /*
        Some Code 
        */

        public void Dispose()
        {
            if (!_contentManager.IsValueCreated)
                return;

            HttpContentManager contentManager = _contentManager.Value;
            contentManager.ResourceLoadFailed -= ContentManagerOnResourceLoadFailed;
            contentManager.Dispose();
            _contentManager = null;
        }
    }
}
