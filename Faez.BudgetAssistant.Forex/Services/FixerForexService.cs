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

    public sealed class FixerForexService : IForexService
    {
        private const string Endpoint = "https://api.fixer.io";

        private readonly HttpClient _httpClient;

        public FixerForexService(HttpClient httpClient)
        {
            _httpClient = httpClient.EnsureArgumentNotNull(nameof(httpClient));
        }

        public async Task<ForexRate> GetRateAsync(DateTime date, string fromCurrency, string toCurrency)
        {
            fromCurrency.EnsureArgumentIsCurrency(nameof(fromCurrency));
            toCurrency.EnsureArgumentIsCurrency(nameof(toCurrency));

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
            return $"{Endpoint}/{date:yyyy-MM-dd}?base={fromCurrency}&symbols={toCurrency}";
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
