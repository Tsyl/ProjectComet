using Newtonsoft.Json;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace Comet
{
    class Skill
    {
        const int MAX_EFFECTS = 4;

        public string name { get; set; }
        public Texture2D icon { get; set; }
        public SoundEffect sfx { get; set; }
        public string description { get; set; }
        public SkillType castingType { get; set; }
        public TargetingType targetingType { get; set; }
        public float castTime { get; set; }
        public float cost { get; set; }
        public ResourceType costType { get; set; }
        public Effect[] effects { get; set; }

        public string iconName { get; set; }
        public string sfxName { get; set; }

        public Character user { get; set; }
        public Character target { get; set; }

        public Skill()
        {
            name = "Basic Attack";
            castingType = SkillType.Standard;
            targetingType = (TargetingType)4;
            castTime = 1;
            effects = new Effect[MAX_EFFECTS];
        }

        public Skill(string _name, Texture2D _icon, SkillType _type, float _castTime, params Effect[] _effects)
        {
            name = _name;
            icon = _icon;
            castingType = _type;
            castTime = _castTime;
            effects = _effects;
        }

        public Skill(string _name, Texture2D _icon, SoundEffect _sfx, string _description,
            SkillType _castingType, TargetingType _targetingType, float _castTime,
            float _cost, ResourceType _costType, params Effect[] _effects)
        {
            name = _name;
            description = _description;
            icon = _icon;
            sfx = _sfx;
            castingType = _castingType;
            targetingType = _targetingType;
            castTime = _castTime;
            cost = _cost;
            costType = _costType;
            effects = new Effect[MAX_EFFECTS];
            for (int effectNum = 0; effectNum < effects.Length; effectNum++)
            {
                effects[effectNum] = _effects[effectNum];
            }
        }

        [JsonConstructor]
        public Skill(string name, string iconName, string sfxName, string description,
            SkillType castingType, TargetingType targetingType, float castTime,
            float cost, ResourceType costType, params Effect[] effects)
        {
            this.name = name;
            this.description = description;
            this.iconName = iconName;
            this.sfxName = sfxName;
            this.castingType = castingType;
            this.targetingType = targetingType;
            this.castTime = castTime;
            this.cost = cost;
            this.costType = costType;
            this.effects = new Effect[MAX_EFFECTS];
            for (int effectNum = 0; effectNum < this.effects.Length; effectNum++)
            {
                this.effects[effectNum] = effects[effectNum];
            }
        }

        private Skill(Skill _skill)
        {
            name = _skill.name;
            icon = _skill.icon;
            sfx = _skill.sfx;
            description = _skill.description;
            user = _skill.user;
            target = _skill.target;
            castingType = _skill.castingType;
            targetingType = _skill.targetingType;
            castTime = _skill.castTime;
            cost = _skill.cost;
            costType = _skill.costType;

            effects = new Effect[_skill.effects.Length];
            for (int efctNum = 0; efctNum < effects.Length; efctNum++)
            {
                if (_skill.effects[efctNum] == null)
                    break;

                effects[efctNum] = _skill.effects[efctNum].Copy();
            }
        }

        public void SubstractCosts()
        {
            switch (costType)
            {
                case ResourceType.Life:
                    user.currentLife -= cost;
                    break;
                case ResourceType.Stamina:
                    user.currentStamina -= cost;
                    break;
                case (ResourceType)3:
                    user.currentLife -= cost;
                    user.currentStamina -= cost;
                    break;
                case ResourceType.TensionOptional:
                    user.currentTension -= cost >= user.currentTension ? cost : user.currentTension;
                    break;
                case (ResourceType)5:
                    user.currentLife -= cost;
                    user.currentTension -= cost >= user.currentTension ? cost : user.currentTension;
                    break;
                case (ResourceType)6:
                    user.currentStamina -= cost;
                    user.currentTension -= cost >= user.currentTension ? cost : user.currentTension;
                    break;
                case (ResourceType)7:
                    user.currentLife -= cost;
                    user.currentStamina -= cost;
                    user.currentTension -= cost >= user.currentTension ? cost : user.currentTension;
                    break;
                case ResourceType.TensionRequired:
                case (ResourceType)12:
                    user.currentTension -= cost;
                    break;
                case (ResourceType)9:
                case (ResourceType)13:
                    user.currentLife -= cost;
                    user.currentTension -= cost;
                    break;
                case (ResourceType)10:
                case (ResourceType)14:
                    user.currentStamina -= cost;
                    user.currentTension -= cost;
                    break;
                case (ResourceType)11:
                case (ResourceType)15:
                    user.currentLife -= cost;
                    user.currentStamina -= cost;
                    user.currentTension -= cost;
                    break;
            }
        }

        public bool CanCast()
        {
            bool hasResources = true;
            bool castTimeExpired = castTime <= 0;

            switch (costType)
            {
                // ResourceType 4 (TensionOptional) does not make a difference
                // since it only reduces tension if present. And since 0 is no
                // cost type, neither 0 nor 4 get cases.
                case ResourceType.Life:
                case (ResourceType)5:
                    hasResources = user.currentLife > cost;
                    break;
                case ResourceType.Stamina:
                case (ResourceType)6:
                    hasResources = user.currentStamina > cost;
                    break;
                case (ResourceType)3:
                case (ResourceType)7:
                    hasResources = user.currentLife > cost;
                    hasResources = user.currentStamina > cost;
                    break;
                case ResourceType.TensionRequired:
                case (ResourceType)12:
                    hasResources = user.currentTension > cost;
                    break;
                case (ResourceType)9:
                case (ResourceType)13:
                    hasResources = user.currentLife > cost;
                    hasResources = user.currentTension > cost;
                    break;
                case (ResourceType)10:
                case (ResourceType)14:
                    hasResources = user.currentStamina > cost;
                    hasResources = user.currentTension > cost;
                    break;
                case (ResourceType)11:
                case (ResourceType)15:
                    hasResources = user.currentLife > cost;
                    hasResources = user.currentStamina > cost;
                    hasResources = user.currentTension > cost;
                    break;
            }

            return hasResources && castTimeExpired;
        }

        public void Cast()
        {
            foreach (Effect efct in effects)
            {
                if (efct == null)
                    break;

                Effect tempEfct = efct;

                tempEfct.Tune(user.power);
                target.InjectEffect(tempEfct);
            }
        }

        public Skill Copy()
        {
            return new Skill(this);
        }
    }
}
