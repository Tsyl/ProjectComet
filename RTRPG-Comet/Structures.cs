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
        /// <summary> The character is inactive. </summary>
        Down,
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
        /// <summary> Effect activates upon being injected in the host. </summary>
        OnSelfInjection,              
        /// <summary> Effect activates upon the host being injected a different effect. </summary>
        OnOtherInjection,
        /// <summary> Effect activates upon the host prepping a skill. </summary>
        OnHostReactionPrepping,
        /// <summary> Effect activates upon the host casting a skill. </summary>
        OnHostReactionCasting
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
