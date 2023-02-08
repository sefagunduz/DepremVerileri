using HtmlAgilityPack;
using System.Net;
using System.Text;

namespace DepremVerileri
{
    public class Afad
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
            string webData = wc.DownloadString("https://deprem.afad.gov.tr/last-earthquakes.html");

            //string webData = System.Text.Encoding.UTF8.GetString(raw);

            HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(webData);

            HtmlNodeCollection htmlNodes = htmlDocument.DocumentNode.SelectNodes("//table[@class='content-table']/tbody/tr");

            if (htmlNodes != null)
            {
                foreach (HtmlNode item in htmlNodes)
                {
                    HtmlAgilityPack.HtmlDocument _subDocument = new HtmlAgilityPack.HtmlDocument();
                    _subDocument.LoadHtml(item.InnerHtml);

                    HtmlNodeCollection linkNodes = _subDocument.DocumentNode.SelectNodes("//td");
                    if (linkNodes != null)
                    {
                        DepremData depremData = new DepremData();

                        int tdcount = 0;
                        foreach (HtmlNode subitem in linkNodes)
                        {
                            tdcount++;
                            switch (tdcount)
                            {
                                case 1:
                                    depremData.TarihSaat = Convert.ToDateTime(subitem.InnerText);
                                    break;
                                case 2:
                                    depremData.Enlem = subitem.InnerText;
                                    break;
                                case 3:
                                    depremData.Boylam = subitem.InnerText;
                                    break;
                                case 4:
                                    depremData.DerinlikKM = subitem.InnerText;
                                    break;
                                case 5:
                                    depremData.Tip = subitem.InnerText;
                                    break;
                                case 6:
                                    depremData.Buyukluk = subitem.InnerText;
                                    break;
                                case 7:
                                    depremData.Yer = subitem.InnerText;
                                    break;
                                case 8:
                                    depremData.ID = subitem.InnerText;
                                    break;
                                default:
                                    break;
                            }
                        }
                        depremData.GoogleMapsLink = GoogleMapsUrl(depremData.Enlem, depremData.Boylam, 15);
                        list.Add(depremData);

                    }

                }
            }
            return list;
        }
    }
}
