using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using HtmlAgilityPack;

namespace HtmlParser
{
    public class HtmlTask
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Dir { get; set; }
    }

    public class PidruchnikiParser : LinkParser
    {
        public void Normalize()
        {
            string dir = @"D:\Projects\My\cs\HtmlParser\HtmlParser\bin\Debug";

            var files = new DirectoryInfo(dir).EnumerateFiles("*.html", SearchOption.AllDirectories).ToArray();

            foreach (var file in files)
            {
                var htmlText = File.ReadAllText(file.FullName, Encoding.GetEncoding(1251));
                var doc = GetHtmlFromText(htmlText);
                var grouped = doc.DocumentNode.SelectNodes(".//div[@class='innercontent']/a[@href]")
                    .GroupBy(link => link.Attributes["href"].Value).ToArray();

                var nodes = grouped
                    .Where(gr => gr.Count() > 1).ToArray();
                var indexes = grouped.Select(group => group.Key).Distinct().ToList();
                if (nodes.Length == 0)
                {
                    continue;
                }

                foreach (var group in nodes)
                {
                    var id = group.Key.Trim().TrimStart('#');

                    var headers = doc.DocumentNode.SelectNodes($".//*[@id={id}]")
                        .Where(n => n.Name.StartsWith("h", StringComparison.OrdinalIgnoreCase)).ToArray();
                    if (headers.Length != group.Count())
                    {

                    }

                    for (int i = 0; i < group.Count(); i++)
                    {
                        var link = group.ElementAt(i);
                        var linkText = link.InnerText.Trim();
                        foreach (var header in headers)
                        {
                            var headerText = header.InnerText.Trim();
                            if (linkText.Trim() == headerText.Trim())
                            {
                                int numericId = GetId(indexes);

                                link.Attributes["href"].Value = $"#{numericId}";
                                header.Attributes["id"].Value = $"{numericId}";
                                break;
                            }
                        }


                        //var header = headers[i];
                        //var link = group.ElementAt(i);
                        //var text = link.InnerText.Trim();
                        //var text2 = header.InnerText.Trim();
                        //if (text != text2)
                        //{

                        //}

                        //int numericId = GetId(indexes);

                        //link.Attributes["href"].Value = $"#{numericId}";
                        //header.Attributes["id"].Value = $"{numericId}";
                    }
                }

                doc.Save(file.DirectoryName + Path.DirectorySeparatorChar
                                            + file.Name.Substring(0, file.Name.IndexOf(file.Extension)) + "_converted"
                                            + file.Extension);
            }
        }

        public void Normalize2()
        {
            string dir = @"D:\Projects\My\cs\HtmlParser\HtmlParser\bin\Debug";

            var files = new DirectoryInfo(dir).EnumerateFiles("*.html", SearchOption.AllDirectories).ToArray();

            foreach (var file in files)
            {
                var htmlText = File.ReadAllText(file.FullName, Encoding.GetEncoding(1251));
                var doc = GetHtmlFromText(htmlText);
                var links = doc.DocumentNode.SelectNodes(".//div[@class='innercontent']/a[@href]").ToArray();

                var indexes = new List<string>();

                foreach (var link in links)
                {
                    var linkText = link.InnerText.Trim();
                    if ("��������� ��� ������������" == linkText)
                    {
                        continue;
                    }
                    //var header = doc.DocumentNode.SelectNodes($".//*[contains(text(),'{linkText}')]");
                    var header = doc.DocumentNode.SelectNodes($".//*[normalize-space(text()) = '{linkText}']");
                    if (header == null || header.Count == 1)
                    {
                        continue;
                    }
                    int numericId = GetId(indexes);

                    link.Attributes["href"].Value = $"#{numericId}";
                    var h = header[1];
                    while (!h.Name.StartsWith("h"))
                    {
                        h = h.ParentNode;
                    }
                    h.Attributes["id"].Value = $"{numericId}";
                }
                doc.Save(file.DirectoryName + Path.DirectorySeparatorChar
                                            + file.Name.Substring(0, file.Name.IndexOf(file.Extension)) + "_converted"
                                            + file.Extension);
            }
        }


        private int GetId(IList<string> indexes)
        {
            int i = 1;
            while (true)
            {
                if (!indexes.Contains($"#{i}"))
                {
                    indexes.Add($"#{i}");
                    return i;
                }

                i++;
            }
        }

        public void Parse()
        {
            var list = new List<HtmlTask>
            {
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1089030455826/pravo/administrativne_pravo_ukrayini",
                //    Name = "������������� ����� ������",
                //    Dir = "������������� ����� ������ 1089030455826"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072058730/pravo/administrativne_pravo_zarubizhnih_krayin",
                //    Name = "������������� ����� ��������� ����",
                //    Dir = "������������� ����� ��������� ���� 1584072058730"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/2015082666422/pravo/advokatura_ukrayini",
                //    Name = "���������� ������",
                //    Dir = "���������� ������ 2015082666422"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1770071255523/pravo/advokatura",
                //    Name = "������� - ������ ������",
                //    Dir = "������� - ������ ������ 2015082666464"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/84659/pravo/derzhavne_upravlinnya_ta_vikonavcha_vlada_v_ukrayini",
                //    Name = "�������� ��������� �� ��������� ����� � �����",
                //    Dir = "�������� ��������� �� ��������� ����� � ����� 84659"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1261042551755/pravo/istoriya_vchen_pro_derzhavu_i_pravo",
                //    Name = "������ ����� ��� ������� � �����",
                //    Dir = "������ ����� ��� ������� � ����� 1261042551755"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1126062244400/pravo/istoriya_derzhavi_i_prava_zarubizhnih_krayin",
                //    Name = "������ ������� � ����� ��������� ����",
                //    Dir = "������ ������� � ����� ��������� ���� 1126062244400"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/76392/pravo/konstitutsionnoe_pravo_zarubezhnyh_stran",
                //    Name = "��������������� ����� ���������� �����",
                //    Dir = "��������������� ����� ���������� ����� 76392"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072010935/pravo/konstitutsiyne_derzhavne_pravo_zarubizhnih_krayin",
                //    Name = "������������� (��������) ����� ��������� ����",
                //    Dir = "������������� (��������) ����� ��������� ���� 1584072010935"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/83433/pravo/konstitutsiyne_pravo_zarubizhnih_krayin",
                //    Name = "������������� ����� ��������� ����",
                //    Dir = "������������� ����� ��������� ���� 83433"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/10811007/pravo/kriminalne_pravo_ukrayini",
                //    Name = "���������� ����� ������",
                //    Dir = "���������� ����� ������ 10811007"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072024423/pravo/kriminalne_pravo_ukrayini",
                //    Name = "���������� ����� ������",
                //    Dir = "���������� ����� ������ 1584072024423"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/71075/pravo/kriminalne_protsesualne_pravo_ukrayini",
                //    Name = "���������� ������������ ����� ������",
                //    Dir = "���������� ������������ ����� ������ 71075"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072061522/pravo/kriminalniy_protses",
                //    Name = "����������� ������",
                //    Dir = "����������� ������ 1584072061522"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1074011055907/pravo/kriminalniy_protses",
                //    Name = "����������� ������",
                //    Dir = "����������� ������ 1074011055907"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072048402/pravo/kriminalniy_protses_ukrayini",
                //    Name = "����������� ������ ������",
                //    Dir = "����������� ������ ������ 1584072048402"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1670090859142/pravo/kriminalniy_protses_ukrayini_osobliva_chastina",
                //    Name = "����������� ������ ������. �������� �������",
                //    Dir = "����������� ������ ������. �������� ������� 1670090859142"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072058311/pravo/kriminalniy_protsesualniy_kodeks_ukrayini_naukovo-praktichniy_komentar",
                //    Name = "����������� ������������� ������ ������. �������-���������� ��������",
                //    Dir = "����������� ������������� ������ ������ �������-���������� �������� 1584072058311"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1233090949171/pravo/kriminalniy_protsesualniy_kodeks_ukrayini_naukovo-praktichniy_komentar",
                //    Name = "����������� ������������� ������ ������. �������-���������� ��������. �1",
                //    Dir = "����������� ������������� ������ ������. �������-���������� ��������. �1 1233090949171"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072050461/pravo/kriminalniy_protsesualniy_kodeks_ukrayini_naukovo-praktichniy_komentar_t2",
                //    Name = "����������� ������������� ������ ������. �������-���������� ��������. �2",
                //    Dir = "����������� ������������� ������ ������. �������-���������� ��������. �2 1584072050461"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/90759/pravo/kriminalno-vikonavche_pravo",
                //    Name = "����������-��������� �����",
                //    Dir = "����������-��������� ����� 90759"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/158407209278/pravo/kriminalno-vikonavche_pravo_ukrayini",
                //    Name = "����������-��������� ����� ������",
                //    Dir = "����������-��������� ����� ������ 158407209278"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/82329/pravo/kriminalno-vikonavche_pravo_ukrayini",
                //    Name = "����������-��������� ����� ������",
                //    Dir = "����������-��������� ����� ������ 82329"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1977041355292/pravo/nastilna_kniga_advokata_u_kriminalniy_spravi",
                //    Name = "�������� ����� �������� � ���������� �����",
                //    Dir = "�������� ����� �������� � ���������� ����� 1977041355292"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072059019/pravo/osnovi_pravoznavstva",
                //    Name = "������ �������������",
                //    Dir = "������ ������������� 1584072059019"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1186112449432/pravo/mizhnarodne_pravo",
                //    Name = "̳�������� �����",
                //    Dir = "̳�������� ����� 1186112449432"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/80100/pravo/mizhnarodne_privatne_pravo",
                //    Name = "̳�������� �������� �����",
                //    Dir = "̳�������� �������� ����� 80100"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1999040547987/pravo/mizhnarodne_privatne_pravo",
                //    Name = "̳�������� �������� �����",
                //    Dir = "̳�������� �������� ����� 1999040547987"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072047166/pravo/mizhnarodne_privatne_pravo",
                //    Name = "̳�������� �������� �����",
                //    Dir = "̳�������� �������� ����� 1584072047166"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072056767/pravo/mizhnarodne_privatne_pravo_osobliva_chastina",
                //    Name = "̳�������� �������� �����. �������� �������",
                //    Dir = "̳�������� �������� �����. �������� ������� 1584072056767"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072046657/pravo/mizhnarodne_publichne_pravo",
                //    Name = "̳�������� ������� �����",
                //    Dir = "̳�������� ������� ����� 1584072046657"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/19991130/pravo/pravo",
                //    Name = "�����",
                //    Dir = "����� 19991130"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/11750204/pravo/pravoznavstvo",
                //    Name = "�������������",
                //    Dir = "������������� 11750204"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/12610515/pravo/pravoznavstvo",
                //    Name = "�������������",
                //    Dir = "������������� 12610515"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/19991130/pravo/pravoznavstvo",
                //    Name = "�������������",
                //    Dir = "������������� 19991130"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072028465/pravo/problemi_teoriyi_derzhavi_i_prava",
                //    Name = "�������� ���� ������� � �����",
                //    Dir = "�������� ���� ������� � ����� 1584072028465"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/81432/pravo/simeyne_pravo_ukrayini",
                //    Name = "ѳ����� ����� ������",
                //    Dir = "ѳ����� ����� ������ 81432"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072022878/pravo/sudova_vlada_v_ukrayini",
                //    Name = "������ ����� � �����",
                //    Dir = "������ ����� � ����� 1584072022878"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/2015073065424/pravo/sudovi_promovi_derzhavnogo_obvinuvacha_ta_advokata-zahisnika_u_kriminalnomu_sudochinstvi_ukrayini",
                //    Name = "����� ������� � ������������ ���������� ������",
                //    Dir = "����� ������� � ������������ ���������� ������"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/85435/pravo/sudovi_ta_pravoohoronni_organi_ukrayini",
                //    Name = "����� �� ������������ ������ ������",
                //    Dir = "����� �� ������������ ������ ������ 85435"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072020653/pravo/suchasne_mizhnarodne_pravo",
                //    Name = "������� ��������� �����",
                //    Dir = "������� ��������� ����� 1584072020653"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072058168/pravo/trudove_pravo_ukrayini",
                //    Name = "������� ����� ������",
                //    Dir = "������� ����� ������ 1584072058168"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1074041343312/pravo/trudove_pravo",
                //    Name = "������� ����� ������",
                //    Dir = "������� ����� ������ 1074041343312"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/89400/pravo/trudove_pravo_ukrayini",
                //    Name = "������� ����� ������",
                //    Dir = "������� ����� ������ 89400"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/69083/pravo/trudove_pravo_ukrayini",
                //    Name = "������� ����� ������",
                //    Dir = "������� ����� ������ 69083"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/2015082666117/pravo/trudove_pravo_ukrayini",
                //    Name = "������� ����� ������",
                //    Dir = "������� ����� ������ 2015082666117"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1547121956142/pravo/ukrayinske_kriminalne_pravo",
                //    Name = "���������� ����� ������",
                //    Dir = "���������� ����� ������ 1547121956142"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1310060844505/pravo/ukrayinske_simeyne_pravo",
                //    Name = "��������� ������ �����",
                //    Dir = "��������� ������ ����� 1310060844505"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072056516/pravo/filosofiya_prava",
                //    Name = "Գ������� �����",
                //    Dir = "Գ������� ����� 1584072056516"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1120121748323/pravo/tsivilniy_protses_ukrayini",
                //    Name = "�������� ������ ������",
                //    Dir = "�������� ������ ������ 1120121748323"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072054563/pravo/tsivilniy_protses",
                //    Name = "�������� ������",
                //    Dir = "�������� ������ 1584072054563"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072046328/pravo/tsivilniy_protses_ukrayini",
                //    Name = "�������� ������ ������",
                //    Dir = "�������� ������ ������ 1584072046328"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1791040757945/pravo/tsivilniy_protses_ukrayini",
                //    Name = "�������� ������ ������",
                //    Dir = "�������� ������ ������ 1791040757945"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072046508/pravo/tsivilne_pravo",
                //    Name = "������� �����",
                //    Dir = "������� ����� 1584072046508"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072057339/pravo/tsivilne_pravo_ukrayini_zagalna_chastina_",
                //    Name = "������� ����� ������. �������� �������",
                //    Dir = "������� ����� ������. �������� ������� 1584072057339"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1029022857219/pravo/tsivilne_pravo_ukrayini_dogovirni_ta_nedogovirni_zobovyazannya",
                //    Name = "������� ����� ������. ������� �� ��������� �����'������",
                //    Dir = "������� ����� ������. ������� �� ��������� �����'������ 1029022857219"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072046944/pravo/tsivilne_pravo_ukrayini",
                //    Name = "������� ����� ������",
                //    Dir = "������� ����� ������ 1584072046944"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072046113/pravo/tsivilne_pravo_ukrayini",
                //    Name = "������� ����� ������",
                //    Dir = "������� ����� ������ 1584072046113"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1657072249700/pravo/tsivilne_pravo_ukrayini_osobliva_chastina",
                //    Name = "������� ����� ������",
                //    Dir = "������� ����� ������ 1657072249700"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072045843/pravo/tsivilne_pravo_tom_1_-_borisova_vi",
                //    Name = "������� �����. ��� 1",
                //    Dir = "������� �����. ��� 1 1584072045843"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072047810/pravo/tsivilne_pravo_tom_2",
                //    Name = "������� �����. ��� 2",
                //    Dir = "������� �����. ��� 2 1584072047810"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072045125/pravo/administrativne_pravo",
                //    Name = "������������� �����",
                //    Dir = "������������� ����� 1584072045125"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1770071255523/pravo/advokatura",
                //    Name = "����������",
                //    Dir = "���������� 1770071255523"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/91020/pravo/administrativna_vidpovidalnist_ta_provadzhennya_v_spravah_pro_administrativni_pravoporushennya",
                //    Name = "������������� ������������� �� ����������� � ������� ��� ������������ ��������������",
                //    Dir = "������������� ������������� �� ����������� � ������� ��� ������������ ��������������"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/91318/pravo/garantiyi_prava_na_zvernennya_gromadyan_do_organiv_vnutrishnih_sprav_ukrayini",
                //    Name = "������ ����� �� ��������� �������� �� ������ �������� ����� ������",
                //    Dir = "������ ����� �� ��������� �������� �� ������ �������� ����� ������"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1801031852155/pravo/zakon_ukrayini_pro_sudoustriy_i_status_suddiv_",
                //    Name = "����� ������ ��� ��������� � ������ �����",
                //    Dir = "����� ������ ��� ��������� � ������ �����"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072058608/pravo/istoriya_derzhavi_i_prava_zarubizhnih_krayin",
                //    Name = "������ ������� � ����� ��������� ����",
                //    Dir = "������ ������� � ����� ��������� ���� 1584072058608"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1071042251923/pravo/istoriya_vchen_pro_derzhavu_i_pravo",
                //    Name = "������ ����� ��� ������� � �����",
                //    Dir = "������ ����� ��� ������� � �����"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072056540/pravo/istoriya_derzhavi_i_prava_zarubizhnih_krayin",
                //    Name = "������ ������� � ����� ��������� ����",
                //    Dir = "������ ������� � ����� ��������� ���� 1584072056540"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072054892/pravo/istoriya_derzhavi_i_prava_ukrayini",
                //    Name = "������ ������� � ����� ������",
                //    Dir = "������ ������� � ����� ������ 1584072054892"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/17390617/pravo/istoriya_derzhavi_i_prava_ukrayini",
                //    Name = "������ ������� � ����� ������",
                //    Dir = "������ ������� � ����� ������ 17390617"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072045407/pravo/istoriya_derzhavi_i_prava_ukrayini",
                //    Name = "������ ������� � ����� ������",
                //    Dir = "������ ������� � ����� ������ 1584072045407"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/75232/pravo/istoriya_derzhavi_i_prava_ukrayini",
                //    Name = "������ ������� � ����� ������",
                //    Dir = "������ ������� � ����� ������ 75232"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1237042343223/pravo/istoriya_derzhavi_ta_prava_ukrayini",
                //    Name = "������ ������� �� ����� ������",
                //    Dir = "������ ������� �� ����� ������ 1237042343223"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072016682/pravo/konstitutsiyne_pravo_ukrayini",
                //    Name = "������������� ����� ������",
                //    Dir = "������������� ����� ������ 1584072016682"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072046786/pravo/konstitutsiyne_pravo_ukrayini",
                //    Name = "������������� ����� ������",
                //    Dir = "������������� ����� ������ 1584072046786"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072046851/pravo/konstitutsiyne_pravo_ukrayini",
                //    Name = "������������� ����� ������",
                //    Dir = "������������� ����� ������ 1584072046851"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1896080148173/pravo/kurs_administrativnogo_prava_ukrayini",
                //    Name = "���� ��������������� ����� ������",
                //    Dir = "���� ��������������� ����� ������ 1896080148173"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1329031147562/pravo/kurs_administrativnogo_protsesu",
                //    Name = "���� ��������������� �������",
                //    Dir = "���� ��������������� �������"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072016370/pravo/osnovi_derzhavi_i_prava_ukrayini",
                //    Name = "������ ������� � ����� ������",
                //    Dir = "������ ������� � ����� ������ 1584072016370"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072018496/pravo/pravoznavstvo",
                //    Name = "�������������",
                //    Dir = "������������� 1584072018496"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/85124/pravo/pravoznavstvo",
                //    Name = "�������������",
                //    Dir = "������������� 85124"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072054412/pravo/sudoustriy",
                //    Name = "���������",
                //    Dir = "��������� 1584072054412"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/78359/pravo/sudoustriy_ukrayini",
                //    Name = "��������� ������",
                //    Dir = "��������� ������ 78359"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1838010451487/dokumentoznavstvo/pravove_pismo",
                //    Name = "������� ������",
                //    Dir = "������� ������ 1838010451487"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/70606/pravo/teoriya_derzhavi_i_prava",
                //    Name = "����� ������� � �����",
                //    Dir = "����� ������� � ����� 70606"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072056692/pravo/teoriya_derzhavi_i_prava",
                //    Name = "����� ������� � �����",
                //    Dir = "����� ������� � ����� 1584072056692"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072014908/pravo/teoriya_derzhavi_i_prava",
                //    Name = "����� ������� � �����",
                //    Dir = "����� ������� � ����� 1584072014908"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/84511/pravo/teoriya_derzhavi_i_prava",
                //    Name = "����� ������� � �����",
                //    Dir = "����� ������� � ����� 84511"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072021816/pravo/teoriya_derzhavi_i_prava",
                //    Name = "����� ������� � �����",
                //    Dir = "����� ������� � ����� 1584072021816"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/2015073165661/pravo/teoriya_derzhavi_i_prava",
                //    Name = "����� ������� � �����",
                //    Dir = "����� ������� � ����� 2015073165661"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1584072045035/pravo/teoriya_derzhavi_ta_prava",
                //    Name = "����� ������� � �����",
                //    Dir = "����� ������� � ����� 1584072045035"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/1262091843012/pravo/teoriya_prava_i_derzhavi",
                //    Name = "����� ������� � �����",
                //    Dir = "����� ������� � ����� 1262091843012"
                //},
                //new HtmlTask
                //{
                //    Url = "https://pidru4niki.com/11570611/pravo/administrativne_pravo",
                //    Name = "������������� �����",
                //    Dir = "������������� ����� 11570611"
                //}
            };

            foreach (var item in list)
            {
                var doc = Parse(item.Dir, item.Url);
                doc.Save(item.Dir + Path.DirectorySeparatorChar + item.Name + ".html");
            }
        }

        protected override string GetHost()
        {
            return "https://pidru4niki.com";
        }

        protected override string GetUrI(string dir, string url)
        {
            return url.Contains("http") ? url : GetHost() + url;
        }

        protected override HtmlNode GetContents(HtmlNode element)
        {
            return element.SelectSingleNode("//article");
        }

        protected override HtmlNode GetElement(HtmlNode element)
        {
            return element.SelectSingleNode("//article");
        }

        protected override void ProcessElement(HtmlNode element, HtmlNode body, string dir, string uri, string text)
        {
            if (element == null)
            {
                return;
            }
            var addedElement = AddElement(element, body);
            ReplaceImages(dir, addedElement);

            //Save(element, text);
        }

        private void Save(HtmlNode element, string uri)
        {
            var newDocument = CreateHtml().OwnerDocument;
            AddElement(element, newDocument.DocumentNode);
            newDocument.Save(Normalize(uri) + ".html", Encoding.UTF8);
        }

        protected override HtmlNode CreateHtml()
        {
            var newDocument = new HtmlDocument();
            var html = newDocument.DocumentNode.AppendChild(newDocument.CreateElement("html"));
            var head = html.AppendChild(newDocument.CreateElement("head"));
            head.InnerHtml = "<link rel=\"stylesheet\" type=\"text/css\" href=\"template_css.css\">";
            var body = html.AppendChild(newDocument.CreateElement("body"));
            return body;
        }
    }
}