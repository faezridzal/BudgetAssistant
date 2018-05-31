namespace Faez.BudgetAssistant.Forex.Services
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Core.Extensions;
    using HtmlAgilityPack;
    using Models;

    public sealed class MaybankForexService : IForexService
    {
        private const string Ringgit = "MYR";
        private const string BankName = "Maybank";
        private const string Endpoint = "http://bnm.xrate.org/rate-history";

        private readonly HttpClient _httpClient;

        public MaybankForexService(HttpClient httpClient)
        {
            _httpClient = httpClient.EnsureArgumentNotNull(nameof(httpClient));
        }

        public async Task<ForexRate> GetRateAsync(DateTime date, string fromCurrency, string toCurrency)
        {
            fromCurrency.EnsureArgumentIsCurrency(nameof(fromCurrency));
            toCurrency.EnsureArgumentIsCurrency(nameof(toCurrency));

            if (fromCurrency == Ringgit && toCurrency == Ringgit)
            {
                return new ForexRate
                {
                    Date = DateTime.Today,
                    FromCurrency = Ringgit,
                    ToCurrency = Ringgit,
                    Rate = 1
                };
            }

            if (fromCurrency != Ringgit && toCurrency != Ringgit)
            {
                throw new InvalidOperationException($"One of the currency leg must be an {Ringgit}");
            }

            var path = GetQueryString(date);
            var document = new HtmlDocument();

            using (var stream = await _httpClient.GetStreamAsync(path))
            {
                document.Load(stream);
            }

            var isBuy = toCurrency == Ringgit;
            var currency = isBuy ? fromCurrency : toCurrency;

            return document.DocumentNode == null ? null : ParseDocument(document.DocumentNode, isBuy, currency);
        }

        private static string GetQueryString(DateTime date)
        {
            return $"{Endpoint}/{date:yyyy-MM-dd}";
        }

        private static ForexRate ParseDocument(HtmlNode htmlNode, bool isBuy, string currency)
        {
            if (!TryGetColumnIndex(htmlNode, out var columnIndex))
            {
                return null;
            }

            var bodyRows = htmlNode.SelectSingleNode("//tbody");

            foreach (var rowNode in bodyRows.SelectNodes("//tr"))
            {
                // TODO: Parse row pairs
            }

            return null;
        }

        private static bool TryGetColumnIndex(HtmlNode htmlNode, out int columnIndex)
        {
            columnIndex = 0;

            var headerRow = htmlNode.SelectSingleNode("//thead");

            if (headerRow == null)
            {
                return false;
            }
            
            foreach (var columnNode in headerRow.SelectNodes("//td"))
            {
                if (columnNode.InnerText == BankName)
                {
                    return true;
                }

                columnIndex++;
            }

            return false;
        }
    }
}