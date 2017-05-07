using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CSTest._31_Json
{
    public static class JsonMiniGameConfigParserExtensions
    {

        public static string CreateJson(this object reels)
        {
            return JsonConvert.SerializeObject(reels);
        }

        public static object CreateObject(this string json)
        {
            return JsonConvert.DeserializeObject(json);
        }

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

        public static T FindDescendantElement<T>(this JToken containerToken, string name)
        {
            //if (containerToken.Type == JTokenType.Object)
            //{
            foreach (JProperty child in containerToken.Children<JProperty>())
            {
                if (child.Name == name)
                {
                    return child.Value.Value<T>();
                }
                T token = FindDescendantElement<T>(child.Value, name);
                if (token != null)
                {
                    return token;
                }
            }
            //}
            //else if (containerToken.Type == JTokenType.Array)
            //{
            //    foreach (JToken child in containerToken.Children())
            //    {
            //        var token = FindAnccestorToken(child, name);
            //        if (token != null)
            //        {
            //            return token;
            //        }
            //    }
            //}
            return default(T);
        }

        public static T FindElement<T>(this JToken containerToken, string name)
        {
            foreach (var child in containerToken.Children<JProperty>())
            {
                if (child.Name == name)
                {
                    return child.Value.Value<T>();
                }
            }
            return default(T);
        }


        public static T GetDescendantItem<T>(this JToken containerToken, string name)
        {
            foreach (JProperty child in containerToken.Children<JProperty>())
            {
                if (child.Name == name)
                {
                    return child.Value.Value<T>();
                }
                T token = GetDescendantItem<T>(child.Value, name);
                if (token != null)
                {
                    return token;
                }
            }
            return default(T);
        }

        public static T GetItem<T>(this JToken containerToken, string name)
        {
            foreach (var child in containerToken.Children<JProperty>())
            {
                if (child.Name == name)
                {
                    return child.Value.Value<T>();
                }
            }
            return default(T);
        }

        public static double GetItemOrDefault(this JToken data, string name, double defaultValue)
        {
            var attribute = data.GetItem<string>(name);
            double result;
            return attribute != null
                && double.TryParse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture, out result)
                ? result
                : defaultValue;
        }

        public static int GetItemOrDefault(this JToken data, string name, int defaultValue)
        {
            var attribute = data.GetItem<string>(name);
            int result;
            return attribute != null
                && int.TryParse(attribute, NumberStyles.Float, CultureInfo.InvariantCulture, out result)
                ? result
                : defaultValue;
        }

        public static bool GetItemOrDefault(this JToken data, string name, bool defaultValue)
        {
            var attribute = data.GetItem<string>(name);
            bool result;
            return attribute != null
                && bool.TryParse(attribute, out result)
                ? result
                : defaultValue;
        }

        public static IList<JToken> GetArrayItem(this JToken token)
        {
            if (token == null)
                return new JToken[0];
            if (token.Type == JTokenType.Object)
                return new[] { token };
            return token.Children().ToArray();
        }

        public static IList<T> GetArrayItem<T>(this JToken token, Func<JToken, T> func)
        {
            if (token == null)
                return new T[0];
            if (token.Type == JTokenType.Object)
                return new[] { func(token) };
            return token.Children().Select(func).ToArray();
        }
    }
}
