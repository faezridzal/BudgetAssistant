namespace Faez.BudgetAssistant.Transferwise.DataLayer
{
    using System.Collections.Generic;
    using System.IO;
    using Core.Extensions;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Models;

    public sealed class MoneyTransferWriter : IMoneyTransferWriter
    {
        private readonly string _file;

        public MoneyTransferWriter(string file)
        {
            _file = file.EnsureArgumentNotNullOrWhitespace(nameof(file));
        }

        public void Write(IEnumerable<MoneyTransfer> moneyTransfers)
        {
            moneyTransfers.EnsureArgumentNotNull(nameof(moneyTransfers));

            var configuration = new Configuration { HasHeaderRecord = true };

            configuration.RegisterClassMap<MoneyTransferMapping>();

            using (var streamWriter = File.CreateText(_file))
            using (var writer = new CsvWriter(streamWriter, configuration))
            {
                writer.WriteHeader<MoneyTransfer>();
                streamWriter.WriteLine();
                writer.WriteRecords(moneyTransfers);
            }
        }

        private class MoneyTransferMapping : ClassMap<MoneyTransfer>
        {
            public MoneyTransferMapping()
            {
                AutoMap();
                Map(t => t.Date).TypeConverterOption.Format("yyyy-MM-dd");
            }
        }
    }
}
