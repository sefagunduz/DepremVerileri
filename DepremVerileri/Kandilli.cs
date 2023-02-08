using HtmlAgilityPack;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace DepremVerileri
{
    public class Kandilli
    {
        private static string GoogleMapsUrl(string enlem, string boylam, int zoom)
        {
            string url = "https://www.google.com/maps/@" + enlem + "," + boylam + "," + zoom + "z";
            return url;
        }

        public static List<DepremData> SonVeriler()
        {
            List<DepremData> list = new List<DepremData>();

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string webData = wc.DownloadString("http://www.koeri.boun.edu.tr/scripts/sondepremler.asp");

            //string webData = System.Text.Encoding.UTF8.GetString(raw);

            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(webData);

            HtmlNode htmlNode = htmlDocument.DocumentNode.SelectSingleNode("//pre");
            string icerik = htmlNode.InnerText;

            string[] icerikler = icerik.Split("--------------");
            string depremler = icerikler.Last().Trim();

            string[] depremList = depremler.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);

            foreach (string item in depremList)
            {
                DepremData depremData = new DepremData();

                Match m1 = Regex.Match(item, (@"(\d{4}).(\d{2}).(\d{2}) (\d{2}):(\d{2}):(\d{2})"));
                string dt = m1.Value;
                depremData.TarihSaat = Convert.ToDateTime(dt);

                Match m2 = Regex.Match(item, (@"[0-9][0-9].[0-9][0-9][0-9][0-9]   [0-9][0-9].[0-9][0-9][0-9][0-9]"));
                string coor = m2.Value;
                string[] coors = coor.Split("   ");
                string enlem = coors.First().Trim(), boylam = coors.Last().Trim();
                depremData.Enlem = enlem;
                depremData.Boylam = boylam;
                depremData.GoogleMapsLink = GoogleMapsUrl(depremData.Enlem, depremData.Boylam, 15);

                Match m3 = Regex.Match(item, (@"   [A-Z]\w+        "));
                Match m4 = Regex.Match(item, (@"   [A-Z]\w+.\w+ .\w+.        "));
                Match m5 = Regex.Match(item, (@"   [A-Z]\w+..\w+.        "));
                string yer = m3.Value;
                if (string.IsNullOrEmpty(yer))
                {
                    yer = m4.Value;
                }
                if (string.IsNullOrEmpty(yer))
                {
                    yer = m5.Value;
                }
                depremData.Yer = yer.Trim();

                Match m6 = Regex.Match(item, (@"[0-9]+\.[0-9]      "));
                string derinlik = m6.Value;
                depremData.DerinlikKM = derinlik.Trim();

                Match m7 = Regex.Match(item, (@"\-.-  [0-9].[0-9]  "));
                string ML = m7.Value.Replace("-.-", "").Trim();
                depremData.Buyukluk = ML;
                depremData.Tip = "ML";

                depremData.ID = depremData.TarihSaat.ToString("MMddyyHHmmss");

                list.Add(depremData);
            }

            return list;
        }
    }
}
