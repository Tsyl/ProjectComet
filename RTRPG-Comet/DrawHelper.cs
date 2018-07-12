using System;
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

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            Texture2D rect = new Texture2D(graphicsDevice, 1, 1);
            rect.SetData(new Color[] { color });

            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(rect, new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1), null, Color.White, angle, Vector2.Zero, SpriteEffects.None, 0);
        }

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

            /*Vector2 UpperLeftPoint = position;
            Vector2 UpperRightPoint = new Vector2(position.X + width, position.Y);
            Vector2 LowerLeftPoint = new Vector2(position.X, position.Y + height);
            Vector2 LowerRightPoint = new Vector2(position.X + width, position.Y + height);
            DrawLine(spriteBatch, UpperLeftPoint, UpperRightPoint, color);
            DrawLine(spriteBatch, UpperLeftPoint, LowerLeftPoint, color);
            DrawLine(spriteBatch, UpperRightPoint, LowerRightPoint, color);
            DrawLine(spriteBatch, LowerLeftPoint, LowerRightPoint, color);*/

            Texture2D rect = new Texture2D(graphicsDevice, width, height);

            Color[] rectData = new Color[width * height];
            for (int pixel = 0; pixel < rectData.Length; pixel++)
            {
                int rowIndex = pixel / width;

                rectData[pixel] = Color.TransparentBlack;

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
