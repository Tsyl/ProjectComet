using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    class Effect
    {
        public Texture2D icon { get; set; }
        public string name { get; set; }
        public Stat targetStat { get; set; }
        public EffectActivationType type { get; set; }
        public float value { get; set; }
        public float duration { get; set; }
        public float delay { get; set; }

        public Effect()
        {
            name = "TESTEFFECTINSTANT";
            targetStat = Stat.Health;
            type = EffectActivationType.Instant;
            value = -10;
        }

        public Effect(string _name, Stat _targetStat, EffectActivationType _type, float _value)
        {
            name = _name;
            targetStat = _targetStat;
            type = _type;
            value = _value;
        }

        private Effect(Effect _effect)
        {
            icon = _effect.icon;
            name = _effect.name;
            targetStat = _effect.targetStat;
            type = _effect.type;
            value = _effect.value;
            duration = _effect.duration;
            delay = _effect.delay;
    }

        public void Activate(Character chr)
        {
            switch (targetStat)
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

        public Effect Copy()
        {
            return new Effect(this);
        }
    }
}
