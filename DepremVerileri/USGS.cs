using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace DepremVerileri
{
    public class USGS
    {
        private static string UrlHazirla()
        {
            string url = "https://earthquake.usgs.gov/fdsnws/event/1/query?format=geojson";
            string ek1 = "&starttime=" + DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd");
            string ek2 = "&endtime=" + DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd");
            url = url + ek1 + ek2;

            return url;
        }

        public static List<Feature> SonVeriler()
        {
            List<Feature> list = new List<Feature>();

            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string webData = wc.DownloadString(UrlHazirla());

            Root sonuc = JsonConvert.DeserializeObject<Root>(webData);
            list = sonuc.features.Where(x => x.properties.title.Contains("Turkey")).ToList();

            return list;
        }
    }

    public class Feature
    {
        public string type { get; set; }
        public Properties properties { get; set; }
        public Geometry geometry { get; set; }
        public string id { get; set; }

        public class Properties
        {
            public double mag { get; set; }
            public string place { get; set; }
            public object time { get; set; }
            public object updated { get; set; }
            public object tz { get; set; }
            public string url { get; set; }
            public string detail { get; set; }
            public int? felt { get; set; }
            public double? cdi { get; set; }
            public double? mmi { get; set; }
            public string alert { get; set; }
            public string status { get; set; }
            public int tsunami { get; set; }
            public int sig { get; set; }
            public string net { get; set; }
            public string code { get; set; }
            public string ids { get; set; }
            public string sources { get; set; }
            public string types { get; set; }
            public int? nst { get; set; }
            public double? dmin { get; set; }
            public double rms { get; set; }
            public double? gap { get; set; }
            public string magType { get; set; }
            public string type { get; set; }
            public string title { get; set; }
        }
    }

    public class Geometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Metadata
    {
        public long generated { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public int status { get; set; }
        public string api { get; set; }
        public int count { get; set; }
    }

    public class Root
    {
        public string type { get; set; }
        public Metadata metadata { get; set; }
        public List<Feature> features { get; set; }
        public List<double> bbox { get; set; }

    }


}
