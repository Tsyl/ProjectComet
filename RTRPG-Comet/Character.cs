using Newtonsoft.Json;
using Microsoft.Xna.Framework;

namespace Comet
{
    class Character
    {
        int SKILL_CAP = 4;

        public string name { get; set; }
        public float defaultLife { get; set; }
        public float defaultStamina { get; set; }
        public float defaultRegen { get; set; }
        public float defaultTensionLimit { get; set; }
        public KnockoutCondition knockoutCondition;

        public float power { get; set; }
        public float speed { get; set; }
        public float endurance { get; set; }

        public Skill[] skills { get; set; }

        public float currentLife { get; set; }
        public float maxLife { get; set; }
        public float currentStamina { get; set; }
        public float maxStamina { get; set; }
        public float currentTension { get; set; }
        public float maxTension { get; set; }
        public float regen { get; set; }
        public CharacterStatus status;

        Effect[] effects { get; set; }
        Skill selectedSkill { get; set; }
        Skill currentSkill { get; set; }

        [JsonConstructor]
        public Character(
                         string name,
                         float defaultLife,
                         float defaultStamina,
                         float defaultRegen,
                         float defaultTensionLimit,
                         KnockoutCondition knockoutCondition,
                         float power,
                         float speed,
                         float endurance,
                         params Skill[] skills
                         )
        {
            this.name = name;

            this.defaultLife = defaultLife;
            this.defaultStamina = defaultStamina;
            this.defaultRegen = defaultRegen;
            this.defaultTensionLimit = defaultTensionLimit;
            this.knockoutCondition = knockoutCondition;

            this.power = power;
            this.speed = speed;
            this.endurance = endurance;

            this.skills = new Skill[SKILL_CAP];
            for (int skillNum = 0; skillNum < this.skills.Length; skillNum++)
            {
                this.skills[skillNum] = skills[skillNum];
            }

            effects = new Effect[20];
        }

        public void PrepareForFight()
        {
            maxLife = defaultLife;
            currentLife = maxLife;
            maxStamina = defaultStamina;
            currentStamina = maxStamina;
            maxTension = defaultTensionLimit;
            currentTension = 0;

            foreach(Skill skl in skills)
            {
                if(skl != null)
                    skl.user = this;
            }
        }   // PrepareForFight

        public void Update(GameTime gameTime)
        {
            float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Alive
            if (status != CharacterStatus.Down)
            {
                // NOT Shocked
                if (status != CharacterStatus.Shocked)
                {
                    // Stamina Regen
                    if (regen > 0)
                    {
                        currentStamina += regen * seconds;
                    }

                    // Casting Countdown
                    if (currentSkill != null)
                    {
                        currentSkill.castTime -= seconds;
                    }

                    // Casting
                    if (selectedSkill != null && currentSkill.target.status != CharacterStatus.Down)
                    {
                        if (currentSkill.CanCast())
                        {
                            currentSkill.SubstractCosts();
                            currentSkill.Cast();
                            if (currentSkill.castingType == SkillType.Standard)
                            {
                                currentSkill = selectedSkill.Copy();
                            }
                            else
                            {
                                GiveCommand(null);
                            }
                        }
                    }
                    else
                    {
                        GiveCommand(null);
                    }
                } // NOT Shocked
                // Shocked
                else
                {
                    if(selectedSkill != null)
                    {
                        GiveCommand(null);
                    }
                }

                // Prevent Stat Overflow
                if (currentLife > maxLife)
                    currentLife = maxLife;
                if (currentStamina > maxStamina)
                    currentStamina = maxStamina;
                if (currentTension > maxTension)
                    currentTension = maxTension;

                // Effect Reaction
                for (int effectNum = 0; effectNum < effects.Length; effectNum++)
                {
                    if (effects[effectNum] == null)
                        break;

                    effects[effectNum].Update(gameTime);

                    if (effects[effectNum].host != this)
                        effects[effectNum] = null;
                }
                
                // Check if requirements are met for knockout
                switch(knockoutCondition)
                {
                    case KnockoutCondition.Life:
                        if (currentLife <= 0)
                            status = CharacterStatus.Down;
                        break;
                    case KnockoutCondition.Stamina:
                        if (currentStamina <= 0)
                            status = CharacterStatus.Down;
                        break;
                    case KnockoutCondition.Either:
                        if (currentStamina <= 0 || currentLife <= 0)
                            status = CharacterStatus.Down;
                        break;
                    case KnockoutCondition.Both:
                        if (currentStamina <= 0 && currentLife <= 0)
                            status = CharacterStatus.Down;
                        break;
                }
            }
            else // Down Loop
            {

            }
        }   // Update

        public void GiveCommand(Skill skill)
        {
            selectedSkill = skill;
            currentSkill = null;
            if (selectedSkill != null)
                currentSkill = selectedSkill.Copy();
        }

        /// <summary>
        /// This method will inject the given effect into a character.
        /// It will also automatically sort the effect list so that the
        /// effects ending first (namely those with the shortest delay + 
        /// duration) are the ones at the end of the list.
        /// </summary>
        /// <param name="efct">The effect to inject on the character. </param>
        public void InjectEffect(Effect efct)
        {
            for(int effectNum = 0; effectNum < effects.Length; effectNum++)
            {
                if (effects[effectNum] != null)
                {
                    if (effects[effectNum].delay + effects[effectNum].triggerDuration > efct.delay + efct.triggerDuration)
                    {
                        Effect tmp = effects[effectNum].Copy();
                        effects[effectNum] = efct;
                        efct = tmp;
                    }
                    continue;
                }
                effects[effectNum] = efct;
                efct.host = this;
                break;
            }
        }

        /// <summary>
        /// This method will inject the given effect into a character.
        /// It will also automatically sort the effect list so that the
        /// effects ending first (namely those with the shortest delay + 
        /// duration) are the ones at the end of the list.
        /// </summary>
        /// <param name="efcts">The effects to inject on the character. </param>
        public void InjectEffect(Effect[] efcts)
        {
            foreach(Effect efct in efcts)
            {
                InjectEffect(efct);
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

        public Character Copy()
        {
            return new Character(name,
                                 defaultLife,
                                 defaultStamina,
                                 defaultRegen,
                                 defaultTensionLimit,
                                 knockoutCondition,
                                 power,
                                 speed,
                                 endurance,
                                 skills);
        }
    }
}
