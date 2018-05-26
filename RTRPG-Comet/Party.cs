using Microsoft.Xna.Framework;

namespace Comet
{
    class Party
    {
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
    }
}
