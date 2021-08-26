using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Entities
{
    public class Bullet : IGameEntity
    {
        private Sprite _bulletSprite;
        private EntityManager _entityManager;
        private float _depth;
        public Vector2 Position { get; private set; }

        public Bullet(Sprite sprite, EntityManager entityManager, Vector2 spawnPos, float depth)
        {
            _bulletSprite = sprite;
            _entityManager = entityManager;
            Position = spawnPos;
            _depth = depth;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _bulletSprite.Draw(spriteBatch, Position, _depth, 1f);
        }

        public void Update(GameTime gameTime)
        {
            float posX = Position.X - 300 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            Position = new Vector2(MathF.Round(posX), Position.Y);
        }
    }
}
