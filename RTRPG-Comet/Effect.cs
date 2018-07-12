using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    class Effect
    {
        public Texture2D icon { get; set; }
        public string name { get; set; }
        public Character host { get; set; }
        public Stat targetStat { get; set; }
        public EffectActivationType activationType { get; set; }
        public float value { get; set; }
        public float delay { get; set; }
        public float standbyDuration { get; set; }
        public float triggerDuration { get; set; }
        public int triggerAmount { get; set; }
        public float tickRate { get; set; }
        public bool isPersistent { get; set; }
        public bool isMultiafflict { get; set; }

        float defaultDuration;
        bool active = false;

        public Effect()
        {
            name = "TESTEFFECTINSTANT";
            targetStat = Stat.Health;
            activationType = EffectActivationType.OnSelfInjection;
            isPersistent = true;
            value = -5;
        }

        public Effect(  string _name, Stat _targetStat, EffectActivationType _activationType, 
                        float _value, float _standbyDuration, float _triggerDuration, int _triggerAmount,
                        float _delay, bool _isPersistent)
        {
            name = _name;
            targetStat = _targetStat;
            activationType = _activationType;
            value = _value;
            standbyDuration = _standbyDuration;
            triggerDuration = _triggerDuration;
            defaultDuration = triggerDuration;
            triggerAmount = _triggerAmount;
            delay = _delay;
            isPersistent = _isPersistent;
            isMultiafflict = triggerDuration > 0 && tickRate > 0;

            if(!isMultiafflict && triggerDuration > 0)
            {
                tickRate = triggerDuration;
            }
        }

        private Effect(Effect _effect)
        {
            icon = _effect.icon;
            name = _effect.name;
            targetStat = _effect.targetStat;
            activationType = _effect.activationType;
            value = _effect.value;
            triggerDuration = _effect.triggerDuration;
            defaultDuration = triggerDuration;
            tickRate = _effect.tickRate;
            // Set tick rate to one if some idiot wants to make an effect with a duration 
            // persistent and with a tick rate of zero for the heck of it.
            if(triggerDuration > 0 && tickRate <= 0 && isPersistent)
            {
                tickRate = 1;
            }
            delay = _effect.delay;
            isPersistent = _effect.isPersistent;
            isMultiafflict = _effect.isMultiafflict;
        }

        public void Update(GameTime gameTime)
        {
            float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (delay <= 0 && active)
            {
                // Effect is now active

                // Instant (duration == 0)
                if (triggerDuration <= 0 && isPersistent)
                {
                    // Effect happens.
                    Afflict();
                }
                
                // Over-time (duration > 0)
                if (triggerDuration > 0)
                {
                    if (Math.Round(triggerDuration, 2) % tickRate == 0)
                        // Effect happens.
                        Afflict();

                    triggerDuration -= seconds;
                }

                if(triggerDuration <= 0)
                {
                    if (!isPersistent)
                        // Effect is reversed.
                        Afflict(true);

                    host = null;
                }
            }
            else
            {
                // Effect is not active. Countdown delay by seconds.
                delay -= seconds;
                if(activationType == EffectActivationType.OnSelfInjection)
                {
                    active = true;
                }
                if(activationType == EffectActivationType.OnOtherInjection)
                {

                }
                if (activationType == EffectActivationType.OnHostReactionPrepping)
                {
                    if (host.GetCurrentSkill() != null)
                        active = true;
                }
                if (activationType == EffectActivationType.OnHostReactionCasting)
                {
                    if (host.GetCurrentSkill() != null && host.GetCurrentSkill().castTime <= 0)
                        active = true;
                }
            }
        }

        public void Afflict(bool isReverse = false)
        {
            if (isReverse)
                value *= (defaultDuration/tickRate)*-1;

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
