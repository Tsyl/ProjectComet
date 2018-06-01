﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    class Party
    {
        readonly Color CHARACTER_UNSELECTED = Color.White;
        readonly Color CHARACTER_SELECTED = Color.CornflowerBlue;
        readonly Color CHARACTER_HEALTH = Color.Red;
        readonly Color CHARACTER_STAMINA = Color.Yellow;

        public Character[] characters { get; set;  }

        public Party()
        {
            characters = new Character[4];
            characters[0] = new Character();
            characters[1] = new Character();
            characters[2] = new Character();
            characters[3] = new Character();
        }

        public void Prepare()
        {
            foreach(Character chr in characters)
            {
                if (chr == null)
                    break;

                chr.PrepareForFight();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Character chr in characters)
            {
                if (chr == null)
                    break;

                chr.Update(gameTime);
            }
        }

        public void DrawCharacterFrames(int pos, Character selectedCharacter, SpriteBatch spriteBatch, SpriteFont font)
        {
            const int lifeOffset = 20;
            const int staminaOffset = 40;

            for (int numberOfCharacter = 0; numberOfCharacter < characters.Length; numberOfCharacter++)
            {
                Character chr = characters[numberOfCharacter];

                if (chr == null)
                    continue;

                Vector2 namePosition = DrawHelper.GetCharacterPosition(true, numberOfCharacter + 1, pos, 0);
                Vector2 lifePosition = DrawHelper.GetCharacterPosition(true, numberOfCharacter + 1, pos, lifeOffset);
                Vector2 staminaPosition = DrawHelper.GetCharacterPosition(true, numberOfCharacter + 1, pos, staminaOffset);
                Vector2 skillBarPosition = DrawHelper.GetCharacterPosition(true, numberOfCharacter + 1, pos, 60);

                int healthPercent = (int)(chr.currentLife / chr.maxLife * 100);
                int staminaPercent = (int)(chr.currentStamina / chr.maxStamina * 100);
                int castPercent;

                if (chr == selectedCharacter)
                    spriteBatch.DrawString(font, chr.name, namePosition, CHARACTER_SELECTED);
                else
                    spriteBatch.DrawString(font, chr.name, namePosition, CHARACTER_UNSELECTED);
                DrawHelper.DrawFillRectangle(spriteBatch, lifePosition, healthPercent * 2, 20, CHARACTER_HEALTH);
                spriteBatch.DrawString(font, "Life", lifePosition, Color.Black);
                DrawHelper.DrawFillRectangle(spriteBatch, staminaPosition, staminaPercent * 2, 20, CHARACTER_STAMINA);
                spriteBatch.DrawString(font, "Stamina", staminaPosition, Color.Black);
                if (chr.GetCurrentSkill() != null)
                {
                    castPercent = 200 - (int)(chr.GetCurrentSkill().castTime / chr.GetSelectedSkill().castTime * 100) * 2;
                    DrawHelper.DrawFillRectangle(spriteBatch, skillBarPosition, castPercent, 20, Color.Aquamarine);
                    spriteBatch.DrawString(font, chr.GetCurrentSkill().name, skillBarPosition, Color.Black);
                }
                else
                {
                    spriteBatch.DrawString(font, "Inactive", skillBarPosition, Color.White);
                }
            }
        }
    }
}
