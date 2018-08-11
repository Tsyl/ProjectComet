using Newtonsoft.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Comet
{
    /// <summary>
    /// 
    /// </summary>
    class Effect
    {
        public string name { get; set; }
        public Texture2D icon { get; set; }
        public SoundEffect sfx { get; set; }
        public string description { get; set; }
        public Stat targetStat { get; set; }
        public CharacterStatus hostStatusRequirement { get; set; }
        public EffectActivationType activationType { get; set; }
        public float value { get; set; }
        public float delay { get; set; }
        public float standbyDuration { get; set; }
        public float triggerDuration { get; set; }
        public int triggerAmount { get; set; }
        public float tickRate { get; set; }
        public bool isPersistent { get; set; }

        public Character host { get; set; }
        public float currentValue { get; set; }
        public float currentDelay { get; set; }
        public float currentStandbyDuration { get; set; }
        public float currentTriggerDuration { get; set; }
        public float currentTickRate { get; set; }
        public float currentTriggerAmount { get; set; }
        bool statusRequirementMet = false;
        bool activationRequirementMet = false;
        bool isActive = false;
        bool isOver = false;

        public string iconName { get; set; }
        public string sfxName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_icon"></param>
        /// <param name="_sfx"></param>
        /// <param name="_description"></param>
        /// <param name="_targetStat">The stat this effect is modifying.</param>
        /// <param name="_hostStatusRequirement">The status requirement of the host.</param>
        /// <param name="_activationType">The activation requirement for the effect.</param>
        /// <param name="_value">The value that will be added to the target stat.</param>
        /// <param name="_standbyDuration">The duration in which the effect will wait for its requirements to be met.</param>
        /// <param name="_triggerDuration">The duration for which the effect will be in place.</param>
        /// <param name="_triggerAmount">The number of times the value is added to its host during trigger duration.</param>
        /// <param name="_delay">The duration before the ability enters standby.</param>
        /// <param name="_isPersistent">Whether the effect persists after its trigger duration is over.</param>
        public Effect(string _name, Texture2D _icon, SoundEffect _sfx, string _description,
                        Stat _targetStat, CharacterStatus _hostStatusRequirement, EffectActivationType _activationType,
                        float _value, float _standbyDuration, float _triggerDuration, int _triggerAmount,
                        float _delay, bool _isPersistent)
        {
            name = _name;
            icon = _icon;
            sfx = _sfx;
            description = _description;
            targetStat = _targetStat;
            hostStatusRequirement = _hostStatusRequirement;
            activationType = _activationType;
            value = _value;
            standbyDuration = _standbyDuration;
            triggerDuration = _triggerDuration;
            triggerAmount = _triggerAmount;
            delay = _delay;
            isPersistent = _isPersistent;

            Initialize();
        }

        [JsonConstructor]
        public Effect(string name, string iconName, string sfxName, string description,
                        Stat targetStat, CharacterStatus hostStatusRequirement, EffectActivationType activationType,
                        float value, float standbyDuration, float triggerDuration, int triggerAmount,
                        float delay, bool isPersistent)
        {
            this.name = name;
            this.iconName = iconName;
            this.sfxName = sfxName;
            this.description = description;
            this.targetStat = targetStat;
            this.hostStatusRequirement = hostStatusRequirement;
            this.activationType = activationType;
            this.value = value;
            this.standbyDuration = standbyDuration;
            this.triggerDuration = triggerDuration;
            this.triggerAmount = triggerAmount;
            this.delay = delay;
            this.isPersistent = isPersistent;
        }

        private Effect(Effect _effect)
        {
            name = _effect.name;
            icon = _effect.icon;
            sfx = _effect.sfx;
            description = _effect.description;
            targetStat = _effect.targetStat;
            hostStatusRequirement = _effect.hostStatusRequirement;
            activationType = _effect.activationType;
            value = _effect.value;
            standbyDuration = _effect.standbyDuration;
            triggerDuration = _effect.triggerDuration;
            triggerAmount = _effect.triggerAmount;
            delay = _effect.delay;
            isPersistent = _effect.isPersistent;

            Initialize();
        }

        public void Initialize()
        {
            // Ability needs at least 1 trigger to function.
            if (triggerAmount < 1)
                triggerAmount = 1;

            tickRate = triggerDuration / triggerAmount;
            
            currentValue = value;
            currentDelay = delay;
            currentStandbyDuration = standbyDuration;
            currentTriggerDuration = triggerDuration;
            currentTriggerAmount = triggerAmount;
        }

        public void Update(GameTime gameTime)
        {
            float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (!isOver)
            {
                // Delay Phase
                if (currentDelay > 0)
                {
                    // Countdown
                    currentDelay -= seconds;
                }
                // Standby Phase
                else if (!isActive)
                {
                    // Standby duration runs out and still not active? Effect is over.
                    if (currentStandbyDuration <= 0)
                    {
                        isOver = true;
                    }
                    else
                    {
                        // Countdown
                        currentStandbyDuration -= seconds;

                        // Status Requirement
                        // If requirement is any, just set to true.
                        statusRequirementMet = hostStatusRequirement == CharacterStatus.Any ?
                                                true : host.status == hostStatusRequirement;

                        // Activation Requirement
                        switch (activationType)
                        {
                            case EffectActivationType.OnSelfInjection:
                                activationRequirementMet = true;
                                break;
                            case EffectActivationType.OnOtherInjection:
                                // TODO
                                break;
                            case EffectActivationType.OnHostReactionPrepping:
                                if (host != null)
                                    activationRequirementMet = host.GetCurrentSkill() != null;
                                break;
                            case EffectActivationType.OnHostReactionCasting:
                                if (host != null)
                                    activationRequirementMet = host.GetCurrentSkill() != null && host.GetCurrentSkill().castTime <= 0;
                                break;
                        }

                        // Activation
                        isActive = statusRequirementMet && activationRequirementMet;
                    }
                    
                }
                // Trigger Phase
                else
                {
                    // Afflict immediately if its the starting trigger.
                    if (currentTriggerAmount == triggerAmount)
                    {
                        Trigger();
                    }
                    // If not, check if there are triggers left.
                    else if (currentTriggerAmount > 0)
                    {
                        if (currentTickRate > 0)
                        {
                            currentTickRate -= seconds;
                        }
                        // Call Trigger after tickRate is up.
                        else
                        {
                            Trigger();
                        }
                    }
                    // If not, check if the duration of the trigger phase is 0 to end the effect.
                    else if (currentTriggerDuration <= 0)
                    {
                        isOver = true;
                        if (!isPersistent)
                            Restore();
                    }
                }
            } // !isOver
        } // Update

        /// <summary>
        /// Affects the host, substarcts one from the trigger amount and resets the tick rate if
        /// the former is higher than 0.
        /// </summary>
        public void Trigger()
        {
            switch (targetStat)
            {
                case Stat.Health:
                    host.currentStamina += value;
                    if (host.currentStamina < 0)
                    {
                        float diff = host.currentStamina;
                        host.currentStamina = 0;
                        host.currentLife += diff;
                    }
                    break;
                case Stat.CurrentLife:
                    host.currentLife += value;
                    break;
                case Stat.MaxLife:
                    host.maxLife += value;
                    break;
                case Stat.CurrentStamina:
                    host.currentStamina += value;
                    break;
                case Stat.MaxStamina:
                    host.maxStamina += value;
                    break;
                case Stat.CurrentTension:
                    host.currentTension += value;
                    break;
                case Stat.MaxTension:
                    host.maxTension += value;
                    break;
                case Stat.Regen:
                    host.regen += value;
                    break;
                case Stat.Status:
                    // Set "Any" to "Open"
                    if (value == 0)
                        value = 3;
                    host.status = (CharacterStatus)value;
                    break;
                case Stat.Power:
                    host.power += value;
                    break;
                case Stat.Speed:
                    host.speed += value;
                    break;
                case Stat.Endurance:
                    host.endurance += value;
                    break;
            }

            // Substract one from the amount of triggers.
            currentTriggerAmount--;
            // Check if ability still has triggers left.
            if (currentTriggerAmount > 0)
                currentTickRate = tickRate;
            else
                currentTickRate = 0;
        }

        public void Restore()
        {
            value *= -triggerAmount;

            switch (targetStat)
            {
                case Stat.CurrentLife:
                    host.currentLife += value;
                    break;
                case Stat.CurrentStamina:
                    host.currentStamina += value;
                    break;
                case Stat.Health:
                    host.currentStamina += value;
                    if (host.currentStamina < 0)
                    {
                        float diff = host.currentStamina;
                        host.currentStamina = 0;
                        host.currentLife += diff;
                    }
                    break;
                case Stat.MaxLife:
                    host.maxLife += value;
                    break;
                case Stat.MaxStamina:
                    host.maxStamina += value;
                    break;
                case Stat.Power:
                    host.power += value;
                    break;
                case Stat.Speed:
                    host.speed += value;
                    break;
                case Stat.Endurance:
                    host.endurance += value;
                    break;
            }

            currentTriggerAmount--;
        }

        public void Tune(float power)
        {
            value *= power / 100;
        }

        public void Tune(float power, float speed)
        {
            value *= power / 100;
            delay *= speed / 100;
        }

        public Effect Copy()
        {
            return new Effect(this);
        }
    }
}
