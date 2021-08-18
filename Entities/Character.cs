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
        Vector2 _offset = new Vector2(0, -16);

        protected SpriteAnimation[] _animations;
        protected Sprite[] _sprites;
        
        public Location Location { get; set; }
        public Location TargetLocation { get; set; }
        public Location StartLocation { get; set; }
        public Vector2 Position { get; set; }
        public CharacterState State { get; set; }
        public CharacterType Type { get; }
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
        public Character(Location startLocation, CharacterType ctype)
        {
            Location = startLocation;
            Position = startLocation;
            State = CharacterState.Idle;
            Direction = Direction.South;
            Type = ctype;
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
                    
                    float time = _timeElapsed / MOVE_SPEED;

                    _animations[CurrentDirection].Update(gameTime);

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

    public class Player : Character
    {
        public Player(Location startLocation, Texture2D texture) : base(startLocation, CharacterType.Player)
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
                        new Sprite(texture, 0, 16, 16, 16),
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 48, 16, 18),
                        new Sprite(texture, 16, 48, 16, 18),
                        new Sprite(texture, 32, 48, 16, 18),
                        new Sprite(texture, 48, 48, 16, 18),
                        new Sprite(texture, 0, 16, 16, 16),
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 86, 16, 18),
                        new Sprite(texture, 16, 86, 16, 18),
                        new Sprite(texture, 32, 86, 16, 18),
                        new Sprite(texture, 48, 86, 16, 18),
                        new Sprite(texture, 0, 16, 16, 16),
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 104, 16, 18),
                        new Sprite(texture, 16, 104, 16, 18),
                        new Sprite(texture, 32, 104, 16, 18),
                        new Sprite(texture, 48, 104, 16, 18),
                        new Sprite(texture, 0, 104, 16, 16),
                    }
                    )
            };
        }
    }

    public class RedGirl : Character
    {
        private const float MOVE_INTERVAL = 0.008f;
        Random _random = new Random();
        public RedGirl(Location startLocation, Texture2D texture) : base(startLocation, CharacterType.RedGirl)
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
                        new Sprite(texture, 0, 16, 16, 16),
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 48, 16, 18),
                        new Sprite(texture, 16, 48, 16, 18),
                        new Sprite(texture, 32, 48, 16, 18),
                        new Sprite(texture, 48, 48, 16, 18),
                        new Sprite(texture, 0, 16, 16, 16),
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 86, 16, 18),
                        new Sprite(texture, 16, 86, 16, 18),
                        new Sprite(texture, 32, 86, 16, 18),
                        new Sprite(texture, 48, 86, 16, 18),
                        new Sprite(texture, 0, 16, 16, 16),
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 0, 104, 16, 18),
                        new Sprite(texture, 16, 104, 16, 18),
                        new Sprite(texture, 32, 104, 16, 18),
                        new Sprite(texture, 48, 104, 16, 18),
                        new Sprite(texture, 0, 104, 16, 16),
                    }
                    )
            };
        }

        public event Action Move;
        public CharacterType Type { get; } = CharacterType.RedGirl;

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

                    float time = _timeElapsed / MOVE_SPEED;

                    _animations[CurrentDirection].Update(gameTime);

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
    }

    public enum CharacterState { Idle, Moving, Dead }
    public enum Direction { North, South, East, West }
    public enum CharacterType { Player, RedGirl, Grandma }
}
