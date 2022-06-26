using System;
using System.Collections.Generic;
using System.Linq;
using RooftopChallenge.Core.Actions;
using RooftopChallenge.Infrastructure;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RooftopChallenge.Infrastructure.Services;

namespace RooftopChallenge
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = BuildHttpClient();
            var checkBlockService = new HttpCheckBlockService(httpClient);
            var getOrderedBlocksAction = new GetOrderedBlocks(checkBlockService);

            var blocks = new List<string>()
            {
                "6BUazic9PVoeqVAFUOJ3Whp8Aj5Z7WRjtojZBmGHysui6qhHnAXCpEjgPkADBhDQAmE5eaCYa4ogCsie9aXHuHCNXeZzzKvXs4az",
                "cTio7Nvz1mBApi1FURO2PZu6exa0GVuz6jOr9VitiNmXgMJphHbVClSDwRR8LSkgeXami5ybLvQgtTY52jZS8YtuFkZqGXA68NsX",
                "EtvHE2hCiSh5uzKbk2pyEpI0BqC5L2BZEFE8RcMeiZmjfTRzkPfJsWie2UsoqcO0oeAYiuyCy6w2TY8ajn80dn8TKZdsLWZ9jgRH",
                "QgZ4i1KEYXfXX52muUd360WDVh03yuHOPJIAWM4jQo4FVystTUpNg9aLSFPFeOrvDwQZzElZXyNBnyL5ylLXW1IPRUP4RYms4cBG",
                "XxYWDRvStrq9R7aztRxLPXYPod67PQGBe6tDqvyUFwWnnZGnZe64jGQ34O1eJuO6cJ5DUfW7EPMHTuaCiU4xXgynpPpbClQYBsmr",
                "nI8TA2r5KtvOGxuoq2iQrLMokLHVkQwn5UEezJKxJM7cXglaUnwsphvu8C6UacLIDY7Wja5pJuBdxuajjHM9RyrlUcabOEqzsPpJ",
                "cryzSZVdKGalqUYV6XcuqhxqU8zjlzKXb1sotytdqqCXE5UjD7WSnNEUsg99iKdpmVIUCx6vHDzJhSSEH1LljpR5BqzIzQCcrNId",
                "8d4OqfTOZuEajZCUYVyDMNfVtg0sIZDworu4g3yX2DOiZC9CZF4kvosYzdxfZYdqvBfulo3aDraGsboRZ0kfPlCDjzNPWtTINQje",
                "f2oLIsvUn2vVV1w8GOSNSniafEVlLjAdiTvih5HvpYIKn1y6nFohZr2ObEmILyRIItPYaH3iaRMaitr35JI5pGGeRxdbivXccDpo"
            };
            var orderedBlocks =  await  getOrderedBlocksAction.Invoke(blocks);
            Console.WriteLine($"Ordered Blocks: {string.Join("",orderedBlocks)}");


            var request = new {encoded = string.Join("", orderedBlocks)};
            var jsonRequest = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);
            
            var checkBlockResponse =
                await httpClient.PostAsync(
                    "/check?token=28320b9d-bd3b-4ade-a655-12899924276f",
                    jsonRequest);
            
            var response = await checkBlockResponse.Content.ReadFromJsonAsync<CheckBlockResponse>();
            
            
            Console.WriteLine($"Result verification: {response?.message}");
        }

        private static HttpClient BuildHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://rooftop-career-switch.herokuapp.com");
            return httpClient;
        }
    }
}
