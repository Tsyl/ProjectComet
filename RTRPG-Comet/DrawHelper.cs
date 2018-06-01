using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    /// <summary>
    /// Used to draw programmatic visuals.
    /// </summary>
    public static class DrawHelper
    {
        public static GraphicsDevice graphicsDevice { get; set; }

        /// <summary>
        /// Draw a rectangle.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        /// <param name="dimensions"></param>
        /// <param name="color"></param>
        public static void DrawRectangle(SpriteBatch spriteBatch, Vector2 position, int width, int height, Color color)
        {
            if (width <= 0)
                width = 1;
            if (height <= 0)
                height = 1;

            Texture2D rect = new Texture2D(graphicsDevice, width, height);

            Color[] rectData = new Color[width * height];
            for (int pixel = 0; pixel < rectData.Length; pixel++)
            {
                int rowIndex = pixel / width;

                if (rowIndex == 0 ||
                   pixel % width == 0 || pixel % width == width - 1 ||
                   rowIndex == height - 1)
                    rectData[pixel] = color;
            }
            rect.SetData(rectData);

            spriteBatch.Draw(rect, position, Color.White);
        }

        public static void DrawFillRectangle(SpriteBatch spriteBatch, Vector2 position, int width, int height, Color color)
        {
            if (width <= 0)
                width = 1;
            if (height <= 0)
                height = 1;

            Texture2D rect = new Texture2D(graphicsDevice, width, height);

            Color[] rectData = new Color[width * height];
            for (int pixel = 0; pixel < rectData.Length; pixel++)
            {
                rectData[pixel] = color;
            }
            rect.SetData(rectData);

            spriteBatch.Draw(rect, position, Color.White);
        }

        public static Vector2 GetCharacterPosition(bool isPlayer, int charNum, int xOffset, int yOffset)
        {
            Vector2 tempVector = new Vector2();
            tempVector.X = xOffset;
            if (!isPlayer)
                tempVector.X += graphicsDevice.Viewport.Width / 5 * 4;
            tempVector.Y = (graphicsDevice.Viewport.Height * charNum) / 5 + yOffset;

            return tempVector;
        }
    }
}
