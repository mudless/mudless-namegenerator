using FluentAssertions;
using NUnit.Framework;
using System;

namespace Mudless.NameGenerator.Tests
{
    [TestFixture]
    public class NameGeneratorTest
    {
        [Test]
        public void SmokeTest()
        {
            var nameGenerator = new NameGenerator();

            for (var i = 0; i < 100000; i++)
            {
                var name = nameGenerator.Generate();
                name.Should().NotBeNullOrWhiteSpace();
            }
        }

        [Test]
        public void GivenEmptyListOfPatterns_WhenCreating_ShouldThrowException()
        {
            var config = Config.Default();
            config.Patterns.Clear();

            Assert.Throws<ArgumentException>(() => new NameGenerator(config));
        }
    }
}