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
        public CharacterStatus status;
        public DowningType downingType;

        public float power { get; set; }
        public float speed { get; set; }
        public float endurance { get; set; }

        public Skill[] skills { get; set; }
        public Effect[] effects { get; set; }
        Skill selectedSkill { get; set; }
        Skill currentSkill { get; set; }

        // ListenerMethods

        public Character()
        {
            name = "TEST";

            defaultLife = 100;
            defaultStamina = 100;
            defaultRegen = 0.1f;
            status = CharacterStatus.Open;
            downingType = DowningType.Either;

            power = 100;
            speed = 100;
            endurance = 100;

            skills = new Skill[4];
            effects = new Effect[20];

            skills[0] = new Skill("A");
            skills[1] = new Skill("B");
            skills[2] = new Skill("E");
            skills[3] = new Skill("F");
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
            float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Alive Loop
            if (status != CharacterStatus.Down)
            {
                // Health Regen
                if (regen > 0)
                {
                    currentStamina += regen*seconds;
                }

                // Casting Delay
                if (currentSkill != null)
                {
                    currentSkill.castTime -= seconds;
                }

                // Effects
                for (int effectNum = 0; effectNum < effects.Length; effectNum++)
                {
                    if (effects[effectNum] == null)
                        break;

                    effects[effectNum].Update(gameTime);

                    if (effects[effectNum].host != this)
                        effects[effectNum] = null;
                }

                // Casting
                if (selectedSkill != null && currentSkill.target.status != CharacterStatus.Down)
                {
                    if (currentSkill.castTime <= 0)
                    {
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

                // Check if requirements are met for downed
                switch(downingType)
                {
                    case DowningType.Life:
                        if (currentLife <= 0)
                            status = CharacterStatus.Down;
                        break;
                    case DowningType.Stamina:
                        if (currentStamina <= 0)
                            status = CharacterStatus.Down;
                        break;
                    case DowningType.Either:
                        if (currentStamina <= 0 || currentLife <= 0)
                            status = CharacterStatus.Down;
                        break;
                    case DowningType.Both:
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
    }
}
