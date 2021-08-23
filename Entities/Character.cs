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
        protected const float MOVE_SPEED = 0.5f;

        protected int _currentDirection;

        protected float _timeElapsed;
        protected Vector2 _offset = new Vector2(0, -16);

        protected SpriteAnimation[] _animations;
        protected Sprite[] _sprites;
        
        public Location Location { get; set; }
        public Location TargetLocation { get; set; }
        public Location StartLocation { get; set; }
        public Vector2 Position { get; set; }
        public CharacterState State { get; set; }
        public Direction Direction { get; set; }
        protected int CurrentDirection => Direction switch
        {
            Direction.South => 0,
            Direction.North => 1,
            Direction.West => 2,
            Direction.East => 3
        };
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
        public float Depth => (float)(Location.Row * 0.01f);
        public ICommand[] Commands { get; } = new ICommand[1];
        public Character(Location startLocation)
        {
            Location = startLocation;
            Position = startLocation;
            State = CharacterState.Idle;
            Direction = Direction.South;
        }

        public virtual void Update(GameTime gameTime)
        {
            switch (State)
            {
                case CharacterState.Idle:
                    _timeElapsed = 0;

                    foreach (SpriteAnimation sa in _animations)
                        sa.Time = 0;

                    if (Commands[0] != null)
                    {
                        foreach (ICommand command in Commands)
                            command.Run(this);
                    }
                    Commands[0] = null;
                    break;

                case CharacterState.Moving:

                    MoveLerp(gameTime);
                    break;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            switch (State)
            {
                case CharacterState.Idle:
                    _sprites[CurrentDirection].Draw(spriteBatch, Position + _offset, Depth);
                    break;

                case CharacterState.Moving:
                    _animations[CurrentDirection].Draw(spriteBatch, Position + _offset, Depth);
                    break;
            }
        }

        protected void MoveLerp(GameTime gameTime)
        {
            float time = _timeElapsed / MOVE_SPEED;
            Location = TargetLocation;
            _animations[CurrentDirection].Update(gameTime);

            if (_timeElapsed >= MOVE_SPEED)
            {
                Position = TargetLocation;
                State = CharacterState.Idle;
            }
            else
            {
                Position = Vector2.Lerp(StartLocation, TargetLocation, MathHelper.Clamp(time, 0, 1));
                _timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }
    }

    public class Player : Character
    {
        public Player(Location startLocation, Texture2D texture) : base(startLocation)
        {
            _sprites = new Sprite[]
            {
                new Sprite(texture, 16, 67, 16, 18),
                new Sprite(texture, 16, 48, 16, 18),
                new Sprite(texture, 16, 86, 16, 18),
                new Sprite(texture, 16, 104, 16, 18)
            };

            _animations = new SpriteAnimation[] 
            {
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 67, 16, 18),
                        new Sprite(texture, 16, 67, 16, 18),
                        new Sprite(texture, 32, 67, 16, 18),
                        new Sprite(texture, 48, 67, 16, 18),
                        new Sprite(texture, 48, 67, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 48, 16, 18),
                        new Sprite(texture, 16, 48, 16, 18),
                        new Sprite(texture, 32, 48, 16, 18),
                        new Sprite(texture, 48, 48, 16, 18),
                        new Sprite(texture, 48, 48, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 86, 16, 18),
                        new Sprite(texture, 16, 86, 16, 18),
                        new Sprite(texture, 32, 86, 16, 18),
                        new Sprite(texture, 48, 86, 16, 18),
                        new Sprite(texture, 48, 86, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 104, 16, 18),
                        new Sprite(texture, 16, 104, 16, 18),
                        new Sprite(texture, 32, 104, 16, 18),
                        new Sprite(texture, 48, 104, 16, 18),
                        new Sprite(texture, 48, 104, 16, 18)
                    }
                    )
            };
        }
    }

    public class RedGirl : Character
    {
        private const float MOVE_INTERVAL = 0.008f;
        Random _random = new Random();

        public int PlayerFloor { get; set; }

        public event Action Move;
        public RedGirl(Location startLocation, Texture2D texture) : base(startLocation)
        {
            _sprites = new Sprite[]
            {
                new Sprite(texture, 16, 67, 16, 18),
                new Sprite(texture, 16, 48, 16, 18),
                new Sprite(texture, 16, 86, 16, 18),
                new Sprite(texture, 16, 104, 16, 18)
            };

            _animations = new SpriteAnimation[]
            {
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 67, 16, 18),
                        new Sprite(texture, 16, 67, 16, 18),
                        new Sprite(texture, 32, 67, 16, 18),
                        new Sprite(texture, 48, 67, 16, 18),
                        new Sprite(texture, 48, 67, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 48, 16, 18),
                        new Sprite(texture, 16, 48, 16, 18),
                        new Sprite(texture, 32, 48, 16, 18),
                        new Sprite(texture, 48, 48, 16, 18),
                        new Sprite(texture, 48, 48, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 86, 16, 18),
                        new Sprite(texture, 16, 86, 16, 18),
                        new Sprite(texture, 32, 86, 16, 18),
                        new Sprite(texture, 48, 86, 16, 18),
                        new Sprite(texture, 48, 86, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 104, 16, 18),
                        new Sprite(texture, 16, 104, 16, 18),
                        new Sprite(texture, 32, 104, 16, 18),
                        new Sprite(texture, 48, 104, 16, 18),
                        new Sprite(texture, 48, 104, 16, 18)
                    }
                    )
            };
        }

        public override void Update(GameTime gameTime)
        {
            switch (State)
            {
                case CharacterState.Idle:
                    if (_random.NextDouble() < MOVE_INTERVAL)
                    {
                        Move?.Invoke();
                    }

                    _timeElapsed = 0;

                    foreach (SpriteAnimation sa in _animations)
                        sa.Time = 0;

                    if (Commands[0] != null)
                    {
                        foreach (ICommand command in Commands)
                            command.Run(this);
                    }
                    Commands[0] = null;
                    break;

                case CharacterState.Moving:

                    MoveLerp(gameTime);
                    break;

            }
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Location.Floor == PlayerFloor)
            {
                switch (State)
                {
                    case CharacterState.Idle:
                        _sprites[CurrentDirection].Draw(spriteBatch, Position + _offset, Depth);
                        break;

                    case CharacterState.Moving:
                        _animations[CurrentDirection].Draw(spriteBatch, Position + _offset, Depth);
                        break;
                }
            }
        }
    }

    public enum CharacterState { Idle, Moving, Dead }
    public enum Direction { North, South, East, West }
}
