using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    class ContentContainer
    {
        SpriteBatch spriteBatch;
        Party[] parties;
        Character[] characters;

        public ContentContainer()
        {
            parties = new Party[2];
            characters = new Character[6];
            Skill[] testSkills = new Skill[6];

            testSkills[0] = new Skill("Basic Attack",
                                   "N/A",
                                   "N/A",
                                   "N/A",
                                   SkillType.Standard,
                                   TargetingType.Enemy,
                                   2,
                                   0,
                                   0,
                                   new Effect("Attack", "N/A", "N/A", "N/A", Stat.Health, CharacterStatus.Any, EffectActivationType.OnSelfInjection, -5, 0, 0, 0, 0, true));

            testSkills[1] = new Skill("Poison",
                                   "N/A",
                                   "N/A",
                                   "N/A",
                                   SkillType.Special,
                                   TargetingType.Enemy,
                                   3,
                                   0,
                                   0,
                                   new Effect("Poison",
                                   "N/A",
                                   "N/A",
                                   "N/A", Stat.CurrentLife, CharacterStatus.Any, EffectActivationType.OnSelfInjection, -5, 0, 6, 2, 0, true));

            testSkills[2] = new Skill("Debilitating Strike",
                                   "N/A",
                                   "N/A",
                                   "N/A",
                                   SkillType.Special,
                                   TargetingType.Enemy,
                                   4,
                                   0,
                                   0,
                                   new Effect("Debilitate",
                                   "N/A",
                                   "N/A",
                                   "N/A", Stat.CurrentStamina, CharacterStatus.Any, EffectActivationType.OnSelfInjection, -20, 0, 6, 0, 0, false));

            testSkills[3] = new Skill("Debilitating Flask",
                                   "N/A",
                                   "N/A",
                                   "N/A",
                                   SkillType.Special,
                                   TargetingType.Enemy,
                                   5,
                                   0,
                                   0,
                                   new Effect("Poison",
                                   "N/A",
                                   "N/A",
                                   "N/A", Stat.CurrentStamina, CharacterStatus.Any, EffectActivationType.OnSelfInjection, -15, 0, 6, 2, 0, false));

            testSkills[4] = new Skill("Caltrops",
                                   "N/A",
                                   "N/A",
                                   "N/A",
                                   SkillType.Special,
                                   TargetingType.Enemy,
                                   3,
                                   0,
                                   0,
                                   new Effect("Caltrops",
                                   "N/A",
                                   "N/A",
                                   "N/A", Stat.Health, CharacterStatus.Any, EffectActivationType.OnHostReactionPrepping, -30, 5, 5, 0, 0, false));

            testSkills[5] = new Skill("Sabotage",
                                   "N/A",
                                   "N/A",
                                   "N/A",
                                   SkillType.Special,
                                   TargetingType.Enemy,
                                   3,
                                   0,
                                   0,
                                   new Effect("Sabotage",
                                   "N/A",
                                   "N/A",
                                   "N/A", Stat.Health, CharacterStatus.Any, EffectActivationType.OnHostReactionCasting, -50, 3, 3, 0, 0, false));

            characters[0] = new Character("Rafael",
                                          150,
                                          250,
                                          0.5f,
                                          100,
                                          KnockoutCondition.Stamina,
                                          100,
                                          100,
                                          0,
                                          testSkills[0],
                                          testSkills[2],
                                          testSkills[5],
                                          testSkills[1]);
            characters[1] = new Character("Eliza",
                                          150,
                                          150,
                                          0.5f,
                                          100,
                                          KnockoutCondition.Stamina,
                                          100,
                                          100,
                                          0,
                                          testSkills[1],
                                          testSkills[2],
                                          testSkills[3],
                                          testSkills[4]);
            characters[2] = new Character("Axel",
                                          200,
                                          200,
                                          0.5f,
                                          100,
                                          KnockoutCondition.Stamina,
                                          100,
                                          100,
                                          0,
                                          testSkills[0],
                                          testSkills[1],
                                          testSkills[3],
                                          testSkills[5]);
            characters[3] = new Character("Mika",
                                          150,
                                          100,
                                          0.5f,
                                          100,
                                          KnockoutCondition.Stamina,
                                          100,
                                          100,
                                          0,
                                          testSkills[3],
                                          testSkills[5],
                                          testSkills[4],
                                          testSkills[2]);
            characters[4] = new Character("Zayden",
                                          150,
                                          250,
                                          0.5f,
                                          100,
                                          KnockoutCondition.Stamina,
                                          100,
                                          100,
                                          0,
                                          testSkills[5],
                                          testSkills[4],
                                          testSkills[3],
                                          testSkills[2]);
            characters[5] = new Character("Gunhilde",
                                          150,
                                          130,
                                          0.5f,
                                          100,
                                          KnockoutCondition.Stamina,
                                          100,
                                          100,
                                          0,
                                          testSkills[1],
                                          testSkills[3],
                                          testSkills[4],
                                          testSkills[5]);

            parties[0] = new Party("Red Team",
                                    characters[0].Copy(),
                                    characters[2].Copy(),
                                    characters[4].Copy());
            parties[1] = new Party("Blue Team",
                                    characters[1].Copy(),
                                    characters[3].Copy(),
                                    characters[5].Copy());
        }

        public ContentContainer(Character[] _characters)
        {
            parties = new Party[2];
            parties[0] = new Party("Red Team");
            parties[1] = new Party("Blue Team");
            characters = _characters;

            int charNum = 0;
            for (int pNum = 0; pNum < parties[0].characters.Length; pNum++)
            {
                parties[0].characters[pNum] = characters[charNum].Copy();

                charNum++;
                if (charNum == characters.Length)
                    charNum = 0;
            }
            for (int pNum = 0; pNum < parties[1].characters.Length; pNum++)
            {
                parties[1].characters[pNum] = characters[charNum];

                charNum++;
                if (charNum == characters.Length)
                    charNum = 0;
            }
        }
        
        // Loader Methods
        // These give out content loaded from a file.

        // Default Methods
        // These give out pre-made content for testing.
        public Party GetParty(int partyNum)
        {
            return parties[partyNum];
        }
    }
}
