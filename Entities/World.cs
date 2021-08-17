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
            spriteBatch.Draw(_worldTexture, Vector2.Zero, new Rectangle(0, 0, 320, 352), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            foreach (Cell c in _world)
            {
                if (c.Type == CellType.Tree)
                    c.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public Cell GetCellByLocation(Location loc)
        {
            return _world[loc.Row, loc.Column];
        }
    }
}
