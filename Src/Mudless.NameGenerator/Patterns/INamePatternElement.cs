using System;
using System.Text;

namespace Mudless.NameGenerator.Patterns
{
    public interface INamePatternElement
    {
        string GetValue(Random random);

        string Describe();

        void DescribeInternal(StringBuilder stringBuilder, int indent);
    }
}