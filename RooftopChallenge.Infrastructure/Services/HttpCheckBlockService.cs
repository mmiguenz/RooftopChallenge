using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RooftopChallenge.Core.Domain;

namespace RooftopChallenge.Infrastructure.Services
{
    public class HttpCheckBlockService : ICheckBlockService
    {
        private readonly HttpClient _httpClient;

        public HttpCheckBlockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> AreConsequent(ImmutableList<string> list)
        {
            var request = new CheckBlockRequest(list.ToList());
            var jsonRequest = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            var checkBlockResponse =
                await _httpClient.PostAsync(
                    "/check?token=28320b9d-bd3b-4ade-a655-12899924276f",
                    jsonRequest);
            var response = await checkBlockResponse.Content.ReadFromJsonAsync<CheckBlockResponse>();

            return response?.message ?? false;
        }
    }

    public class GetBlocksResponse
    {
        public List<String> Data { get; set; }
        public int ChunkSize { get; set; }
        public int Length { get; set; }
    }

    public record CheckBlockResponse(bool message);

    internal record CheckBlockRequest(List<string> blocks);
}