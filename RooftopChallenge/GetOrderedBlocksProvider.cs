using System;
using System.Net.Http;
using RooftopChallenge.Core.Actions;
using RooftopChallenge.Infrastructure.Services;

namespace RooftopChallenge
{
    public class GetOrderedBlocksProvider
    {

        public static GetOrderedBlocks GetOrderedBlocks
        {
            get
            {
                var httpClient = BuildHttpClient();
                var checkBlockService = new HttpCheckBlockService(httpClient);
                var getOrderedBlocksAction = new GetOrderedBlocks(checkBlockService);

                return getOrderedBlocksAction;
            }
        }
        private static HttpClient BuildHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://rooftop-career-switch.herokuapp.com");
            return httpClient;
        }
    }
    
    
}