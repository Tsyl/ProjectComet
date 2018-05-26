using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    class Fight
    {
        readonly Color CHARACTER_UNSELECTED = Color.White;
        readonly Color CHARACTER_SELECTED = Color.CornflowerBlue;
        readonly Color CHARACTER_HEALTH = Color.Red;
        readonly Color CHARACTER_STAMINA = Color.Yellow;

        public enum SelectionState
        {
            User,
            Skill,
            Target
        }
        SelectionState selectState;
        InputManager input = InputManager.GetInstance();

        public Party leftParty { get; set; }
        public Party rightParty { get; set; }
        public bool isOver { get; set; }
        Skill[] skills;

        Character selectedUser;
        Skill selectedSkill;
        Character selectedTarget;

        public Fight()
        {
            leftParty = new Party();
            rightParty = new Party();

            leftParty.Prepare();
            rightParty.Prepare();

            skills = new Skill[50];
            isOver = false;
            selectState = SelectionState.User;
        }

        public void Update(GameTime gameTime)
        {
            // Selecting a character.
            if (selectState == SelectionState.User)
            {
                if (input.alliedCharacter1)
                    selectedUser = leftParty.characters[0];
                else if (input.alliedCharacter2)
                    selectedUser = leftParty.characters[1];
                else if (input.alliedCharacter3)
                    selectedUser = leftParty.characters[2];
                else if (input.alliedCharacter4)
                    selectedUser = leftParty.characters[3];

                if (selectedUser != null)
                    selectState = SelectionState.Skill;
            }
            // Selecting an action for a character.
            if (selectState == SelectionState.Skill)
            {
                if (input.alliedCharacter1)
                    selectedUser = leftParty.characters[0];
                else if (input.alliedCharacter2)
                    selectedUser = leftParty.characters[1];
                else if (input.alliedCharacter3)
                    selectedUser = leftParty.characters[2];
                else if (input.alliedCharacter4)
                    selectedUser = leftParty.characters[3];

                if (input.skill1)
                    selectedSkill = selectedUser.skills[0];
                else if (input.skill2)
                    selectedSkill = selectedUser.skills[1];
                else if (input.skill3)
                    selectedSkill = selectedUser.skills[2];
                else if (input.skill4)
                    selectedSkill = selectedUser.skills[3];

                if (selectedUser == null)
                    selectState = SelectionState.User;
                else if (selectedSkill != null)
                        selectState = SelectionState.Target;
            }
            // Selecting the target of said action.
            if (selectState == SelectionState.Target)
            {
                if (input.skill1)
                    selectedSkill = selectedUser.skills[0];
                else if (input.skill2)
                    selectedSkill = selectedUser.skills[1];
                else if (input.skill3)
                    selectedSkill = selectedUser.skills[2];
                else if (input.skill4)
                    selectedSkill = selectedUser.skills[3];
                
                if (input.enemyCharacter1)
                    selectedTarget = rightParty.characters[0];
                else if (input.enemyCharacter2)
                    selectedTarget = rightParty.characters[1];
                else if (input.enemyCharacter3)
                    selectedTarget = rightParty.characters[2];
                else if (input.enemyCharacter4)
                    selectedTarget = rightParty.characters[3];
                else if (input.alliedCharacter1)
                    selectedTarget = leftParty.characters[0];
                else if (input.alliedCharacter2)
                    selectedTarget = leftParty.characters[1];
                else if (input.alliedCharacter3)
                    selectedTarget = leftParty.characters[2];
                else if (input.alliedCharacter4)
                    selectedTarget = leftParty.characters[3];

                if (selectedSkill == null)
                    selectState = SelectionState.Skill;
                else if (selectedTarget != null)
                {
                    selectedSkill.target = selectedTarget;
                    for (int skillNum = 0; skillNum < skills.Length; skillNum++)
                    {
                        if (skills[skillNum] != null)
                            continue;
                        skills[skillNum] = selectedSkill;
                        break;
                    }

                    selectedUser = null;
                    selectedSkill = null;
                    selectedTarget = null;
                    selectState = SelectionState.User;
                }
                
            }

            for (int skillNum = 0; skillNum < skills.Length; skillNum++)
            {
                if (skills[skillNum] == null)
                    break;

                skills[skillNum].Cast();
                skills[skillNum] = null;
            }

            leftParty.Update(gameTime);
            rightParty.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            const int leftPos = 15;
            const int rightPos = 600;
            const int height = 500;

            const int lifeOffset = 20;
            const int staminaOffset = 40;

            const int offset = 20;

            Character[] lpCharacters = leftParty.characters;
            Character[] rpCharacters = rightParty.characters;

            for (int numberOfCharacter = 0; numberOfCharacter < lpCharacters.Length; numberOfCharacter++)
            {
                Character chr = lpCharacters[numberOfCharacter];

                if (chr == null)
                    break;

                Vector2 namePosition = new Vector2(leftPos, (height / (lpCharacters.Length)) * numberOfCharacter + offset);
                Vector2 lifePosition = new Vector2(leftPos, (height / (lpCharacters.Length)) * numberOfCharacter + lifeOffset + offset);
                Vector2 staminaPosition = new Vector2(leftPos, (height / (lpCharacters.Length)) * numberOfCharacter + staminaOffset + offset);
                if (chr == selectedUser)
                    spriteBatch.DrawString(font, chr.name, namePosition, CHARACTER_SELECTED);
                else
                    spriteBatch.DrawString(font, chr.name, namePosition, CHARACTER_UNSELECTED);
                spriteBatch.DrawString(font, String.Format("Life: {0}/{1}", chr.currentLife, chr.maxLife), lifePosition, Color.Red);
                spriteBatch.DrawString(font, String.Format("Stamina: {0}/{1}", chr.currentStamina, chr.maxStamina), staminaPosition, Color.Yellow);
            }

            for (int numberOfCharacter = 0; numberOfCharacter < rpCharacters.Length; numberOfCharacter++)
            {
                Character chr = rpCharacters[numberOfCharacter];

                if (chr == null)
                    break;

                Vector2 namePosition = new Vector2(rightPos, (height / rpCharacters.Length + 1) * numberOfCharacter + offset);
                Vector2 lifePosition = new Vector2(rightPos, (height / rpCharacters.Length + 1) * numberOfCharacter + lifeOffset + offset);
                Vector2 staminaPosition = new Vector2(rightPos, (height / rpCharacters.Length + 1) * numberOfCharacter + staminaOffset + offset);
                spriteBatch.DrawString(font, chr.name, namePosition, Color.White);
                spriteBatch.DrawString(font, String.Format("Life: {0}/{1}", chr.currentLife, chr.maxLife), lifePosition, Color.Red);
                spriteBatch.DrawString(font, String.Format("Stamina: {0}/{1}", chr.currentStamina, chr.maxStamina), staminaPosition, Color.Yellow);
            }

            // Debug Draw
            Vector2 DebugPos = new Vector2(340, 300);
            Vector2 DebugPos2 = new Vector2(340, 320);
            spriteBatch.DrawString(font, selectState.ToString(), DebugPos, Color.AliceBlue);
            for (int skillNum = 0; skillNum < skills.Length; skillNum++)
            {
                if (skills[skillNum] == null)
                    break;

                spriteBatch.DrawString(font, skills[skillNum].name, new Vector2(DebugPos2.X, DebugPos2.Y + (20 * (skillNum + 1))), Color.AliceBlue);
            }
            
        }   // Draw
    }
}
