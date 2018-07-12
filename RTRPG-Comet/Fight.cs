using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    class Fight
    {
        InputManager input = InputManager.GetInstance();

        public Party p1Party { get; set; }
        public Party p2Party { get; set; }
        public Party winningParty { get; set; }
        public bool isOver { get; set; }

        Player player1;
        Player player2;
        Skill[] skills;

        Character p1SelectedUser;
        Skill p1SelectedSkill;
        Character p1SelectedTarget;

        Character p2SelectedUser;
        Skill p2SelectedSkill;
        Character p2SelectedTarget;

        float inputDelay = 1;

        public Fight(Player p1, Player p2)
        {
            player1 = p1;
            player2 = p2;

            p1Party = new Party("Red");
            p2Party = new Party("Blue");

            p1Party.Prepare();
            p2Party.Prepare();

            skills = new Skill[50];
            winningParty = null;
            isOver = false;
        }

        /// <summary>
        /// Controls the player's actions and keeps tabs on the skills that will be casted.
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (winningParty == null)
            {
                // Selecting a character.
                // Player 1
                if (player1.selectState == SelectionState.User)
                {
                    if (player1.inputs.alliedCharacter1)
                        p1SelectedUser = p1Party.characters[0].status != CharacterStatus.Down ? p1Party.characters[0] : null;
                    else if (player1.inputs.alliedCharacter2)
                        p1SelectedUser = p1Party.characters[1].status != CharacterStatus.Down ? p1Party.characters[1] : null;
                    else if (player1.inputs.alliedCharacter3)
                        p1SelectedUser = p1Party.characters[2].status != CharacterStatus.Down ? p1Party.characters[2] : null;
                    //else if (player1.inputs.alliedCharacter4)
                    //    p1SelectedUser = p1Party.characters[3].status != CharacterStatus.Down ? p1Party.characters[3] : null;

                    if (p1SelectedUser != null)
                        player1.selectState = SelectionState.Skill;
                } // Player 1
                  // Player 2
                if (player2.selectState == SelectionState.User)
                {
                    if (player2.inputs.alliedCharacter1)
                        p2SelectedUser = p2Party.characters[0].status != CharacterStatus.Down ? p2Party.characters[0] : null;
                    else if (player2.inputs.alliedCharacter2)
                        p2SelectedUser = p2Party.characters[1].status != CharacterStatus.Down ? p2Party.characters[1] : null;
                    else if (player2.inputs.alliedCharacter3)
                        p2SelectedUser = p2Party.characters[2].status != CharacterStatus.Down ? p2Party.characters[2] : null;
                    //else if (player2.inputs.alliedCharacter4)
                    //    p2SelectedUser = p2Party.characters[3].status != CharacterStatus.Down ? p2Party.characters[3] : null;

                    if (p2SelectedUser != null)
                        player2.selectState = SelectionState.Skill;
                } // Player 2

                // Selecting an action for a character.
                // Player 1
                if (player1.selectState == SelectionState.Skill)
                {
                    if (player1.inputs.alliedCharacter1)
                        p1SelectedUser = p1Party.characters[0].status != CharacterStatus.Down ? p1Party.characters[0] : null;
                    else if (player1.inputs.alliedCharacter2)
                        p1SelectedUser = p1Party.characters[1].status != CharacterStatus.Down ? p1Party.characters[1] : null;
                    else if (player1.inputs.alliedCharacter3)
                        p1SelectedUser = p1Party.characters[2].status != CharacterStatus.Down ? p1Party.characters[2] : null;
                    //else if (player1.inputs.alliedCharacter4)
                    //    p1SelectedUser = p1Party.characters[3].status != CharacterStatus.Down ? p1Party.characters[3] : null;

                    if (player1.inputs.skill1)
                        p1SelectedSkill = p1SelectedUser.skills[0];
                    else if (player1.inputs.skill2)
                        p1SelectedSkill = p1SelectedUser.skills[1];
                    else if (player1.inputs.skill3)
                        p1SelectedSkill = p1SelectedUser.skills[2];
                    else if (player1.inputs.skill4)
                        p1SelectedSkill = p1SelectedUser.skills[3];

                    if (p1SelectedUser == null)
                        player1.selectState = SelectionState.User;
                    else if (p1SelectedSkill != null)
                        player1.selectState = SelectionState.Target;
                } // Player 1
                  // Player 2
                if (player2.selectState == SelectionState.Skill)
                {
                    if (player2.inputs.alliedCharacter1)
                        p2SelectedUser = p2Party.characters[0].status != CharacterStatus.Down ? p2Party.characters[0] : null;
                    else if (player2.inputs.alliedCharacter2)
                        p2SelectedUser = p2Party.characters[1].status != CharacterStatus.Down ? p2Party.characters[1] : null;
                    //else if (player2.inputs.alliedCharacter3)
                    //    p2SelectedUser = p2Party.characters[2].status != CharacterStatus.Down ? p2Party.characters[2] : null;
                    //else if (player2.inputs.alliedCharacter4)
                    //    p2SelectedUser = p2Party.characters[3].status != CharacterStatus.Down ? p2Party.characters[3] : null;

                    if (player2.inputs.skill1)
                        p2SelectedSkill = p2SelectedUser.skills[0];
                    else if (player2.inputs.skill2)
                        p2SelectedSkill = p2SelectedUser.skills[1];
                    else if (player2.inputs.skill3)
                        p2SelectedSkill = p2SelectedUser.skills[2];
                    else if (player2.inputs.skill4)
                        p2SelectedSkill = p2SelectedUser.skills[3];

                    if (p2SelectedUser == null)
                        player2.selectState = SelectionState.User;
                    else if (p2SelectedSkill != null)
                        player2.selectState = SelectionState.Target;
                } // Player 2

                // Selecting the target of said action.
                // Player 1
                if (player1.selectState == SelectionState.Target)
                {
                    if (player1.inputs.skill1)
                        p1SelectedSkill = p1SelectedUser.skills[0];
                    else if (player1.inputs.skill2)
                        p1SelectedSkill = p1SelectedUser.skills[1];
                    else if (player1.inputs.skill3)
                        p1SelectedSkill = p1SelectedUser.skills[2];
                    else if (player1.inputs.skill4)
                        p1SelectedSkill = p1SelectedUser.skills[3];

                    if (player1.inputs.enemyCharacter1)
                        p1SelectedTarget = p2Party.characters[0].status != CharacterStatus.Down ? p2Party.characters[0] : null;
                    else if (player1.inputs.enemyCharacter2)
                        p1SelectedTarget = p2Party.characters[1].status != CharacterStatus.Down ? p2Party.characters[1] : null;
                    else if (player1.inputs.enemyCharacter3)
                        p1SelectedTarget = p2Party.characters[2].status != CharacterStatus.Down ? p2Party.characters[2] : null;
                    //else if (player1.inputs.enemyCharacter4)
                    //    p1SelectedTarget = p2Party.characters[3].status != CharacterStatus.Down ? p2Party.characters[3] : null;

                    if (player1.inputs.alliedCharacter1)
                        p1SelectedTarget = p1Party.characters[0].status != CharacterStatus.Down ? p1Party.characters[0] : null;
                    else if (player1.inputs.alliedCharacter2)
                        p1SelectedTarget = p1Party.characters[1].status != CharacterStatus.Down ? p1Party.characters[1] : null;
                    else if (player1.inputs.alliedCharacter3)
                        p1SelectedTarget = p1Party.characters[2].status != CharacterStatus.Down ? p1Party.characters[2] : null;
                    //else if (player1.inputs.alliedCharacter4)
                    //    p1SelectedTarget = p1Party.characters[3].status != CharacterStatus.Down ? p1Party.characters[3] : null;

                    if (p1SelectedSkill == null)
                        player1.selectState = SelectionState.Skill;
                    else if (p1SelectedTarget != null)
                    {
                        p1SelectedSkill.target = p1SelectedTarget;
                        for (int skillNum = 0; skillNum < skills.Length; skillNum++)
                        {
                            if (skills[skillNum] != null)
                                continue;
                            skills[skillNum] = p1SelectedSkill;
                            p1SelectedUser.GiveCommand(p1SelectedSkill);
                            break;
                        }

                        p1SelectedUser = null;
                        p1SelectedSkill = null;
                        p1SelectedTarget = null;
                        player1.selectState = SelectionState.User;
                    }

                } // Player 1
                  // Player 2
                if (player2.selectState == SelectionState.Target)
                {
                    if (player2.inputs.skill1)
                        p2SelectedSkill = p2SelectedUser.skills[0];
                    else if (player2.inputs.skill2)
                        p2SelectedSkill = p2SelectedUser.skills[1];
                    else if (player2.inputs.skill3)
                        p2SelectedSkill = p2SelectedUser.skills[2];
                    else if (player2.inputs.skill4)
                        p2SelectedSkill = p2SelectedUser.skills[3];

                    if (player2.inputs.enemyCharacter1)
                        p2SelectedTarget = p1Party.characters[0].status != CharacterStatus.Down ? p1Party.characters[0] : null;
                    else if (player2.inputs.enemyCharacter2)
                        p2SelectedTarget = p1Party.characters[1].status != CharacterStatus.Down ? p1Party.characters[1] : null;
                    else if (player2.inputs.enemyCharacter3)
                        p2SelectedTarget = p1Party.characters[2].status != CharacterStatus.Down ? p1Party.characters[2] : null;
                    //else if (player2.inputs.enemyCharacter4)
                    //    p2SelectedTarget = p1Party.characters[3].status != CharacterStatus.Down ? p1Party.characters[3] : null;

                    if (player2.inputs.alliedCharacter1)
                        p2SelectedTarget = p2Party.characters[0].status != CharacterStatus.Down ? p2Party.characters[0] : null;
                    else if (player2.inputs.alliedCharacter2)
                        p2SelectedTarget = p2Party.characters[1].status != CharacterStatus.Down ? p2Party.characters[1] : null;
                    else if (player2.inputs.alliedCharacter3)
                        p2SelectedTarget = p2Party.characters[2].status != CharacterStatus.Down ? p2Party.characters[2] : null;
                    //else if (player2.inputs.alliedCharacter4)
                    //    p2SelectedTarget = p2Party.characters[3].status != CharacterStatus.Down ? p2Party.characters[3] : null;

                    if (p2SelectedSkill == null)
                        player2.selectState = SelectionState.Skill;
                    else if (p2SelectedTarget != null)
                    {
                        p2SelectedSkill.target = p2SelectedTarget;
                        for (int skillNum = 0; skillNum < skills.Length; skillNum++)
                        {
                            if (skills[skillNum] != null)
                                continue;
                            skills[skillNum] = p2SelectedSkill;
                            p2SelectedUser.GiveCommand(p2SelectedSkill);
                            break;
                        }

                        p2SelectedUser = null;
                        p2SelectedSkill = null;
                        p2SelectedTarget = null;
                        player2.selectState = SelectionState.User;
                    }

                } // Player 2

                // Update parties and skill queue
                p1Party.Update(gameTime);
                p2Party.Update(gameTime);

                // Refresh the skill list (Probably being removed, btw)
                for (int skillNum = 0; skillNum < skills.Length; skillNum++)
                {
                    skills[skillNum] = null;
                }

                // Deselect current character if said character is downed.
                if (p1SelectedUser != null && p1SelectedUser.status == CharacterStatus.Down)
                    p1SelectedUser = null;
                if (p2SelectedUser != null && p2SelectedUser.status == CharacterStatus.Down)
                    p2SelectedUser = null;

                if (p1Party.IsDown)
                    winningParty = p2Party;
                else if (p2Party.IsDown)
                    winningParty = p1Party;
            }
            else
            {
                float seconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
                inputDelay -= seconds;

                if(inputDelay <= 0 && input.Any())
                    isOver = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            const int leftPos = 15;
            const int rightPos = 500;

            if (winningParty == null)
            {
                p1Party.DrawCharacterFrames(leftPos, p1SelectedUser, spriteBatch, font);
                p2Party.DrawCharacterFrames(rightPos, p2SelectedUser, spriteBatch, font);

                // Debug Draw
                //Vector2 DebugPos = new Vector2(340, 300);
                //Vector2 DebugPos2 = new Vector2(340, 320);
                //spriteBatch.DrawString(font, player2.selectState.ToString(), DebugPos, Color.AliceBlue);
            }
            else
            {
                Vector2 DebugPos = new Vector2(340, 300);
                spriteBatch.DrawString(font, String.Format("\"{0}\" Team wins!", winningParty.name), DebugPos, Color.AliceBlue);
            }
            
        }   // Draw

        public Character SelectCharacter(Character chr)
        {
            return chr;
        }
    }
}
