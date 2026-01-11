using System;
using HtmlParser.Language;

namespace HtmlParser
{
    public class ParametersProvider
    {
        public static Parameters Parse(string line)
        {
            var parameters = new Parameters();

            var prms = line.Trim().ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            parameters.Action = prms[0];

            for (int i = 1; i < prms.Length; i++)
            {
                switch (prms[i])
                {
                    case "noord":
                        parameters.Order = false;
                        break;
                    case "ord":
                        parameters.Order = true;
                        break;
                    case "verb":
                        parameters.WordType = WordType.Verb;
                        break;
                    case "subst":
                        parameters.WordType = WordType.Subst;
                        break;
                    case "adv":
                        parameters.WordType = WordType.Adv;
                        break;
                    case "adj":
                        parameters.WordType = WordType.Adj;
                        break;
                    case "all":
                        parameters.WordType = WordType.All;
                        break;
                    case "pron":
                        parameters.WordType = WordType.Pron;
                        break;
                    case "präp":
                        parameters.WordType = WordType.Prep;
                        break;
                    case "konj":
                        parameters.WordType = WordType.Konj;
                        break;
                    case "de":
                        parameters.Lang = "de";
                        break;
                    case "en":
                        parameters.Lang = "en";
                        break;
                    case "example":
                        parameters.GetExample = true;
                        break;
                    case "level":
                        parameters.SetLevel = true;
                        break;
                    case "remdupl":
                        parameters.RemoveDuplicates = true;
                        break;
                }
            }
            
            return parameters;
        }
    }
}