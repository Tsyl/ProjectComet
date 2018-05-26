using Microsoft.Xna.Framework.Graphics;

namespace Comet
{

    /// <summary>
    /// Character Stats
    /// </summary>
    public enum Stat
    {
        /// <summary> </summary>
        Health,
        /// <summary> </summary>
        CurrentLife,
        /// <summary> </summary>
        MaxLife,
        /// <summary> </summary>
        CurrentStamina,
        /// <summary> </summary>
        MaxStamina,
        /// <summary> </summary>
        Regen,
        /// <summary> </summary>
        Power,
        /// <summary> </summary>
        Speed,
        /// <summary> </summary>
        Endurance
    };

    /// <summary>
    /// The defense status of a character.
    /// </summary>
    public enum CharacterStatus
    {
        /// <summary> The character receives standard damage from attacks. </summary>
        Open,
        /// <summary> The character receives reduced damage from attacks. </summary>
        Guarding,
        /// <summary> The character receives no damage from attacks. </summary>
        Dodging
    }

    /// <summary>
    /// Types of Skills
    /// </summary>
    public enum SkillType
    {
        /// <summary> Skill is always active. </summary>
        Passive,
        /// <summary> Skill is casted repeatedly until character is given another command or
        /// either the user or targets are unable to fight. </summary>
        Standard,
        /// <summary> Standard skill that happens continually after its cast. </summary>
        Steady,
        /// <summary> Skill that only affects a target once per cast. </summary>
        Special
    };

    /// <summary>
    /// Types of Effect Activations.
    /// </summary>
    public enum EffectActivationType
    {
        /// <summary> Effect happens on application. </summary>
        Instant,
        /// <summary> Effect happens after its delay. </summary>
        Delayed,
        /// <summary> Effect happens after a character does something. </summary>
        SenderReactive,
        /// <summary> Effect happens after a character is hit by something. </summary>
        TargetReactive
    };


    class Skill
    {
        Texture2D icon;
        public string name { get; set; }
        public Character user { get; set; }
        public Character target { get; set; }
        public SkillType type { get; set; }
        public Effect[] effects { get; set; }
        public float castTime { get; set; }
        public float remainingCastTime { get; set; }

        public Skill()
        {
            name = "TESTSTANDARD";
            type = SkillType.Standard;
            castTime = 1;
            effects = new Effect[1];
            effects[0] = new Effect();
        }

        public void Cast()
        {
            foreach(Effect efct in effects)
            {
                if (efct == null)
                    break;

                efct.Tune(user.power);
                target.InflictEffect(effects);
            }
        }
    }

    class Effect
    {
        Texture2D icon;
        public string name { get; set; }
        public Stat targetStat { get; set; }
        public EffectActivationType type { get; set; }
        public float value { get; set; }
        public float duration { get; set; }
        public float delay { get; set; }
        float currentDuration { get; set; }

        public Effect()
        {
            name = "TESTEFFECTINSTANT";
            targetStat = Stat.Health;
            type = EffectActivationType.Instant;
            value = -10;
        }

        public void Activate(Character chr)
        {
            switch(targetStat)
            {
                case Stat.CurrentLife:
                    chr.currentLife += value;
                    break;
                case Stat.CurrentStamina:
                    chr.currentStamina += value;
                    break;
                case Stat.Health:
                    chr.currentStamina += value;
                    if (chr.currentStamina < 0)
                    {
                        float diff = chr.currentStamina;
                        chr.currentStamina = 0;
                        chr.currentLife += diff;
                    }
                    break;
                case Stat.MaxLife:
                    chr.maxLife += value;
                    break;
                case Stat.MaxStamina:
                    chr.maxStamina += value;
                    break;
                case Stat.Power:
                    chr.power += value;
                    break;
                case Stat.Speed:
                    chr.speed += value;
                    break;
                case Stat.Endurance:
                    chr.endurance += value;
                    break;
            }
        }

        public void Tune(float power)
        {
            value *= power / 100;
        }
    }

    class EffectContainer
    {
        Effect[] instant;
        Effect[] delayed;
        Effect[] sender_reactive;
        Effect[] target_reactive;

        void Update()
        {

        }
    }

    class Structures
    {
    }
}
