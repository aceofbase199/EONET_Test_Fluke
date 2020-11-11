using EONET.DAL.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace EONET.Api.Client
{
    public class HttpEonetClient : IHttpEonetClient
    {
        private const int DEFAULT_LIMIT_EVENTS = 100;

        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpEonetClient> _logger;
        public HttpEonetClient(IHttpClientFactory clientFactory, ILogger<HttpEonetClient> logger)
        {
            _httpClient = clientFactory.CreateClient("eonetClient");
            _logger = logger;
        }

        public async Task<List<Event>> GetEventsAsync(int days = 20, bool isOpen = true)
        {
            try
            {
                var url = $"events?days={days}&status={(isOpen ? "open" : "closed")}&limit={DEFAULT_LIMIT_EVENTS}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                var responseJson = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<RootEvent>(responseJson);

                return model.Events;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                return null;
            }
        }

        public async Task<Event> GetEventAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"events/{id}?limit={DEFAULT_LIMIT_EVENTS}");
                response.EnsureSuccessStatusCode();
                var responseJson = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Event>(responseJson);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                return null;
            }
        }

        public async Task<List<Category>> GetCategoriesAsync(bool isOpen = true)
        {
            try
            {
                var response = await _httpClient.GetAsync($"categories?status=${(isOpen ? "open" : "closed")}&limit=${DEFAULT_LIMIT_EVENTS}");
                response.EnsureSuccessStatusCode();
                var responseJson = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<RootCategory>(responseJson);

                return model.Categories;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                return null;
            }
        }
    }
}