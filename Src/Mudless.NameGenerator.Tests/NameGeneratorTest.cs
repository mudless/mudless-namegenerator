using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

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
        public void WhenGeneratingMany_ShouldRetunIterator()
        {
            var nameGenerator = new NameGenerator();

            var names = nameGenerator.GenerateMany().Take(100).ToList();

            names.Should().HaveCount(100);
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
