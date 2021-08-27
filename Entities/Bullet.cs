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
        private const int BULLET_SPEED = 300;
        private Sprite _bulletSprite;
        private EntityManager _entityManager;
        private float _depth;
        private Direction _direction;
        private Vector2 _spawnPos;
        public Vector2 Position { get; private set; }

        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, 8, 4);

        public Bullet(Sprite sprite, EntityManager entityManager, Vector2 spawnPos, float depth, Direction direction)
        {
            _bulletSprite = sprite;
            _entityManager = entityManager;
            _spawnPos = spawnPos;
            Position = new Vector2(spawnPos.X + 8, spawnPos.Y + 8);
            _depth = depth;
            _direction = direction;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _bulletSprite.Draw(spriteBatch, Position, _depth, 1f);
        }

        public void Update(GameTime gameTime)
        {

            float posX = _direction switch
            {
                Direction.East  =>  Position.X + BULLET_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds,
                Direction.West  =>  Position.X - BULLET_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds,
                _               =>  Position.X
            };

            float posY = _direction switch
            {
                Direction.North =>  Position.Y - BULLET_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds,
                Direction.South =>  Position.Y + BULLET_SPEED * (float)gameTime.ElapsedGameTime.TotalSeconds,
                _               =>  Position.Y
            };

            Position = new Vector2(MathF.Round(posX), MathF.Round(posY));
            CheckCollisions();

            if (Vector2.Distance(Position, _spawnPos) > 300)
            {
                _entityManager.Remove(this);
            }
        }

        private void CheckCollisions()
        {
            foreach (Furry furry in _entityManager.GetEntitiesOfType<Furry>())
            {
                Rectangle box = furry.Rectangle;
                box.Inflate(-10, -3);
                if (Rectangle.Intersects(box))
                {
                    _entityManager.Remove(this);
                }
            }
        }
    }
}
