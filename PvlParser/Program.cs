using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PvlParser
{
    class Program
    {
        static void Main(string[] args)
        {
            //string file = "D:\\pvl.csv";
            //var lines = File.ReadLines(file);
            //var parser = new PvlParser();
            //parser.Parse2(lines.Skip(1).ToArray());
            string file = "D:\\2023 data.csv";
            var parser = new PvlAuditParser();
            parser.Parse(file);
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }

    public class PvlAuditParser
    {
        public void Parse(string fileName)
        {
            var delimiterRegExp = DelimiterRegExp();
            var entities = new HashSet<string>();

            const Int32 BufferSize = 1024;
            using (var fileStream = File.OpenRead(fileName))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                int j = 0;
                String line;

                int count = 300000;
                var lines = new List<string>();

                while ((line = streamReader.ReadLine()) != null)
                {
                    lines.Add(line);

                    if (lines.Count >= count)
                    {
                        var partitions = Partitioner.Create(1, lines.Count, 50000);

                        Parallel.ForEach(partitions, new ParallelOptions { MaxDegreeOfParallelism = 5 },
                            () => new HashSet<string>(), // local list to add entities
                            (range, s, local) =>
                            {
                                for (int i = range.Item1; i < range.Item2; i++)
                                {
                                    var elements = delimiterRegExp.Split(lines[i]);
                                    var strategy = elements[3].ToLower();

                                    if (strategy == "updatelisted")
                                    {
                                        if (!string.IsNullOrEmpty(elements[5]))
                                        {
                                            local.UnionWith(elements[5].Split(";"));
                                        }
                                    }
                                }

                                return local;
                            },
                            local => //Aggregating thread local entities to global list
                            {
                                lock (entities)
                                {
                                    entities.UnionWith(local);
                                }
                            });

                        j++;
                        Console.WriteLine(j);

                        lines = new List<string>();
                    }
                }
            }


            var str = string.Join("\r\n", entities.OrderBy(s => s));
            File.WriteAllText("groups.txt", str);
        }

        private Regex DelimiterRegExp()
        {
            return DelimiterRegExp(",");
        }

        public static Regex DelimiterRegExp(string csvColumnSeparator)
        {
            var defaultDelimiterRegExpPattern = $@"{csvColumnSeparator}\s*(?=(?:[^""]*""[^""]*"")*[^""]*$)";
            return new Regex(defaultDelimiterRegExpPattern);
        }
    }

    public class PvlParser
    {
        private static JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
        };

        public void Parse(IList<string> lines)
        {
            var list = new List<PvlItem>();
            var delimiterRegExp = DelimiterRegExp();

            int i = 0;

            foreach (var line in lines)
            {
                var elements = delimiterRegExp.Split(line);
                var id = elements[0];
                var groupsString = elements[3];

                i++;

                if (string.IsNullOrEmpty(groupsString))
                {
                    continue;
                }

                IList<string> groups;
                if (groupsString.Contains("groupItems"))
                {
                    var grp = groupsString.TrimStart('\"').TrimEnd('\"');
                    var groupItems = JsonConvert.DeserializeObject<PvlGroupsItem>(grp, _settings);
                    groups = groupItems.groupItems.Select(x => x.groupKey).ToArray();
                }
                else
                {
                    groups = groupsString.Split(';').ToArray();
                }

                if (groups.Count >= 200)
                {
                    list.Add(new PvlItem
                    {
                        Id = id,
                        GroupsCount = groups.Count
                    });
                }
                
            }

            var str = JsonConvert.SerializeObject(list);
        }

        public void Parse2(IList<string> lines)
        {
            var delimiterRegExp = DelimiterRegExp();


            var partitions = Partitioner.Create(1, lines.Count, 1000);
            var entities = new List<PvlItem>();

            Parallel.ForEach(partitions, new ParallelOptions { MaxDegreeOfParallelism = 5 },
                () => new List<PvlItem>(), // local list to add entities
                (range, s, local) =>
                {
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        var line = lines[i];

                        var item = ParseLine(delimiterRegExp, line);


                        if (item.GroupsCount >= 200)
                        {
                            local.Add(new PvlItem
                            {
                                Id = item.Id,
                                GroupsCount = item.GroupsCount
                            });
                        }
                    }

                    return local;
                },
                local => //Aggregating thread local entities to global list
                {
                    lock (entities)
                    {
                        entities.AddRange(local);
                    }
                });


            //foreach (var line in lines)
            //{
            //    var item = ParseLine(delimiterRegExp, line);


            //    if (item.GroupsCount >= 200)
            //    {
            //        list.Add(new PvlItem
            //        {
            //            Id = item.Id,
            //            GroupsCount = item.GroupsCount
            //        });
            //    }
            //}

            var str = JsonConvert.SerializeObject(entities);
        }

        private static PvlItem ParseLine(Regex delimiterRegExp, string line)
        {
            var elements = delimiterRegExp.Split(line);
            string id = elements[0];
            var groupsString = elements[3];

            if (string.IsNullOrEmpty(groupsString))
            {
                return new PvlItem();
            }

            IList<string> groups;
            if (groupsString.Contains("groupItems"))
            {
                var grp = groupsString.TrimStart('\"').TrimEnd('\"');
                var groupItems = JsonConvert.DeserializeObject<PvlGroupsItem>(grp, _settings);
                groups = groupItems.groupItems.Select(x => x.groupKey).ToArray();
            }
            else
            {
                groups = groupsString.Split(';').ToArray();
            }

            return new PvlItem
            {
                Id = id,
                GroupsCount = groups.Count
            };
        }

        private Regex DelimiterRegExp()
        {
            return DelimiterRegExp(",");
        }

        public static Regex DelimiterRegExp(string csvColumnSeparator)
        {
            var defaultDelimiterRegExpPattern = $@"{csvColumnSeparator}\s*(?=(?:[^""]*""[^""]*"")*[^""]*$)";
            return new Regex(defaultDelimiterRegExpPattern);
        }

    }

    public class PvlItem
    {
        public string Id { get; set; }
        public int GroupsCount { get; set; }
    }

    public class PvlGroupsItem
    {
        public IList<GroupItem> groupItems { get; set; }
    }
    
    public class GroupItem
{
        public string groupKey { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public bool isQualified { get; set; }
    }
}
