using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Prisma.Models;

namespace Prisma.Services
{
    public class GeminiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string _apiKey;
        private readonly string _apiEndpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent";

        public GeminiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _apiKey = _configuration["ApiKeys:GeminiApiKey"] ?? "AIzaSyByiEpp97Z3PZFUl8pkqQ9FaLkWlKsCGt0";
        }

        public async Task<string> AnalyzeArticleAsync(NewsArticle article, string prompt)
        {
            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        role = "user",
                        parts = new[]
                        {
                            new
                            {
                                text = $"{prompt}\n\nARTICLE TITLE: {article.Title}\n\nARTICLE CONTENT: {article.Summary}\n\nARTICLE SECTION: {article.Section}\n\nAUTHOR: {article.Author}"
                            }
                        }
                    }
                }
            };

            var jsonContent = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiEndpoint}?key={_apiKey}", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var responseJson = JsonDocument.Parse(responseBody);

                // Naviga nel JSON per estrarre il contenuto della risposta
                var candidates = responseJson.RootElement.GetProperty("candidates")[0];
                var content_parts = candidates.GetProperty("content").GetProperty("parts")[0];
                var analysis = content_parts.GetProperty("text").GetString();

                return analysis;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gemini API error: {response.StatusCode}, {errorContent}");
            }
        }
    }
}