using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    /// <summary>
    /// 
    /// </summary>
    public enum SelectionState
    {
        /// <summary> </summary>
        User,
        /// <summary> </summary>
        Skill,
        /// <summary> </summary>
        Target
    }

    /// <summary>
    /// 
    /// </summary>
    public enum KnockoutCondition
    {
        /// <summary> Character is downed when life is depleted. </summary>
        Life,
        /// <summary> Character is downed when stamina is depleted. </summary>
        Stamina,
        /// <summary> Character is downed when either stamina or life are depleted. </summary>
        Either,
        /// <summary> Character is downed when both stamina or life are depleted. </summary>
        Both
    }

    /// <summary>
    /// The defense status of a character.
    /// </summary>
    public enum CharacterStatus
    {
        /// <summary> The character has any status. Made for effects </summary>
        Any,
        /// <summary> The character is inactive. </summary>
        Down,
        /// <summary> The character is shocked. </summary>
        Shocked,
        /// <summary> The character receives standard damage from attacks. </summary>
        Open,
        /// <summary> The character receives reduced damage from attacks. </summary>
        Guarding,
        /// <summary> The character receives no damage from attacks. </summary>
        Dodging,
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
        /// <summary> Skill that only affects a target once per cast. </summary>
        Special
    };

    /// <summary>
    /// The types of characters a skill can target.
    /// </summary>
    public enum TargetingType
    {
        /// <summary> Can target its user. </summary>
        Self = 1,
        /// <summary> Can target allies. </summary>
        Ally = 2,
        /// <summary> Can target enemies. </summary>
        Enemy = 4
    };

    /// <summary>
    /// Types of resources used to cast skills.
    /// </summary>
    public enum ResourceType
    {
        None = 0,
        Life = 1,
        Stamina = 2,
        TensionOptional = 4,
        TensionRequired = 8
    }

    /// <summary>
    /// Types of Effect Activations.
    /// </summary>
    public enum EffectActivationType
    {
        /// <summary> Effect activates upon being injected in the host. </summary>
        OnSelfInjection,              
        /// <summary> Effect activates upon the host being injected a different effect. </summary>
        OnOtherInjection,
        /// <summary> Effect activates upon the host prepping a skill. </summary>
        OnHostReactionPrepping,
        /// <summary> Effect activates upon the host casting a skill. </summary>
        OnHostReactionCasting
    };

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
        CurrentTension,
        /// <summary> </summary>
        MaxTension,
        /// <summary> </summary>
        Regen,
        /// <summary> </summary>
        Status,
        /// <summary> </summary>
        Power,
        /// <summary> </summary>
        Speed,
        /// <summary> </summary>
        Endurance
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
