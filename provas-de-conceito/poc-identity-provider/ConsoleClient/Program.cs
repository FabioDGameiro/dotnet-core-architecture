using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Project.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //ClientCredentialsTest().Wait();
            ResourceOwnerPasswordTest().Wait();

            Console.ReadKey();
        }

        public static async Task ClientCredentialsTest()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("https://localhost:44373");

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:3000/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }

        public static async Task ResourceOwnerPasswordTest()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("https://localhost:44373");

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "taskmvc", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("tiago", "tiago", "api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:3000/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
