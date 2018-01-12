namespace Faez.BudgetAssistant.Forex.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Core.Extensions;
    using Models;
    using Newtonsoft.Json;

    public sealed class FixerForexService : ILatestRateService, IHistoricalRateService
    {
        private const string Endpoint = "https://api.fixer.io";

        private readonly HttpClient _httpClient;

        public FixerForexService(HttpClient httpClient)
        {
            _httpClient = httpClient.EnsureArgumentNotNull(nameof(httpClient));
        }

        public async Task<ForexRate> GetLatestRateAsync(string fromCurrency, string toCurrency)
        {
            var path = GetQueryString(DateTime.Today, fromCurrency, toCurrency);

            using (var stream = await _httpClient.GetStreamAsync(path))
            using (var streamReader = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                var model = serializer.Deserialize<FixerModel>(reader);

                return new ForexRate
                {
                    Date = DateTime.Today,
                    FromCurrency = fromCurrency,
                    ToCurrency = toCurrency,
                    Rate = model.Rates[toCurrency]
                };
            }
        }

        public async Task<ForexRate> GetHistoricalRateAsync(DateTime date, string fromCurrency, string toCurrency)
        {
            var path = GetQueryString(date, fromCurrency, toCurrency);

            using (var stream = await _httpClient.GetStreamAsync(path))
            using (var streamReader = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                var model = serializer.Deserialize<FixerModel>(reader);

                return new ForexRate
                {
                    Date = date,
                    FromCurrency = fromCurrency,
                    ToCurrency = toCurrency,
                    Rate = model.Rates[toCurrency]
                };
            }
        }

        private static string GetQueryString(DateTime date, string fromCurrency, string toCurrency)
        {
            var routing = date.Date == DateTime.Today ? "latest" : date.ToString("yyyy-MM-dd");

            return $"{Endpoint}/{routing}?base={fromCurrency}&symbols={toCurrency}";
        }

        private sealed class FixerModel
        {
            public FixerModel()
            {
                Rates = new Dictionary<string, decimal>();
            }

            public DateTime Date { get; set; }

            public string Base { get; set; }

            public IDictionary<string, decimal> Rates { get; private set; }
        }
    }
}
