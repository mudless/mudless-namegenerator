using Mudless.NameGenerator.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mudless.NameGenerator.Patterns
{
    public class OrNamePatternElement : BaseNamePatternElement
    {
        public IList<INamePatternElement> Elements { get; set; }

        public OrNamePatternElement(params INamePatternElement[] elements)
        {
            Elements = elements.ToList();
        }

        public OrNamePatternElement(IList<INamePatternElement> element)
        {
            Elements = element;
        }

        public override string GetValue(Random random)
        {
            return Elements.TakeRandom(random).GetValue(random);
        }

        public override void DescribeInternal(StringBuilder stringBuilder, int indent)
        {
            var prefix = new string(' ', indent * 4);
            stringBuilder.Append(prefix);
            stringBuilder.AppendLine("OR");
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