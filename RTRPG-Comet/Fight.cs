using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    class Fight
    {
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

        /// <summary>
        /// Controls the player's actions and keeps tabs on the skills that will be casted.
        /// </summary>
        /// <param name="gameTime"></param>
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
                        selectedUser.GiveCommand(selectedSkill);
                        break;
                    }

                    selectedUser = null;
                    selectedSkill = null;
                    selectedTarget = null;
                    selectState = SelectionState.User;
                }
                
            }

            leftParty.Update(gameTime);
            rightParty.Update(gameTime);

            for (int skillNum = 0; skillNum < skills.Length; skillNum++)
            {
                skills[skillNum] = null;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            const int leftPos = 15;
            const int rightPos = 500;

            leftParty.DrawCharacterFrames(leftPos, selectedUser, spriteBatch, font);
            rightParty.DrawCharacterFrames(rightPos, selectedUser, spriteBatch, font);

            // Debug Draw
            Vector2 DebugPos = new Vector2(340, 300);
            Vector2 DebugPos2 = new Vector2(340, 320);
            spriteBatch.DrawString(font, selectState.ToString(), DebugPos, Color.AliceBlue);
            
        }   // Draw
    }
}
