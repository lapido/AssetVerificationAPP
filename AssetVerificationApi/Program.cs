using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using Microsoft.Owin.Hosting;

namespace AssetVerificationApi
{
    public class Program
    {
        static void Main()
        {
            string baseAddress = "http://localhost:9000/";

            using(WebApp.Start<Startup>(url: baseAddress))
            {
                HttpClient client = new HttpClient();
                var response = client.GetAsync(baseAddress + "api/values").Result;

                if (response != null)
                {
                    Console.WriteLine("Information from service: {0}", response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    Console.WriteLine("Error: Impossible to connect to service");
                }

                Console.Write(response);
                Console.WriteLine("Press ENTER to stop the server and close the app...");
                Console.ReadLine();

            }
        }
    }
}