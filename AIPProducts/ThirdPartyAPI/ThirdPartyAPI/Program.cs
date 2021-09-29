using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using ThirdPartyAPI.Model;

namespace ThirdPartyAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44337/api/Products");
                var responseTask = client.GetAsync("Products");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Product[]>();
                    readTask.Wait();
                    var Products = readTask.Result;
                                     
                    foreach (var Product in Products)
                    {
                        decimal Temp = Convert.ToDecimal(Product.Price);
                        Console.WriteLine(Temp.ToString("C", new CultureInfo("be-BY")));
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
