using System;
using System.Text;

namespace Mudless.NameGenerator.Patterns
{
    public abstract class BaseNamePatternElement : INamePatternElement
    {
        public abstract string GetValue(Random random);

        public string Describe()
        {
            var stringBuiler = new StringBuilder();
            DescribeInternal(stringBuiler, 0);
            return stringBuiler.ToString();
        }

        public abstract void DescribeInternal(StringBuilder stringBuilder, int indent);
    }
}