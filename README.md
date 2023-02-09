# Deprem Verileri
## Türkiye Deprem Verilerini Çekebileceğiniz Servisler

Kandilli Rasathanesi verilerini siteden çekip parse eder

Afad verilerini siteden çekip okur

USGS verilerini API üzerinden çekip Turkey kelimesi ile son depremlere göre filtreler.

```
using DepremVerileri;

/* Afad üzerinden verileri çekme */

List<DepremData> liste1 = Afad.SonVeriler();
Console.WriteLine("---------------------------------------------------------");
foreach (DepremData item in liste1)
{
    Console.WriteLine("ID:{0} - Tarih:{1} - Enlem:{2} - Boylam:{3} - Derinlik(KM):{4} - Tip:{5} - Büyüklük:{6} - Yer:{7} "
        , item.ID, item.TarihSaat, item.Enlem, item.Boylam, item.DerinlikKM, item.Tip, item.Buyukluk, item.Yer);
}

/* Kandilli üzerinden verileri çekme */

List<DepremData> liste2 = Kandilli.SonVeriler();
Console.WriteLine("---------------------------------------------------------");
foreach (DepremData item in liste2)
{
    Console.WriteLine("ID:{0} - Tarih:{1} - Enlem:{2} - Boylam:{3} - Derinlik(KM):{4} - Tip:{5} - Büyüklük:{6} - Yer:{7} "
        , item.ID, item.TarihSaat, item.Enlem, item.Boylam, item.DerinlikKM, item.Tip, item.Buyukluk, item.Yer);
}


/* USGS üzerinden verileri çekme */

List<Feature> liste3 = USGS.SonVeriler();
Console.WriteLine("---------------------------------------------------------");
foreach (Feature item in liste3)
{
    Console.WriteLine("ID:{0} - Tarih:{1} - Enlem:{2} - Boylam:{3} - Derinlik(KM):{4} - Tip:{5} - Büyüklük:{6} - Yer:{7} "
        , item.id,
        DateTimeOffset.FromUnixTimeMilliseconds((long)item.properties.time).UtcDateTime, 
        item.geometry.coordinates[0], 
        item.geometry.coordinates[1], 
        "-",
        item.properties.magType, item.properties.mag, item.properties.place);
}

Console.WriteLine("---------------------------------------------------------");

```