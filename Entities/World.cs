using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Entities
{
    public class World : IGameEntity
    {
        private readonly Cell[,] _world;
        Texture2D _worldTexture;

        public World(Cell[,] world, Texture2D texture)
        {
            _world = world;
            _worldTexture = texture;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_worldTexture, Vector2.Zero, new Rectangle(0, 0, 320, 320), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
