Hava Durumu Bilgi Uygulaması

Bu konsol uygulaması, kullanıcıdan bir şehir adı alarak, o şehre ait güncel hava durumu bilgilerini ve 3 günlük tahminleri görüntüler. Hava durumu verileri, [goweather.herokuapp.com](https://goweather.herokuapp.com/) adresinden alınır.

- Kullanım

1. *Projeyi Başlatma:*
   - Visual Studio'da projeyi açın.
   - `Program.cs` dosyasındaki `Main` metodunu çalıştırın.

2. **Şehir Seçimi:**
   - Konsol size hangi şehirin hava durumunu öğrenmek istediğinizi soracaktır.
   - Kabul edilen şehir adları: İstanbul, İzmir veya Ankara.

3. **Hava Durumu Bilgileri Görüntüleme:**
   - Girilen geçerli bir şehir adına ait hava durumu bilgileri konsola yazdırılır.
   - Şu anki sıcaklık, rüzgar hızı, hava durumu açıklaması ve 3 günlük tahmini hava durumu bilgileri görüntülenir.

4. **Devam Etmek veya Kapatmak:**
   - İsterseniz başka bir şehirin hava durumunu görmek için "1" tuşuna basabilirsiniz.
   - Programı kapatmak için başka bir tuşa basabilirsiniz.

- Kod Yapısı

- `CityWeather` Sınıfı

- `Temperature`: Şu anki sıcaklık bilgisini tutar.
- `Wind`: Rüzgar hızı bilgisini tutar.
- `Description`: Hava durumu açıklamasını tutar.
- `Forecast`: 3 günlük tahmini hava durumu bilgilerini içeren dizi.

- `Forecast` Sınıfı

- `Day`: Gün adını tutar.
- `Temperature`: Günlük sıcaklık bilgisini tutar.
- `Wind`: Günlük rüzgar hızı bilgisini tutar.

- `WeatherService` Sınıfı

- `BaseApiUrl`: Hava durumu API'sinin temel URL'sini tanımlar.
- `GetCityWeather(string city)`: Verilen şehir adına ait hava durumu bilgilerini API üzerinden alır ve `CityWeather` sınıfı olarak döndürür.

- `Program` Sınıfı

- Kullanıcıdan şehir adını alır ve geçerliliğini kontrol eder.
- Hava durumu bilgilerini almak için `WeatherService` sınıfını kullanır.
- Alınan bilgileri ekrana yazdırır.
- Hata durumlarında uygun hata mesajını ekrana yazdırır.

- Bağımlılıklar

- [Newtonsoft.Json](https://www.newtonsoft.com/json): JSON serileştirme/deserileştirme işlemleri için kullanılır.
- [System.Net.Http](https://docs.microsoft.com/en-us/dotnet/api/system.net.http): HTTP istekleri göndermek için kullanılır.

- Hata Durumları

- Geçerli bir şehir adı girilmezse veya hava durumu bilgileri alınamazsa, kullanıcıya bir hata mesajı gösterilir.
