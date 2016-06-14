using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace ReelsHelper
{
    public class JSonParser
    {
        //"{ 'Name': 'Jon Smith', 'Address': { 'City': 'New York', 'State': 'NY' }, 'Age': 42 }"
        public static List<Reel> ParseReels(string text)
        {
            List<Reel> reels = new List<Reel>();
            try
            {
                string json = text.Replace("\"", "\'");
                if (!json.StartsWith("{"))
                {
                    json = "{" + json + "}";
                }
                JObject stuff = JObject.Parse(json);
                JToken token = stuff.FindTokens("reels").FirstOrDefault();
                if(token != null)
                {
                    List<List<int>> list = token.ToObject<List<List<int>>>();
                    foreach (List<int> indexes in list)
                    {
                        reels.Add(new Reel(indexes));
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return reels;
        }
    }

    public static class JsonExtensions
    {
        public static List<JToken> FindTokens(this JToken containerToken, string name)
        {
            List<JToken> matches = new List<JToken>();
            FindTokens(containerToken, name, matches);
            return matches;
        }

        private static void FindTokens(JToken containerToken, string name, List<JToken> matches)
        {
            if (containerToken.Type == JTokenType.Object)
            {
                foreach (JProperty child in containerToken.Children<JProperty>())
                {
                    if (child.Name == name)
                    {
                        matches.Add(child.Value);
                    }
                    FindTokens(child.Value, name, matches);
                }
            }
            else if (containerToken.Type == JTokenType.Array)
            {
                foreach (JToken child in containerToken.Children())
                {
                    FindTokens(child, name, matches);
                }
            }
        }
    }
}
