using System;
using System.Text;

namespace Mudless.NameGenerator.Patterns
{
    public class LiteralNamePatternElement : BaseNamePatternElement
    {
        public string Value { get; set; }

        public LiteralNamePatternElement(string value)
        {
            Value = value;
        }

        public override string GetValue(Random random)
        {
            return Value;
        }

        public override void DescribeInternal(StringBuilder stringBuilder, int indent)
        {
            var prefix = new string(' ', indent * 4);
            stringBuilder.Append(prefix);
            if (string.IsNullOrWhiteSpace(Value))
            {
                stringBuilder.Append("<EMPTY>");
            }
            else
            {
                stringBuilder.Append(Value);
            }
            stringBuilder.AppendLine(",");
        }
    }
}
