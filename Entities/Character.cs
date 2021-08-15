using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Commands;
using RedRidingHood.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Entities
{
    public class Character : IGameEntity
    {
        private const float MOVE_SPEED = 0.6f;

        Sprite _sprite;
        Vector2 _offset = new Vector2(0, -32);
        float _timeElapsed;


        public Location Location { get; set; }
        public Location TargetLocation { get; set; }
        public Location StartLocation { get; set; }
        public Vector2 Position { get; set; }
        public CharacterState State { get; set; }
        public Direction Direction { get; set; }
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, 32, 64);
        public float Depth => Location.Floor * 0.1f + Location.Row * 0.01f + 0.1f;
        public ICommand[] Commands { get; } = new ICommand[1];
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
            switch (State)
            {
                case CharacterState.Idle:
                    _timeElapsed = 0;
                    if (Commands[0] != null)
                    {
                        foreach (ICommand command in Commands)
                            command.Run(this);
                    }
                    Commands[0] = null;
                    break;

                case CharacterState.Moving:
                    float time = _timeElapsed / MOVE_SPEED;

                    if (_timeElapsed >= MOVE_SPEED)
                    {
                        Position = TargetLocation;
                        Location = TargetLocation;
                        State = CharacterState.Idle;
                    }
                    else
                    {
                        Position = Vector2.Lerp(StartLocation, TargetLocation, MathHelper.Clamp(time, 0, 1));
                        _timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    break;
            }
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
