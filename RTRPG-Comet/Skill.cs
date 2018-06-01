using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    class Skill
    {
        public Texture2D icon { get; set; }
        public string name { get; set; }
        public Character user { get; set; }
        public Character target { get; set; }
        public SkillType type { get; set; }
        public Effect[] effects { get; set; }
        public float castTime { get; set; }

        public Skill()
        {
            name = "Basic Attack";
            type = SkillType.Standard;
            castTime = 1;
            effects = new Effect[1];
            effects[0] = new Effect();
        }

        public Skill(string _name, SkillType _type, float _castTime, Effect[] _effects)
        {
            name = _name;
            type = _type;
            castTime = _castTime;
            effects = _effects;
        }

        private Skill(Skill _skill)
        {
            icon = _skill.icon;
            name = _skill.name;
            user = _skill.user;
            target = _skill.target;
            type = _skill.type;
            effects = new Effect[_skill.effects.Length];
            castTime = _skill.castTime;

            for (int efctNum = 0; efctNum < effects.Length; efctNum++)
            {
                effects[efctNum] = _skill.effects[efctNum].Copy();
            }
        }

        public void Cast()
        {
            foreach (Effect efct in effects)
            {
                if (efct == null)
                    break;

                Effect tempEfct = efct;

                efct.Tune(user.power);
            }
            target.InflictEffect(effects);
        }

        public Skill Copy()
        {
            return new Skill(this);
        }
    }
}
