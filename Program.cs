using System;
using System.Net.Http;
using System.Threading.Tasks;
using HavaDurumuBahar;
using Newtonsoft.Json;

class Program
{
    static async Task Main()
    {
        try
        {
            while (true)
            {
                Console.Write("Lütfen hava durumunu öğrenmek istediğiniz şehri giriniz (istanbul, izmir, ankara): ");
                string sehir = Console.ReadLine().ToLower();

                while (string.IsNullOrWhiteSpace(sehir) || !IsValidCity(sehir))
                {
                    Console.Write("Lütfen geçerli bir şehir adı giriniz (istanbul, izmir, ankara): ");
                    sehir = Console.ReadLine().ToLower();
                }

                await DisplayWeatherInfo(sehir);

                Console.WriteLine("\nBaşka bir şehir görmek için 1'e basın, programı kapatmak için başka bir tuşa basın.");
                if (Console.ReadKey().KeyChar != '1')
                {
                    break;
                }
                Console.Clear();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
        }
    }

    static async Task DisplayWeatherInfo(string sehir)
    {
        while (true)
        {
            string apiUrl = $"https://goweather.herokuapp.com/weather/{sehir}";
            WeatherData sehirData = await GetWeatherData(apiUrl);

            if (sehirData != null)
            {
                Console.Clear(); // Ekranı temizle
                Console.WriteLine($"Gerçek Zamanlı Tarih: {DateTime.Now}");
                Console.WriteLine($"{char.ToUpper(sehir[0]) + sehir.Substring(1)} Hava Durumu:");
                PrintWeatherData(sehirData);
            }
            else
            {
                Console.WriteLine($"Belirtilen '{sehir}' şehri için hava durumu verisi bulunamadı.");
            }

            Console.WriteLine("\nBaşka bir şehir görmek için 1'e basın, programı kapatmak için başka bir tuşa basın.");
            if (Console.ReadKey().KeyChar != '1')
            {
                break;
            }
            Console.Clear();
        }
    }

    static async Task<WeatherData> GetWeatherData(string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(jsonContent);

                return weatherData;
            }
            else
            {
                return null;
            }
        }
    }

    static void PrintWeatherData(WeatherData weatherData)
    {
        Console.WriteLine($"Sıcaklık: {weatherData.Temperature}");
        Console.WriteLine($"Rüzgar: {weatherData.Wind}");
        Console.WriteLine($"Durum: {weatherData.Description}");

        Console.WriteLine("\nTahmini Hava Durumu:");

        DateTime today = DateTime.Now.Date;
        for (int i = 0; i < Math.Min(weatherData.Forecast.Length, 3); i++)
        {
            DateTime forecastDate = today.AddDays(i + 1);
            string dayName = GetDayName(forecastDate.DayOfWeek);
            Console.WriteLine($"Tarih: {forecastDate:yyyy-MM-dd} ({dayName}), " +
                              $"Sıcaklık: {weatherData.Forecast[i].Temperature}, " +
                              $"Durum: {weatherData.Forecast[i].Description}");
        }
    }

    static string GetDayName(DayOfWeek dayOfWeek)
    {
        switch (dayOfWeek)
        {
            case DayOfWeek.Sunday:
                return "Pazar";
            case DayOfWeek.Monday:
                return "Pazartesi";
            case DayOfWeek.Tuesday:
                return "Salı";
            case DayOfWeek.Wednesday:
                return "Çarşamba";
            case DayOfWeek.Thursday:
                return "Perşembe";
            case DayOfWeek.Friday:
                return "Cuma";
            case DayOfWeek.Saturday:
                return "Cumartesi";
            default:
                return string.Empty;
        }
    }

    static bool IsValidCity(string city)
    {
        string[] validCities = { "istanbul", "izmir", "ankara" };
        return Array.IndexOf(validCities, city) != -1;
    }
}
