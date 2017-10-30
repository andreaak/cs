using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace CSTest._31_Json
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void TestJson1Read()
        {
            JObject document = Read();
            string documentString = ReadString();
            var obj = documentString.CreateObject();
        }

        [Test]
        public void TestJson2Parse()
        {
            JObject document = Read();
            var token = document.FindDescendantElement<JToken>("Round");
            var scene = token.Children().First().FindElement<string>("scene");
            var test = token.Children().First().FindElement<int>("test");

        }

        private JObject Read()
        {
            using (var stream = new FileStream("31_Json/config.json", FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var jr = new JsonTextReader(reader))
                    {
                        return JObject.Load(jr);
                    }
                }
            }
        }

        private string ReadString()
        {
            using (var stream = new FileStream("31_Json/config.json", FileMode.Open))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
