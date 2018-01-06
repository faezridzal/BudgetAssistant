namespace Faez.BudgetAssistant.Transferwise.UnitTests.DataLayer
{
    using System;
    using System.IO;
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;
    using Transferwise.DataLayer;

    [TestFixture]
    public sealed class MoneyTransferReaderTests
    {
        private const string Euro = "EUR";
        private const string Ringgit = "MYR";
        private const decimal EffectiveRate = 4;
        private const decimal Amount = 1000;
        
        private static readonly string FileName = Path.GetTempFileName();
        private static readonly DateTime Date = DateTime.Today; 

        [OneTimeSetUp]
        public void Setup()
        {
            using (var writer = File.CreateText(FileName))
            {
                writer.WriteLine("Date,FromCurrency,ToCurrency,EffectiveRate,Amount");
                writer.WriteLine($"{Date:yyyy-MM-dd},{Euro},{Ringgit},{EffectiveRate},{Amount}");
            }
        }

        [Test]
        public void ReadingBehaviour()
        {
            var transfer = new MoneyTransferReader(FileName).Read().Single();

            transfer.Date.Should().Be(Date);
            transfer.FromCurrency.Should().Be(Euro);
            transfer.ToCurrency.Should().Be(Ringgit);
            transfer.EffectiveRate.Should().Be(EffectiveRate);
            transfer.Amount.Should().Be(Amount);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            File.Delete(FileName);
        }
    }
}
