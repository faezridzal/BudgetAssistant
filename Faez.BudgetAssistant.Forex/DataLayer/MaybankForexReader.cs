namespace Faez.BudgetAssistant.Forex.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using HtmlAgilityPack;
    using Models;

    public sealed class MaybankForexReader : IForexReader
    {
        public IEnumerable<ForexRate> ReadAll(string payload)
        {
            var document = new HtmlDocument();

            document.LoadHtml(payload);

            var qualifiedNodes = document.DocumentNode.SelectNodes("//tr");
            var sellNode = qualifiedNodes.FirstOrDefault(n => n.ChildNodes.Any(c => c.InnerText == "EUR"));

            if (sellNode == null)
            {
                yield break;
            }

            var sellRate = decimal.Parse(sellNode.ChildNodes.Skip(11).Take(1).Single().InnerText, CultureInfo.InvariantCulture);

            yield return new ForexRate
            {
                Date = DateTime.Today,
                FromCurrency = "MYR",
                ToCurrency = "EUR",
                Rate = sellRate
            };
        }
    }
}
