using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedRidingHood.Entities
{
    public class World : IGameEntity
    {
        private readonly Cell[,,] _world;
        Texture2D _worldTexture;
        Texture2D _houseTexture;
        Player _player;
        EntityManager _entityManager;
        public Location PlayerLocation { get; private set; }

        public List<Location> CharLocations { get; private set; } = new List<Location>();

        public World(Cell[,,] world, Texture2D texture, Texture2D house, Player player, EntityManager entityManager)
        {
            _world = world;
            _entityManager = entityManager;
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
                        if (c.Sprite != null && c.Type != CellType.Blanket)
                            c.Draw(spriteBatch);
                    }
                    break;
                case 1:
                    spriteBatch.Draw(_houseTexture, new Vector2(64, 128), new Rectangle(0, 0, 48, 80), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
                    _world[5, 4, 1].Draw(spriteBatch);
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            TrackCharLocations();
        }

        public void TrackCharLocations()
        {
            List<Location> newLocations = new List<Location>();
            foreach (Character character in _entityManager.GetEntitiesOfType<Character>())
            {
                newLocations.Add(character.Location);
            }

            if (!Enumerable.SequenceEqual(CharLocations, newLocations))
            {
                CharLocations = newLocations;
            }
        }

        public Cell GetCellByLocation(Location loc)
        {
            return _world[loc.Row, loc.Column, loc.Floor];
        }
    }
}
