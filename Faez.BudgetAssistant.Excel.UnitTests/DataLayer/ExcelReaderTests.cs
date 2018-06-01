namespace Faez.BudgetAssistant.Excel.UnitTests.DataLayer
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using BudgetAssistant.UnitTests.Helpers;
    using Excel.DataLayer;
    using FluentAssertions;
    using Models;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ExcelReaderTests
    {
        private const string FilePath = "Files\\Budget.xlsx";

        [Test]
        public void ReadingBehaviour()
        {
            var file = Path.Combine(PathHelper.GetAssemblyPath(), FilePath);
            var entries = new List<BudgetEntry>();

            using (var stream = File.OpenRead(file))
            {
                entries.AddRange(new ExcelReader().GetEntries(stream));
            }

            entries.Count.Should().Be(540);

            foreach (var entry in entries)
            {
                entry.Currency.Should().NotBeNull();
                entry.Category.Should().NotBeNull();
                entry.Amount.Should().BeGreaterOrEqualTo(0);
            }

            entries.All(e => e.IsCredit).Should().BeFalse();
        }
    }
}
