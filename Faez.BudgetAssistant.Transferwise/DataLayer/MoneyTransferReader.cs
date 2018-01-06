namespace Faez.BudgetAssistant.Transferwise.DataLayer
{
    using System.Collections.Generic;
    using System.IO;
    using Core.Extensions;
    using CsvHelper;
    using CsvHelper.Configuration;
    using Models;

    public sealed class MoneyTransferReader : IMoneyTransferReader
    {
        private readonly string _file;

        public MoneyTransferReader(string file)
        {
            _file = file.EnsureArgumentNotNullOrWhitespace(nameof(file));

            if (!File.Exists(_file))
            {
                throw new FileNotFoundException("Cannot find a Transferwise file", _file);
            }
        }

        public IEnumerable<MoneyTransfer> Read()
        {
            var configuration = new Configuration { HasHeaderRecord = true };

            using (var reader = new CsvReader(File.OpenText(_file), configuration))
            {
                foreach (var moneyTransfer in reader.GetRecords<MoneyTransfer>())
                {
                    yield return moneyTransfer;
                }
            }
        }
    }
}
