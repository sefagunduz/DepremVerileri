namespace DepremVerileri
{
    public class DepremData
    {
        public string ID { get; set; }
        public DateTime TarihSaat { get; set; }
        public string Enlem { get; set; }
        public string Boylam { get; set; }
        public string DerinlikKM { get; set; }
        public string Tip { get; set; }
        public string Buyukluk { get; set; }
        public string Yer { get; set; }
        public string GoogleMapsLink { get; set; }
    }
}
