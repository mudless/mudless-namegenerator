using System;

namespace Mudless.NameGenerator.Demo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length == 0 || !int.TryParse(args[0], out var count))
            {
                count = 1;
            }
            var generator = new NameGenerator();

            for (var i = 0; i < count; i++)
            {
                var name = generator.Generate();
                Console.Out.WriteLine(name);
            }
        }
    }
}
