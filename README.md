# Mudless.NameGenerator

Generator of random, fantasy-like names for .NET.

## Usage

Generating one name:

```cs
var nameGenerator = new NameGenerator();
var name = nameGenerator.Generate();
```

Generating multiple names at once:

```cs
var nameGenerator = new NameGenerator();
var names = nameGenerator.GenerateMany().Take(100).ToList();
```

## Installation

You can obtain the latest version using NuGet Package Manager or from [NuGet.org](https://www.nuget.org/packages/Mudless.NameGenerator)

