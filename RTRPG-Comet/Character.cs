using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Comet
{
    class Character
    {
        public string name { get; set; }
        public float defaultLife { get; set; }
        public float defaultStamina { get; set; }
        public float defaultRegen { get; set; }

        public float currentLife { get; set; }
        public float maxLife { get; set; }
        public float currentStamina { get; set; }
        public float maxStamina { get; set; }
        public float regen { get; set; }

        public float power { get; set; }
        public float speed { get; set; }
        public float endurance { get; set; }

        public Skill[] skills { get; set; }
        public Effect[] effects { get; set; }
        Skill selectedSkill { get; set; }
        Skill currentSkill { get; set; }

        public Character()
        {
            name = "TEST";

            defaultLife = 100;
            defaultStamina = 100;
            defaultRegen = 100;

            power = 150;
            speed = 100;
            endurance = 100;

            skills = new Skill[4];
            effects = new Effect[20];

            skills[0] = new Skill();
        }

        public void PrepareForFight()
        {
            maxLife = defaultLife;
            currentLife = maxLife;
            maxStamina = defaultStamina;
            currentStamina = maxStamina;

            foreach(Skill skl in skills)
            {
                if(skl != null)
                    skl.user = this;
            }
        }   // PrepareForFight

        public void Update(GameTime gameTime)
        {
            for (int effectNum = 0; effectNum < effects.Length; effectNum++)
            {
                if (effects[effectNum] == null)
                    break;
                effects[effectNum].Activate(this);
                effects[effectNum] = null;
            }

            if (selectedSkill != null)
            {
                float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
                currentSkill.castTime -= seconds;

                if(currentSkill.castTime <= 0)
                {
                    currentSkill.Cast();
                    if(currentSkill.type == SkillType.Standard)
                    {
                        currentSkill = selectedSkill.Copy();
                    }
                    else
                    {
                        currentSkill = null;
                    }
                }
            }
        }   // Update

        public void GiveCommand(Skill skill)
        {
            selectedSkill = skill;
            currentSkill = selectedSkill.Copy();
        }

        public void InflictEffect(Effect efct)
        {
            for(int effectNum = 0; effectNum < effects.Length; effectNum++)
            {
                if (effects[effectNum] != null)
                    continue;
                effects[effectNum] = efct;
                break;
            }
        }

        public void InflictEffect(Effect[] efcts)
        {
            foreach(Effect efct in efcts)
            {
                InflictEffect(efct);
            }
        }

        public Skill GetSelectedSkill()
        {
            return selectedSkill;
        }

        public Skill GetCurrentSkill()
        {
            return currentSkill;
        }
    }
}
