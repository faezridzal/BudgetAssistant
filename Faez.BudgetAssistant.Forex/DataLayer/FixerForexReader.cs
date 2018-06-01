namespace Faez.BudgetAssistant.Forex.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public sealed class FixerForexReader : IForexReader
    {
        public IEnumerable<ForexRate> ReadAll(string payload)
        {
            using (var stringReader = new StringReader(payload))
            using (var reader = new JsonTextReader(stringReader))
            {
                var serializer = new JsonSerializer();
                var fixerPayload = serializer.Deserialize<FixerPayload>(reader);

                foreach (var rate in fixerPayload.Rates)
                {
                    yield return new ForexRate
                    {
                        Date = fixerPayload.Date,
                        FromCurrency = fixerPayload.FromCurrency,
                        ToCurrency = rate.Key,
                        Rate = rate.Value
                    };
                }
            }
        }

        private sealed class FixerPayload
        {
            [JsonProperty(PropertyName = "base")]
            public string FromCurrency { get; set; }

            [JsonConverter(typeof(DateOnlyConverter))]
            [JsonProperty(PropertyName = "date")]
            public DateTime Date { get; set; }

            public IDictionary<string, decimal> Rates { get; set; }
        }

        private sealed class DateOnlyConverter : IsoDateTimeConverter
        {
            public DateOnlyConverter()
            {
                base.DateTimeFormat = "yyyy-MM-dd";
            }
        }
    }
}
