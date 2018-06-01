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
