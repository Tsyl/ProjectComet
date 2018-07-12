using Microsoft.Xna.Framework;

namespace Comet
{
    class EffectManager
    {
        public Effect[] effects = new Effect[50];
        public Character owner;

        public EffectManager(Character _owner)
        {
            owner = _owner;
        }

        public void Update(GameTime gameTime)
        {
            // Effect Handling
            for (int effectNum = 0; effectNum < effects.Length; effectNum++)
            {
                if (effects[effectNum] == null)
                    break;

                effects[effectNum].Update(gameTime);

                if (effects[effectNum].host != owner)
                    effects[effectNum] = null;
            }
        }

        public void Add()
        {

        }

        public void Remove()
        {

        }
    }
}
