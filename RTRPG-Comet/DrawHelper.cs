using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Comet
{
    /// <summary>
    /// Used to draw programmatic visuals.
    /// </summary>
    public class DrawHelper
    {
        GraphicsDevice graphicsDevice;
        Texture2D baseTexture;
        private static readonly DrawHelper instance = new DrawHelper();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DrawHelper GetInstance()
        {
            return instance;
        }

        private DrawHelper()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_graphicsDevice"></param>
        public void Initialize(GraphicsDevice _graphicsDevice)
        {
            graphicsDevice = _graphicsDevice;
            baseTexture = new Texture2D(graphicsDevice, 1, 1);
            baseTexture.SetData(new[] { Color.White });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="color"></param>
        public void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(baseTexture, new Rectangle(
                                            (int)start.X, 
                                            (int)start.Y, 
                                            (int)edge.Length(), 
                                            1), null, color, angle, Vector2.Zero, SpriteEffects.None, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="thickness"></param>
        /// <param name="color"></param>
        public void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end, int thickness, Color color)
        {
            Vector2 edge = end - start;
            Vector2 scale = new Vector2(Vector2.Distance(start, end), thickness);
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            
            spriteBatch.Draw(baseTexture, start, null, color, angle, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Draw empty rectangle.
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        public void DrawRectangle(SpriteBatch spriteBatch, Vector2 position, int width, int height, Color color)
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

            Vector2 p1 = new Vector2(position.X, position.Y);
            Vector2 p2 = new Vector2(position.X + width, position.Y);
            Vector2 p3 = new Vector2(position.X, position.Y + height);
            Vector2 p4 = new Vector2(position.X + width, position.Y + height);

            DrawLine(spriteBatch, p1, p2, color);
            DrawLine(spriteBatch, p1, p3, color);
            DrawLine(spriteBatch, p4, p2, color);
            DrawLine(spriteBatch, p4, p3, color);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="thickness"></param>
        /// <param name="color"></param>
        public void DrawRectangle(SpriteBatch spriteBatch, Vector2 position, int width, int height, int thickness, Color color)
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

            Vector2 p1 = new Vector2(position.X, position.Y);
            Vector2 p2 = new Vector2(position.X + width, position.Y);
            Vector2 p3 = new Vector2(position.X, position.Y + height);
            Vector2 p4 = new Vector2(position.X + width, position.Y + height);

            DrawLine(spriteBatch, p1, p2, thickness, color);
            DrawLine(spriteBatch, p1, p3, thickness, color);
            DrawLine(spriteBatch, p4, p2, thickness, color);
            DrawLine(spriteBatch, p4, p3, thickness, color);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="color"></param>
        public void DrawFillRectangle(SpriteBatch spriteBatch, Vector2 position, int width, int height, Color color)
        {
            if (width <= 0)
                width = 1;
            if (height <= 0)
                height = 1;

            Vector2 start = new Vector2(position.X, position.Y);
            Vector2 end = new Vector2(position.X + width, position.Y);

            for (int y = 0; y < height; y++)
            {
                start.Y = position.Y + y;
                end.Y = position.Y + y;

                DrawLine(spriteBatch, start, end, color);
            }
        }

        /// <summary>
        /// Split the screen vertically and return the segments.
        /// </summary>
        /// <param name="segments"></param>
        /// <param name="xOffset"></param>
        /// <param name="yOffset"></param>
        /// <returns></returns>
        public Rectangle[] GetVerticalScreenSegments(int segments, int xOffset, int yOffset)
        {
            if (segments < 2)
                return null;

            Rectangle[] rects = new Rectangle[segments];

            for(int currentSegment = 0; currentSegment < segments; currentSegment++)
            {
                int width = graphicsDevice.Viewport.Width - xOffset;
                int height = graphicsDevice.Viewport.Height / segments;
                int x = 0;
                int y = height * currentSegment;

                rects[currentSegment] = new Rectangle(x + xOffset, y + yOffset, width, height);
            }
            
            return rects;
        }
    }
}
