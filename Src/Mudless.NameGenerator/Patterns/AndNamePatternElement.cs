using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mudless.NameGenerator.Patterns
{
    public class AndNamePatternElement : BaseNamePatternElement
    {
        public IList<INamePatternElement> Elements { get; set; }

        public AndNamePatternElement(params INamePatternElement[] elements)
        {
            Elements = elements.ToList();
        }

        public AndNamePatternElement(IList<INamePatternElement> elements)
        {
            Elements = elements;
        }

        public override string GetValue(Random random)
        {
            return string.Join("", Elements.Select(m => m.GetValue(random)));
        }

        public override void DescribeInternal(StringBuilder stringBuilder, int indent)
        {
            var prefix = new string(' ', indent * 4);
            stringBuilder.Append(prefix);
            stringBuilder.AppendLine("AND");
            stringBuilder.Append(prefix);
            stringBuilder.AppendLine("{");
            foreach (var element in Elements)
            {
                element.DescribeInternal(stringBuilder, indent + 1);
            }
            stringBuilder.Append(prefix);
            stringBuilder.AppendLine("}");
        }
    }
}