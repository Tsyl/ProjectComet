using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    class Skill
    {
        public Texture2D icon { get; set; }
        public string name { get; set; }
        public Character user { get; set; }
        public Character target { get; set; }
        public SkillType castingType { get; set; }
        public TargetingType targetingType { get; set; }
        public Effect[] effects { get; set; }
        public float castTime { get; set; }

        public Skill()
        {
            name = "Basic Attack";
            castingType = SkillType.Standard;
            targetingType = (TargetingType)4;
            castTime = 1;
            effects = new Effect[1];
            effects[0] = new Effect();
        }

        public Skill(string typeName)
        {
            if(typeName == "A")
            {
                name = "Basic Attack";
                castingType = SkillType.Standard;
                castTime = 2;
                effects = new Effect[1];
                effects[0] = new Effect("Attack", Stat.Health, EffectActivationType.OnSelfInjection, -5, 0, 0, 0, 0, true);
            }
            if(typeName == "B")
            {
                name = "Poison";
                castingType = SkillType.Special;
                castTime = 3;
                effects = new Effect[1];
                effects[0] = new Effect("Poison", Stat.CurrentLife, EffectActivationType.OnSelfInjection, -5, 0, 6, 2, 0, true);
            }
            if (typeName == "C")
            {
                name = "Debilitating Strike";
                castingType = SkillType.Special;
                castTime = 4;
                effects = new Effect[1];
                effects[0] = new Effect("Debilitate", Stat.CurrentStamina, EffectActivationType.OnSelfInjection, -20, 0, 6, 0, 0, false);
            }
            if (typeName == "D")
            {
                name = "Debilitating Flask";
                castingType = SkillType.Special;
                castTime = 5;
                effects = new Effect[1];
                effects[0] = new Effect("Poison", Stat.CurrentStamina, EffectActivationType.OnSelfInjection, -15, 0, 6, 2, 0, false);
            }
            if (typeName == "E")
            {
                name = "Caltrops";
                castingType = SkillType.Special;
                castTime = 3;
                effects = new Effect[1];
                effects[0] = new Effect("Caltrops", Stat.Health, EffectActivationType.OnHostReactionPrepping, -30, 5, 5, 0, 0, false);
            }
            if (typeName == "F")
            {
                name = "Sabotage";
                castingType = SkillType.Special;
                castTime = 3;
                effects = new Effect[1];
                effects[0] = new Effect("Sabotage", Stat.Health, EffectActivationType.OnHostReactionCasting, -50, 3, 3, 0, 0, false);
            }
        }

        public Skill(string _name, SkillType _type, float _castTime, Effect[] _effects)
        {
            name = _name;
            castingType = _type;
            castTime = _castTime;
            effects = _effects;
        }

        private Skill(Skill _skill)
        {
            icon = _skill.icon;
            name = _skill.name;
            user = _skill.user;
            target = _skill.target;
            castingType = _skill.castingType;
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
            target.InjectEffect(effects);
        }

        public Skill Copy()
        {
            return new Skill(this);
        }
    }
}
