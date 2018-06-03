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
    public sealed class MaybankForexReaderTests
    {
        private const string FilePath = "Files\\Maybank.html";

        [Test]
        public void CorrectParsing()
        {
            var file = Path.Combine(PathHelper.GetAssemblyPath(), FilePath);

            using (var stream = File.OpenRead(file))
            {
                var rate = new MaybankForexReader().ReadAll(stream).Single();

                rate.Date.Should().Be(DateTime.Today);
                rate.FromCurrency.Should().Be("MYR");
                rate.ToCurrency.Should().Be("EUR");
                rate.Rate.Should().Be(4.714M);
            }
        }
    }
}
