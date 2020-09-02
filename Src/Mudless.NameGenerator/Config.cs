using System.Collections.Generic;

namespace Mudless.NameGenerator
{
    public class Config
    {
        public int MinLength { get; set; } = 3;
        public int MaxLength { get; set; } = 8;
        public List<string> Patterns { get; set; } = new List<string>();
        public Dictionary<string, string> Parts { get; set; } = new Dictionary<string, string>();

        public static Config Default() => new Config
        {
            Patterns = new List<string>
            {
                PatternsDefinitions.MIDDLE_EARTH,
                PatternsDefinitions.JAPANESE_CONSTRAINED,
                PatternsDefinitions.JAPANESE_DIVERSE,
                PatternsDefinitions.CHINESE,
                PatternsDefinitions.GREEK,
                PatternsDefinitions.SLAVIC,
                PatternsDefinitions.HAWAIIAN_1,
                PatternsDefinitions.HAWAIIAN_2,
                PatternsDefinitions.OLD_LATIN_PLACE,
                PatternsDefinitions.FANTASY_VOWELS_R,
                PatternsDefinitions.FANTASY_S_A,
                PatternsDefinitions.FANTASY_H_L,
                PatternsDefinitions.FANTASY_N_L,
                PatternsDefinitions.FANTASY_K_N,
                PatternsDefinitions.FANTASY_J_G_Z,
                PatternsDefinitions.FANTASY_K_J_Y,
                PatternsDefinitions.FANTASY_S_E,
            },
            Parts = new Dictionary<string, string>
            {
                { "s",  ReplacementGroupsDefinitions.SYLLABLE },
                { "c",  ReplacementGroupsDefinitions.CONSONANT },
                { "C",  ReplacementGroupsDefinitions.CONSONANT_OR_COMBINATION },
                { "B",  ReplacementGroupsDefinitions.CONSONANT_OR_COMBINATION_FOR_BEGINNING },
                { "v",  ReplacementGroupsDefinitions.VOWEL },
                { "V",  ReplacementGroupsDefinitions.VOWEL_OR_COMBINATION },
            }
        };
    }
}