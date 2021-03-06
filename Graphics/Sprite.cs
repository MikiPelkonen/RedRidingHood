using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Graphics
{
    public class Sprite
    {
        public Texture2D Texture { get; private set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Color TintColor { get; set; } = Color.White;

        public Sprite(Texture2D texture, int x, int y, int width, int height)
        {
            Texture = texture;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float depth)
        {
            spriteBatch.Draw(Texture, position, new Rectangle(X, Y, Width, Height), TintColor, 0f, Vector2.Zero, 2f, SpriteEffects.None, depth);
        }
    }
}
