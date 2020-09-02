using FluentAssertions;
using Mudless.NameGenerator.Patterns;
using NUnit.Framework;
using System;

namespace Mudless.NameGenerator.Tests.Patterns
{
    [TestFixture]
    public class NamePatternParserTest
    {
        private NamePatternParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new NamePatternParser();
        }

        [Test]
        public void GivenNullOrEmptyPattern_WhenParsing_ShouldThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => parser.Parse(""));
            Assert.Throws<ArgumentNullException>(() => parser.Parse(null));
        }

        [Test]
        public void GivenPatternWithInvalidBrackets_WhenParsing_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => parser.Parse("((aa)"));
            Assert.Throws<ArgumentException>(() => parser.Parse("(aa))"));
            Assert.Throws<ArgumentException>(() => parser.Parse("<<a>"));
            Assert.Throws<ArgumentException>(() => parser.Parse("<a>>"));
        }

        [Test]
        public void ShouldParseLiteral()
        {
            var result = parser.Parse("aaa");
            result.Should().BeEquivalentTo(
                new LiteralNamePatternElement("aaa")
            );
        }

        [Test]
        public void ShouldParseAndElement()
        {
            var result = parser.Parse("(aaa)(bbb)");
            result.Should().BeEquivalentTo(
                new AndNamePatternElement(
                    new LiteralNamePatternElement("aaa"),
                    new LiteralNamePatternElement("bbb")
                )
            );
        }

        [Test]
        public void ShouldParseOrElement()
        {
            var result = parser.Parse("aaa|bbb");
            result.Should().BeEquivalentTo(
                new OrNamePatternElement(
                    new LiteralNamePatternElement("aaa"),
                    new LiteralNamePatternElement("bbb")
                )
            );
        }

        [Test]
        public void ShouldParseAndOrElement()
        {
            var result = parser.Parse("(aaa)(bb|ccb)");
            result.Should().BeEquivalentTo(
                new AndNamePatternElement(
                    new LiteralNamePatternElement("aaa"),
                    new OrNamePatternElement(
                        new LiteralNamePatternElement("bbb"),
                        new LiteralNamePatternElement("bbb")
                     )
                )
            );
        }

        [Test]
        public void ShouldParseAndOrElement2()
        {
            var result = parser.Parse("sv(nia|lia)");
            result.Should().BeEquivalentTo(
                new AndNamePatternElement(
                    new LiteralNamePatternElement("sv"),
                    new OrNamePatternElement(
                        new LiteralNamePatternElement("nia"),
                        new LiteralNamePatternElement("lia")
                     )
                )
            );
        }

        [Test]
        public void ShouldParseAndOrElement3()
        {
            var result = parser.Parse("(|(a|b)(c|d)|-(e|f)(g|h)(|(i|j)(k|l)))");
            var expected = new OrNamePatternElement(
                    new LiteralNamePatternElement(""),
                    new AndNamePatternElement(
                        new OrNamePatternElement(
                            new LiteralNamePatternElement("a"),
                            new LiteralNamePatternElement("b")
                        ),
                        new OrNamePatternElement(
                            new LiteralNamePatternElement("c"),
                            new LiteralNamePatternElement("d")
                        )
                    ),
                    new AndNamePatternElement(
                        new LiteralNamePatternElement("-"),
                        new OrNamePatternElement(
                            new LiteralNamePatternElement("e"),
                            new LiteralNamePatternElement("f")
                        ),
                        new OrNamePatternElement(
                            new LiteralNamePatternElement("g"),
                            new LiteralNamePatternElement("h")
                        ),
                        new OrNamePatternElement(
                            new LiteralNamePatternElement(""),
                            new AndNamePatternElement(
                                new OrNamePatternElement(
                                    new LiteralNamePatternElement("i"),
                                    new LiteralNamePatternElement("j")
                                ),
                                new OrNamePatternElement(
                                    new LiteralNamePatternElement("k"),
                                    new LiteralNamePatternElement("l")
                                )
                            )
                        )
                    )
                );

            result.Should().BeEquivalentTo(
               expected
            );
        }
    }
}