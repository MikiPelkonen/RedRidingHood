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

        public World(Cell[,] world)
        {
            _world = world;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Cell c in _world)
                c.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {

        }
    }
}
