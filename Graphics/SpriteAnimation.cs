using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Graphics
{
    public class SpriteAnimation
    {
        public Sprite[] _sprites;

        public int Time { get; set; }

        int _currentFrame;

        public SpriteAnimation(Sprite[] sprites)
        {
            _sprites = sprites;
        }

        public void Update(GameTime gameTime)
        {
            double timePerFrame = 125;

            Time += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            _currentFrame = (int)(Time / timePerFrame);

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, float depth)
        {
            _sprites[_currentFrame].Draw(spriteBatch, position, depth);
        }
    }
}
