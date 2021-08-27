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
        public Rectangle Rectangle => new Rectangle((int)Position.X, (int)Position.Y - 16, 32, 32);
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

                case CharacterState.Dialogue:
                    
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

                case CharacterState.Dialogue:
                    _sprites[CurrentDirection].Draw(spriteBatch, Position + _offset, Depth);
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

    public class Furry : Character
    {
        private const float MOVE_INTERVAL = 0.008f;
        Random _random = new Random();
        public int PlayerFloor { get; set; }

        public event Action Move;
        public Furry(Location startLocation, Texture2D texture) : base(startLocation)
        {
            _sprites = new Sprite[]
            {
                new Sprite(texture, 160, 0, 16, 18),
                new Sprite(texture, 160, 18, 16, 18),
                new Sprite(texture, 160, 54, 16, 18),
                new Sprite(texture, 160, 36, 16, 18)
            };

            _animations = new SpriteAnimation[]
            {
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 144, 0, 16, 18),
                        new Sprite(texture, 160, 0, 16, 18),
                        new Sprite(texture, 176, 0, 16, 18),
                        new Sprite(texture, 192, 0, 16, 18),
                        new Sprite(texture, 192, 0, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 144, 18, 16, 18),
                        new Sprite(texture, 160, 18, 16, 18),
                        new Sprite(texture, 176, 18, 16, 18),
                        new Sprite(texture, 192, 18, 16, 18),
                        new Sprite(texture, 192, 18, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 144, 54, 16, 18),
                        new Sprite(texture, 160, 54, 16, 18),
                        new Sprite(texture, 176, 54, 16, 18),
                        new Sprite(texture, 192, 54, 16, 18),
                        new Sprite(texture, 192, 54, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 144, 36, 16, 18),
                        new Sprite(texture, 160, 36, 16, 18),
                        new Sprite(texture, 176, 36, 16, 18),
                        new Sprite(texture, 192, 36, 16, 18),
                        new Sprite(texture, 192, 36, 16, 18)
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

                case CharacterState.Dialogue:

                    
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

                    case CharacterState.Dialogue:
                        _sprites[CurrentDirection].Draw(spriteBatch, Position + _offset, Depth);
                        break;
                }
            }
        }
    }

    public class RedGirl : Character
    {
        private const float MOVE_INTERVAL = 0.008f;
        Random _random = new Random();

        public int PlayerFloor { get; set; }

        public event Action Move;
        public event Action DialogueOver;
        public event Action DialogueStart;
        public RedGirl(Location startLocation, Texture2D texture) : base(startLocation)
        {
            _sprites = new Sprite[]
            {
                new Sprite(texture, 96, 80, 16, 18),
                new Sprite(texture, 96, 98, 16, 18),
                new Sprite(texture, 96, 134, 16, 18),
                new Sprite(texture, 96, 116, 16, 18)
            };

            _animations = new SpriteAnimation[]
            {
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 80, 80, 16, 18),
                        new Sprite(texture, 96, 80, 16, 18),
                        new Sprite(texture, 112, 80, 16, 18),
                        new Sprite(texture, 128, 80, 16, 18),
                        new Sprite(texture, 128, 80, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 80, 98, 16, 18),
                        new Sprite(texture, 96, 98, 16, 18),
                        new Sprite(texture, 112, 98, 16, 18),
                        new Sprite(texture, 128, 98, 16, 18),
                        new Sprite(texture, 128, 98, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 80, 134, 16, 18),
                        new Sprite(texture, 96, 134, 16, 18),
                        new Sprite(texture, 112, 134, 16, 18),
                        new Sprite(texture, 128, 134, 16, 18),
                        new Sprite(texture, 128, 134, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 80, 116, 16, 18),
                        new Sprite(texture, 96, 116, 16, 18),
                        new Sprite(texture, 112, 116, 16, 18),
                        new Sprite(texture, 128, 116, 16, 18),
                        new Sprite(texture, 128, 116, 16, 18)
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

                case CharacterState.Dialogue:

                    DialogueStart?.Invoke();
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

                    case CharacterState.Dialogue:
                        _sprites[CurrentDirection].Draw(spriteBatch, Position + _offset, Depth);
                        break;
                }
            }
        }
    }

    public enum CharacterState { Idle, Moving, Dialogue, Dead }
    public enum Direction { North, South, East, West }
}
