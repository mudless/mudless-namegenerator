using System;
using System.Collections.Generic;
using System.Linq;

namespace Mudless.NameGenerator.Patterns
{
    public class NamePatternParser
    {
        public const char GroupStartToken = '(';
        public const char GroupEndToken = ')';
        public const char ReplacementGroupStartToken = '<';
        public const char ReplacementGroupEndToken = '>';
        public const char OrToken = '|';

        private readonly Config _config;

        public NamePatternParser(Config config = null)
        {
            _config = config ?? Config.Default();
        }

        public INamePatternElement Parse(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern)) throw new ArgumentNullException(nameof(pattern));

            pattern = pattern.Trim();

            var chars = pattern.ToCharArray();
            if (chars.Count(c => c == GroupStartToken) != chars.Count(c => c == GroupEndToken))
            {
                throw new ArgumentException("Unmatched bracktes in expresion");
            }

            if (chars.Count(c => c == ReplacementGroupStartToken) != chars.Count(c => c == ReplacementGroupEndToken))
            {
                throw new ArgumentException("Unmatched replacement group brackets");
            }

            pattern = GroupStartToken + pattern + GroupEndToken;

            return ParseInternal(pattern);
        }

        public INamePatternElement ParseInternal(string pattern)
        {
            if (pattern.IndexOf(GroupStartToken) == -1 && pattern.IndexOf(OrToken) == -1)
            {
                if (pattern.Length > 0 && pattern[0] == ReplacementGroupStartToken)
                {
                    var replacementKey = pattern.TrimStart(ReplacementGroupStartToken).TrimEnd(ReplacementGroupEndToken);

                    var patterns = string.Join("", replacementKey.ToCharArray().Select(m => _config.Parts[m.ToString()]).ToList());

                    return Parse(patterns);
                }
                return new LiteralNamePatternElement(pattern);
            }

            var groups = SplitAndGroups(pattern);

            var elements = new List<INamePatternElement>();
            foreach (var group in groups)
            {
                var options = SplitOrGroups(group);
                if (options.Count == 1)
                {
                    elements.Add(ParseInternal(group));
                }
                else
                {
                    var partElements = new List<INamePatternElement>();
                    foreach (var option in options)
                    {
                        partElements.Add(ParseInternal(option));
                    }
                    elements.Add(new OrNamePatternElement(partElements));
                }
            }

            if (elements.Count == 1)
            {
                return elements[0];
            }
            else
            {
                return new AndNamePatternElement(elements);
            }
        }

        private static List<string> SplitAndGroups(string pattern)
        {
            var groups = new List<string>();
            var last = 0;
            var parenthesis = new Stack<int>();

            for (var i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == GroupStartToken)
                {
                    parenthesis.Push(i);
                }
                else if (pattern[i] == GroupEndToken)
                {
                    last = parenthesis.Pop();
                    if (parenthesis.Count == 0)
                    {
                        if (groups.Count == 0 && last != 0)
                        {
                            var prefix = pattern.Substring(0, last);
                            groups.Add(prefix);
                        }

                        var group = pattern.Substring(last + 1, i - last - 1);

                        groups.Add(group);
                    }
                }
            }

            if (groups.Count == 0)
            {
                groups.Add(pattern);
            }
            return groups;
        }

        private static List<string> SplitOrGroups(string pattern)
        {
            var groups = new List<string>();
            var last = 0;
            var lastReturned = 0;
            var parenthesis = new Stack<int>();

            for (var i = 0; i < pattern.Length; i++)
            {
                if (parenthesis.Count == 0 && pattern[i] == OrToken)
                {
                    var orGroup = pattern.Substring(lastReturned, i - lastReturned);
                    groups.Add(orGroup);
                    lastReturned = i + 1;
                }
                else if (pattern[i] == GroupStartToken)
                {
                    parenthesis.Push(i);
                }
                else if (pattern[i] == GroupEndToken)
                {
                    last = parenthesis.Pop();
                }
            }
            if (lastReturned < pattern.Length)
            {
                var orGroup = pattern.Substring(lastReturned, pattern.Length - lastReturned);
                groups.Add(orGroup);
            }
            return groups;
        }
    }
}