namespace Faez.BudgetAssistant.Forex.UnitTests.DataLayer
{
    using System;
    using System.IO;
    using System.Linq;
    using BudgetAssistant.UnitTests.Helpers;
    using FluentAssertions;
    using Forex.DataLayer;
    using NUnit.Framework;

    [TestFixture]
    public sealed class FixerForexReaderTests
    {
        private const string FilePath = "Files\\Fixer.json";

        [Test]
        public void CorrectParsing()
        {
            var file = Path.Combine(PathHelper.GetAssemblyPath(), FilePath);
            var payload = File.ReadAllText(file);
            var rates = new FixerForexReader().ReadAll(payload).ToList();

            rates.Should().HaveCount(168);

            foreach (var rate in rates)
            {
                rate.Date.Should().Be(new DateTime(2018, 6, 1));
                rate.FromCurrency.Should().Be("EUR");
                rate.ToCurrency.Should().NotBeNullOrWhiteSpace().And.HaveLength(3);
                rate.Rate.Should().BePositive();
            }
        }
    }
}
