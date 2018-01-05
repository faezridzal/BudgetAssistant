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

            nullObject.ShouldThrow<ArgumentNullException>();

            Action emptyString = () => ArgumentExtensions.EnsureArgumentNotNullOrWhitespace(string.Empty, "argumentName");
            Action whitespaceString = () => ArgumentExtensions.EnsureArgumentNotNullOrWhitespace(" ", "argumentName");

            emptyString.ShouldThrow<ArgumentNullException>();
            whitespaceString.ShouldThrow<ArgumentNullException>();
        }
    }
}
