using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;   

namespace IronCenter.Desktop.Helpers
{
    public class GetUsdRate
    {
        public static async Task<decimal> UzGetUsdRateFromCbuAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync("https://cbu.uz/uz/arkhiv-kursov-valyut/json/");
                var data = JArray.Parse(response);

                foreach (var item in data)
                {
                    if (item["Ccy"].ToString() == "USD")
                    {
                        return decimal.Parse(item["Rate"].ToString());
                    }
                }

                throw new Exception("USD kursi topilmadi.");
            }
        }

        public static async Task<decimal> RubGetUsdRateFromCbuAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync("https://cbu.uz/uz/arkhiv-kursov-valyut/json/");
                var data = JArray.Parse(response);

                foreach (var item in data)
                {
                    if (item["Ccy"].ToString() == "USD")
                    {
                        return decimal.Parse(item["Rate"].ToString());
                    }
                }

                throw new Exception("USD kursi topilmadi.");
            }
        }

        public static async Task<decimal> EurGetUsdRateFromCbuAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync("https://cbu.uz/uz/arkhiv-kursov-valyut/json/");
                var data = JArray.Parse(response);

                foreach (var item in data)
                {
                    if (item["Ccy"].ToString() == "USD")
                    {
                        return decimal.Parse(item["Rate"].ToString());
                    }
                }

                throw new Exception("USD kursi topilmadi.");
            }
        }
    }
}
