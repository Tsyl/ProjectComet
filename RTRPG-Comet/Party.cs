using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    class Party
    {
        readonly Color CHARACTER_UNSELECTED = new Color(255, 255, 255);
        readonly Color CHARACTER_SELECTED = new Color(0, 255, 0);
        readonly Color CHARACTER_DOWN = Color.Purple;
        readonly Color CHARACTER_HEALTH = new Color(255, 0, 0);
        readonly Color CHARACTER_HEALTH_BG = new Color(100, 0, 0);
        readonly Color CHARACTER_STAMINA = new Color(255, 255, 0);
        readonly Color CHARACTER_STAMINA_BG = new Color(100, 100, 0);

        public string name { get; set; }
        public Character[] characters { get; set;  }
        public bool IsDown { get; set; }

        public Party(string name)
        {
            characters = new Character[4];
            characters[0] = new Character();
            characters[1] = new Character();
            characters[2] = new Character();
            characters[3] = new Character();

            IsDown = false;
        }

        public void Prepare()
        {
            foreach(Character chr in characters)
            {
                if (chr == null)
                    continue;

                chr.PrepareForFight();
            }
        }

        public void Update(GameTime gameTime)
        {
            int charactersDown = 0;
            foreach (Character chr in characters)
            {
                if (chr == null)
                    continue;
                chr.Update(gameTime);
                if(chr.status == CharacterStatus.Down)
                    charactersDown++;
            }

            if (charactersDown == characters.Length)
                IsDown = true;
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
                int fullPercent = 100;
                int castPercent;

                if (chr == selectedCharacter)
                    spriteBatch.DrawString(font, chr.name, namePosition, CHARACTER_SELECTED);
                else if (chr.status != CharacterStatus.Down)
                    spriteBatch.DrawString(font, chr.name, namePosition, CHARACTER_UNSELECTED);
                else
                    spriteBatch.DrawString(font, chr.name, namePosition, CHARACTER_DOWN);
                DrawHelper.DrawFillRectangle(spriteBatch, lifePosition, fullPercent * 2, 20, CHARACTER_HEALTH_BG);
                DrawHelper.DrawFillRectangle(spriteBatch, lifePosition, healthPercent * 2, 20, CHARACTER_HEALTH);
                spriteBatch.DrawString(font, "Life", lifePosition, Color.Black);
                DrawHelper.DrawFillRectangle(spriteBatch, staminaPosition, fullPercent * 2, 20, CHARACTER_STAMINA_BG);
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
