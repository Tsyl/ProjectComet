using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    class Fight
    {
        readonly Color CHARACTER_UNSELECTED = new Color(255, 255, 255);
        readonly Color CHARACTER_SELECTED = new Color(0, 255, 0);
        readonly Color CHARACTER_DOWN = Color.Purple;
        readonly Color CHARACTER_HEALTH = new Color(255, 0, 0);
        readonly Color CHARACTER_HEALTH_BG = new Color(150, 0, 0);
        readonly Color CHARACTER_STAMINA = new Color(255, 255, 0);
        readonly Color CHARACTER_STAMINA_BG = new Color(150, 150, 0);

        InputManager input = InputManager.GetInstance();
        DrawHelper drawHelper;

        public Party p1Party { get; set; }
        public Party p2Party { get; set; }
        public Party winningParty { get; set; }
        public bool isOver { get; set; }

        Player player1;
        Player player2;

        Character p1SelectedUser;
        Skill p1SelectedSkill;
        Character p1SelectedTarget;

        Character p2SelectedUser;
        Skill p2SelectedSkill;
        Character p2SelectedTarget;

        float inputDelay = 1;

        public Fight(Player p1, Player p2, Party p1party, Party p2party)
        {
            drawHelper = DrawHelper.GetInstance();

            player1 = p1;
            player2 = p2;

            p1Party = p1party;
            p2Party = p2party;

            p1Party.Prepare();
            p2Party.Prepare();

            //skills = new Skill[50];
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
                        p1SelectedUser = p1Party.characters[0].status != CharacterStatus.Down || p1Party.characters[0].status != CharacterStatus.Shocked ? p1Party.characters[0] : null;
                    else if (player1.inputs.alliedCharacter2)
                        p1SelectedUser = p1Party.characters[1].status != CharacterStatus.Down || p1Party.characters[1].status != CharacterStatus.Shocked ? p1Party.characters[1] : null;
                    else if (player1.inputs.alliedCharacter3)
                        p1SelectedUser = p1Party.characters[2].status != CharacterStatus.Down || p1Party.characters[2].status != CharacterStatus.Shocked ? p1Party.characters[2] : null;
                    //else if (player1.inputs.alliedCharacter4)
                    //    p1SelectedUser = p1Party.characters[3].status != CharacterStatus.Down ? p1Party.characters[3] : null;

                    if (p1SelectedUser != null)
                        player1.selectState = SelectionState.Skill;
                } // Player 1
                 // Player 2
                if (player2.selectState == SelectionState.User)
                {
                    if (player2.inputs.alliedCharacter1)
                        p2SelectedUser = p2Party.characters[0].status != CharacterStatus.Down || p2Party.characters[0].status != CharacterStatus.Shocked ? p2Party.characters[0] : null;
                    else if (player2.inputs.alliedCharacter2)
                        p2SelectedUser = p2Party.characters[1].status != CharacterStatus.Down || p2Party.characters[1].status != CharacterStatus.Shocked ? p2Party.characters[1] : null;
                    else if (player2.inputs.alliedCharacter3)
                        p2SelectedUser = p2Party.characters[2].status != CharacterStatus.Down || p2Party.characters[2].status != CharacterStatus.Shocked ? p2Party.characters[2] : null;
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
                        p1SelectedUser = p1Party.characters[0].status != CharacterStatus.Down || p1Party.characters[0].status != CharacterStatus.Shocked ? p1Party.characters[0] : null;
                    else if (player1.inputs.alliedCharacter2)                                                        
                        p1SelectedUser = p1Party.characters[1].status != CharacterStatus.Down || p1Party.characters[1].status != CharacterStatus.Shocked ? p1Party.characters[1] : null;
                    else if (player1.inputs.alliedCharacter3)                                                        
                        p1SelectedUser = p1Party.characters[2].status != CharacterStatus.Down || p1Party.characters[2].status != CharacterStatus.Shocked ? p1Party.characters[2] : null;
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
                        p2SelectedUser = p2Party.characters[0].status != CharacterStatus.Down || p2Party.characters[0].status != CharacterStatus.Shocked ? p2Party.characters[0] : null;
                    else if (player2.inputs.alliedCharacter2)
                        p2SelectedUser = p2Party.characters[1].status != CharacterStatus.Down || p2Party.characters[1].status != CharacterStatus.Shocked ? p2Party.characters[1] : null;
                    else if (player2.inputs.alliedCharacter3)
                        p2SelectedUser = p2Party.characters[2].status != CharacterStatus.Down || p2Party.characters[2].status != CharacterStatus.Shocked ? p2Party.characters[2] : null;
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
                        p1SelectedUser.GiveCommand(p1SelectedSkill);
                        /*for (int skillNum = 0; skillNum < skills.Length; skillNum++)
                        {
                            if (skills[skillNum] != null)
                                continue;
                            skills[skillNum] = p1SelectedSkill;
                            break;
                        }*/

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
                        p2SelectedUser.GiveCommand(p2SelectedSkill);
                        /*for (int skillNum = 0; skillNum < skills.Length; skillNum++)
                        {
                            if (skills[skillNum] != null)
                                continue;
                            skills[skillNum] = p2SelectedSkill;
                            break;
                        }*/

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
                /*for (int skillNum = 0; skillNum < skills.Length; skillNum++)
                {
                    skills[skillNum] = null;
                }*/

                // Deselect current character if said character is downed or shocked.
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
            if (winningParty == null)
            {
                Rectangle[] segments = drawHelper.GetVerticalScreenSegments(5, 0, 0);

                int statusBarWidth = 200;
                int statusBarHeight = 20;
                int charFrameThickness = 5;
                int charFrameWidth = 200 + (charFrameThickness/2);
                int charFrameHeight = 82 + charFrameThickness;
                int skillBarWidth = segments[0].Width / 3 * 2;
                int skillDescriptionWidth = segments[0].Width / 3;

                int xFrameOffset = 10;
                int xStatOffset = 11;
                int yFrameOffset = 10;
                int yStatOffset = 14;

                int iconWidth = 64;
                int iconHeight = 64;
                Vector2 lifeOffset = new Vector2(0, 20);
                Vector2 staminaOffset = new Vector2(0, 40);
                Vector2 castBarOffset = new Vector2(0, 60);

                // Player 1 Party
                for (int numberOfCharacter = 0; numberOfCharacter < p1Party.characters.Length; numberOfCharacter++)
                {
                    // Unique Variable Initialization
                    Character chr = p1Party.characters[numberOfCharacter];
                    Vector2 p1CharFrameOffset = new Vector2(xFrameOffset, yFrameOffset);
                    Vector2 p1CharStatOffset = new Vector2(xStatOffset, yStatOffset);
                    Vector2 p1SkillBarOffset = new Vector2(xStatOffset, 0);
                    Vector2 p1SkillDescriptionOffset = new Vector2(skillDescriptionWidth + xStatOffset, 0);

                    if (chr == null)
                        continue;

                    Vector2 framePosition = segments[numberOfCharacter + 1].Location.ToVector2() + p1CharFrameOffset;
                    Vector2 namePosition = segments[numberOfCharacter + 1].Location.ToVector2() + p1CharStatOffset;
                    Vector2 lifePosition = segments[numberOfCharacter + 1].Location.ToVector2() + lifeOffset + p1CharStatOffset;
                    Vector2 staminaPosition = segments[numberOfCharacter + 1].Location.ToVector2() + staminaOffset + p1CharStatOffset;
                    Vector2 castBarPosition = segments[numberOfCharacter + 1].Location.ToVector2() + castBarOffset + p1CharStatOffset;

                    int xIconOffset = 20;
                    int xIconPadding = 65;
                    int yIconPadding = (segments[4].Height - iconHeight) / 2;
                    int yIconPosition = segments[4].Location.Y + yIconPadding;

                    Rectangle skill1rect = new Rectangle(xIconOffset + xIconPadding * 0, yIconPosition, iconWidth, iconHeight);
                    Rectangle skill2rect = new Rectangle(xIconOffset + xIconPadding * 1, yIconPosition, iconWidth, iconHeight);
                    Rectangle skill3rect = new Rectangle(xIconOffset + xIconPadding * 2, yIconPosition, iconWidth, iconHeight);
                    Rectangle skill4rect = new Rectangle(xIconOffset + xIconPadding * 3, yIconPosition, iconWidth, iconHeight);

                    int healthPercent = (int)(chr.currentLife / chr.defaultLife * 100);
                    int staminaPercent = (int)(chr.currentStamina / chr.defaultStamina * 100);
                    int maxHealthPercent = (int)(chr.maxLife / chr.defaultLife * 100);
                    int maxStaminaPercent = (int)(chr.maxStamina / chr.defaultStamina * 100);
                    int fullPercent = 100;
                    int castPercent;

                    // Drawing
                        // Frame
                    if (chr == p1SelectedUser)
                    {
                        spriteBatch.DrawString(font, chr.name, namePosition, CHARACTER_SELECTED);
                    }
                    else if (chr.status != CharacterStatus.Down)
                        spriteBatch.DrawString(font, chr.name, namePosition, CHARACTER_UNSELECTED);
                    else
                        spriteBatch.DrawString(font, chr.name, namePosition, CHARACTER_DOWN);
                    drawHelper.DrawFillRectangle(spriteBatch, lifePosition, maxHealthPercent * 2, statusBarHeight, CHARACTER_HEALTH_BG);
                    drawHelper.DrawFillRectangle(spriteBatch, lifePosition, healthPercent * 2, statusBarHeight, CHARACTER_HEALTH);
                    drawHelper.DrawRectangle(spriteBatch, lifePosition, fullPercent * 2, statusBarHeight, CHARACTER_HEALTH);
                    spriteBatch.DrawString(font, "Life", lifePosition, Color.Black);
                    drawHelper.DrawFillRectangle(spriteBatch, staminaPosition, maxStaminaPercent * 2, statusBarHeight, CHARACTER_STAMINA_BG);
                    drawHelper.DrawFillRectangle(spriteBatch, staminaPosition, staminaPercent * 2, statusBarHeight, CHARACTER_STAMINA);
                    drawHelper.DrawRectangle(spriteBatch, staminaPosition, fullPercent * 2, statusBarHeight, CHARACTER_STAMINA);
                    spriteBatch.DrawString(font, "Stamina", staminaPosition, Color.Black);
                    if (chr.GetCurrentSkill() != null)
                    {
                        castPercent = statusBarWidth - (int)(chr.GetCurrentSkill().castTime / chr.GetSelectedSkill().castTime * 100) * 2;
                        drawHelper.DrawFillRectangle(spriteBatch, castBarPosition, castPercent, 20, Color.Aquamarine);
                        drawHelper.DrawRectangle(spriteBatch, castBarPosition, fullPercent * 2, 20, Color.Aquamarine);
                        spriteBatch.DrawString(font, chr.GetCurrentSkill().name, castBarPosition, Color.Black);
                    }
                    else
                    {
                        //spriteBatch.DrawString(font, "Inactive", skillBarPosition, Color.White);
                    }
                    drawHelper.DrawRectangle(spriteBatch, framePosition, charFrameWidth, charFrameHeight, 3, Color.White);

                    // Skill Bar
                        // Icons
                    if (p1SelectedUser != null)
                    {
                        if (p1SelectedUser.skills[0].icon != null) // TO REMOVE
                            spriteBatch.Draw(p1SelectedUser.skills[0].icon, skill1rect, Color.White);
                        if (p1SelectedUser.skills[1].icon != null) // TO REMOVE
                            spriteBatch.Draw(p1SelectedUser.skills[1].icon, skill2rect, Color.White);
                        if (p1SelectedUser.skills[2].icon != null) // TO REMOVE
                            spriteBatch.Draw(p1SelectedUser.skills[2].icon, skill3rect, Color.White);
                        if (p1SelectedUser.skills[3].icon != null) // TO REMOVE
                            spriteBatch.Draw(p1SelectedUser.skills[3].icon, skill4rect, Color.White);
                    }
                        // Icon Container
                    drawHelper.DrawRectangle(spriteBatch, segments[0].Location.ToVector2() + p1SkillBarOffset, skillBarWidth, segments[0].Height, 5, Color.White);
                        // Description Container
                    drawHelper.DrawRectangle(spriteBatch, segments[0].Location.ToVector2() + p1SkillDescriptionOffset, skillDescriptionWidth, segments[0].Height, 5, Color.White);

                        // Description
                    if (p1SelectedSkill != null)
                    {
                        spriteBatch.DrawString(font, String.Format("{0}:\n{1}", p1SelectedSkill.name, p1SelectedSkill.description),
                            segments[0].Location.ToVector2() + p1SkillDescriptionOffset + new Vector2(10, 6), Color.White);
                    }
                }

                // Player 2 Party
                for (int numberOfCharacter = 0; numberOfCharacter < p2Party.characters.Length; numberOfCharacter++)
                {
                    Character chr = p2Party.characters[numberOfCharacter];
                    Vector2 p2CharFrameOffset = new Vector2(segments[numberOfCharacter].Width - statusBarWidth - xStatOffset, 14);
                    Vector2 p2SkillBarOffset = new Vector2(segments[numberOfCharacter].Width - skillBarWidth - xStatOffset, 0);
                    Vector2 p2SkillDescriptionOffset = new Vector2(segments[numberOfCharacter].Width - skillDescriptionWidth - xStatOffset, 0);

                    if (chr == null)
                        continue;

                    Vector2 p2NamePosition = segments[numberOfCharacter + 1].Location.ToVector2() + p2CharFrameOffset;
                    Vector2 p2LifePosition = segments[numberOfCharacter + 1].Location.ToVector2() + lifeOffset + p2CharFrameOffset;
                    Vector2 p2StaminaPosition = segments[numberOfCharacter + 1].Location.ToVector2() + staminaOffset + p2CharFrameOffset;
                    Vector2 p2CastBarPosition = segments[numberOfCharacter + 1].Location.ToVector2() + castBarOffset + p2CharFrameOffset;

                    int healthPercent = (int)(chr.currentLife / chr.defaultLife * 100);
                    int staminaPercent = (int)(chr.currentStamina / chr.defaultStamina * 100);
                    int maxHealthPercent = (int)(chr.maxLife / chr.defaultLife * 100);
                    int maxStaminaPercent = (int)(chr.maxStamina / chr.defaultStamina * 100);
                    int castPercent;

                    if (chr == p2SelectedUser)
                    {
                        spriteBatch.DrawString(font, chr.name, p2NamePosition, CHARACTER_SELECTED);
                    }
                    else if (chr.status != CharacterStatus.Down)
                        spriteBatch.DrawString(font, chr.name, p2NamePosition, CHARACTER_UNSELECTED);
                    else
                        spriteBatch.DrawString(font, chr.name, p2NamePosition, CHARACTER_DOWN);
                    drawHelper.DrawFillRectangle(spriteBatch, p2LifePosition, maxHealthPercent * 2, statusBarHeight, CHARACTER_HEALTH_BG);
                    drawHelper.DrawFillRectangle(spriteBatch, p2LifePosition, healthPercent * 2, statusBarHeight, CHARACTER_HEALTH);
                    drawHelper.DrawRectangle(spriteBatch, p2LifePosition, statusBarWidth, statusBarHeight, CHARACTER_HEALTH);
                    spriteBatch.DrawString(font, "Life", p2LifePosition, Color.Black);
                    drawHelper.DrawFillRectangle(spriteBatch, p2StaminaPosition, maxStaminaPercent * 2, statusBarHeight, CHARACTER_STAMINA_BG);
                    drawHelper.DrawFillRectangle(spriteBatch, p2StaminaPosition, staminaPercent * 2, statusBarHeight, CHARACTER_STAMINA);
                    drawHelper.DrawRectangle(spriteBatch, p2StaminaPosition, statusBarWidth, statusBarHeight, CHARACTER_STAMINA);
                    spriteBatch.DrawString(font, "Stamina", p2StaminaPosition, Color.Black);
                    if (chr.GetCurrentSkill() != null)
                    {
                        castPercent = 200 - (int)(chr.GetCurrentSkill().castTime / chr.GetSelectedSkill().castTime * 100) * 2;
                        drawHelper.DrawFillRectangle(spriteBatch, p2CastBarPosition, castPercent, statusBarHeight, Color.Aquamarine);
                        drawHelper.DrawRectangle(spriteBatch, p2CastBarPosition, statusBarWidth, statusBarHeight, Color.Aquamarine);
                        spriteBatch.DrawString(font, chr.GetCurrentSkill().name, p2CastBarPosition, Color.Black);
                    }
                    else
                    {
                        //spriteBatch.DrawString(font, "Inactive", skillBarPosition, Color.White);
                    }
                    drawHelper.DrawRectangle(spriteBatch, p2NamePosition, statusBarWidth, 81, Color.White);
                    
                    int xIconOffset = segments[numberOfCharacter].Width - iconWidth - 20;
                    int xIconPadding = 65;
                    int yIconPadding = (segments[0].Height - iconHeight) / 2;
                    int yIconPosition = segments[0].Location.Y + yIconPadding;

                    Rectangle skill1rect = new Rectangle(xIconOffset - xIconPadding * 3, yIconPosition, iconWidth, iconHeight);
                    Rectangle skill2rect = new Rectangle(xIconOffset - xIconPadding * 2, yIconPosition, iconWidth, iconHeight);
                    Rectangle skill3rect = new Rectangle(xIconOffset - xIconPadding * 1, yIconPosition, iconWidth, iconHeight);
                    Rectangle skill4rect = new Rectangle(xIconOffset - xIconPadding * 0, yIconPosition, iconWidth, iconHeight);

                    if (p2SelectedUser != null)
                    {
                        if (p2SelectedUser.skills[0].icon != null)  // TO REMOVE
                            spriteBatch.Draw(p2SelectedUser.skills[0].icon, skill1rect, Color.White);
                        if (p2SelectedUser.skills[1].icon != null)  // TO REMOVE
                            spriteBatch.Draw(p2SelectedUser.skills[1].icon, skill2rect, Color.White);
                        if (p2SelectedUser.skills[2].icon != null)  // TO REMOVE
                            spriteBatch.Draw(p2SelectedUser.skills[2].icon, skill3rect, Color.White);
                        if (p2SelectedUser.skills[3].icon != null)  // TO REMOVE
                            spriteBatch.Draw(p2SelectedUser.skills[3].icon, skill4rect, Color.White);
                    }

                    drawHelper.DrawRectangle(spriteBatch, segments[4].Location.ToVector2() + p2SkillBarOffset, skillBarWidth, segments[4].Height, Color.White);
                    drawHelper.DrawRectangle(spriteBatch, segments[4].Location.ToVector2() + p2SkillBarOffset, skillDescriptionWidth, segments[4].Height, Color.White);

                    if (p2SelectedSkill != null)
                    {
                        spriteBatch.DrawString(font, String.Format("{0}:\n{1}", p2SelectedSkill.name, p2SelectedSkill.description),
                            segments[4].Location.ToVector2() + p2SkillBarOffset + new Vector2(10, 6), Color.White);
                    }
                }
            }
            else
            {
                Vector2 DebugPos = new Vector2(340, 300);
                spriteBatch.DrawString(font, String.Format("{0} wins!", winningParty.name), DebugPos, Color.AliceBlue);
            }
            
        }   // Draw

        public Character SelectCharacter(Character chr)
        {
            return chr;
        }
    }
}
