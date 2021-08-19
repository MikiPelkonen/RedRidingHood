using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Entities
{
    public class World : IGameEntity
    {
        private readonly Cell[,,] _world;
        Texture2D _worldTexture;
        Texture2D _houseTexture;
        Player _player;

        public World(Cell[,,] world, Texture2D texture, Texture2D house, Player player)
        {
            _world = world;
            _worldTexture = texture;
            _houseTexture = house;
            _player = player;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            

            switch (_player.Location.Floor)
            {
                case 0:
                    spriteBatch.Draw(_worldTexture, Vector2.Zero, new Rectangle(0, 0, 320, 352), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                    foreach (Cell c in _world)
                    {
                        if (c.Sprite != null)
                            c.Draw(spriteBatch);
                    }
                    break;
                case 1:
                    spriteBatch.Draw(_houseTexture, new Vector2(64, 128), new Rectangle(0, 0, 48, 80), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);



                    break;
            }

        }

        public void Update(GameTime gameTime)
        {
            Cell currentCell = GetCellByLocation(_player.Location);
            //if (currentCell.Type == CellType.Door)
            //{
            //    _player.Location = new Location(_player.Location.Row, _player.Location.Column, 1);
            //}

            if (currentCell.Type == CellType.DoorOut)
                _player.Location = new Location(_player.Location.Row, _player.Location.Column, 0);
        }

        public Cell GetCellByLocation(Location loc)
        {
            return _world[loc.Row, loc.Column, loc.Floor];
        }
    }
}
