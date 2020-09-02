using Mudless.NameGenerator.Patterns;
using Mudless.NameGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mudless.NameGenerator
{
    public class NameGenerator
    {
        private const int MaxAttempts = 1000;

        private static Regex _normalizerExpression = new Regex(@"([a-z])\1\1");
        private readonly Random _random;
        private readonly List<INamePatternElement> _patterns;
        private readonly Config _config;

        public NameGenerator(Config config = null, Random random = null)
        {
            _config = config ?? Config.Default();
            _random = random ?? new Random();

            var parser = new NamePatternParser(_config);
            _patterns = _config.Patterns.Select(m => parser.Parse(m)).ToList();

            if (!_patterns.Any()) throw new ArgumentException("Empty list of patterns");
        }

        public string Generate()
        {
            for (var i = 0; i < MaxAttempts; i++)
            {
                var pattern = _patterns.TakeRandom(_random);

                var name = pattern.GetValue(_random);

                name = name.ToLowerInvariant();
                _normalizerExpression.Replace(name, m => m.Groups[0].Value.Substring(0, 2));

                if (name.Length < _config.MinLength)
                {
                    continue;
                }
                if (name.Length > _config.MaxLength)
                {
                    continue;
                }

                return name.UpperFirst();
            }

            return null;
        }
    }
}
