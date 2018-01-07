namespace Faez.BudgetAssistant.Transferwise.UnitTests.DataLayer
{
    using System.IO;
    using AutoFixture;
    using FluentAssertions;
    using Models;
    using NUnit.Framework;
    using Transferwise.DataLayer;

    [TestFixture]
    public sealed class MoneyTransferWriterTests
    {
        private const int LineCount = 4;

        private static readonly IFixture Fixture = new Fixture();
        private static readonly string FileName = Path.GetTempFileName();

        [Test]
        public void WritingBehaviour()
        {
            new MoneyTransferWriter(FileName).Write(Fixture.CreateMany<MoneyTransfer>(LineCount - 1));

            File.ReadAllLines(FileName).Length.Should().Be(LineCount);
        }

        [TearDown]
        public void Teardown()
        {
            File.Delete(FileName);
        }
    }
}
