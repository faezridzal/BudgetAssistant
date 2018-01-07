namespace Faez.BudgetAssistant.Core.UnitTests.Extensions
{
    using System;
    using Core.Extensions;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public sealed class ArgumentExtensionsTests
    {
        [Test]
        public void ShouldThrowExceptions()
        {
            Action nullObject = () => ArgumentExtensions.EnsureArgumentNotNull<object>(null, "argumentName");

            nullObject.Should().Throw<ArgumentNullException>();

            Action emptyString = () => ArgumentExtensions.EnsureArgumentNotNullOrWhitespace(string.Empty, "argumentName");
            Action whitespaceString = () => ArgumentExtensions.EnsureArgumentNotNullOrWhitespace(" ", "argumentName");

            emptyString.Should().Throw<ArgumentNullException>();
            whitespaceString.Should().Throw<ArgumentNullException>();
        }
    }
}
