using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    class Party
    {
        public string name { get; set; }
        public Character[] characters { get; set;  }
        public bool IsDown { get; set; }

        public Party(string _name)
        {
            name = _name;
            characters = new Character[3];

            IsDown = false;
        }

        public Party(string _name,
                     Character char1,
                     Character char2,
                     Character char3)
        {
            name = _name;
            characters = new Character[3];
            characters[0] = char1;
            characters[1] = char2;
            characters[2] = char3;

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
        
    }
}
