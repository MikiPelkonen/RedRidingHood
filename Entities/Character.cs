using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Entities
{
    public class Character : IGameEntity
    {
        Sprite _sprite;
        Vector2 _offset = new Vector2(0, -32);
        public Location Location { get; set; }
        public Vector2 Position { get; set; }
        public CharacterState State { get; set; }
        public Direction Direction { get; set; }
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, 32, 64);
        public float Depth => Location.Floor * 0.1f + Location.Row * 0.01f + 0.01f;
        public Character(Location startLocation, Sprite sprite)
        {
            _sprite = sprite;
            Location = startLocation;
            Position = startLocation;
            State = CharacterState.Idle;
            Direction = Direction.South;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, Position + _offset, Depth);
        }
    }

    public class Player : Character
    {
        public Player(Location startLocation, Sprite sprite) : base(startLocation, sprite)
        {

        }
    }

    public enum CharacterState { Idle, Moving, Dead }
    public enum Direction { North, South, East, West }
}
