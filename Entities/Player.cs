using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Commands;
using RedRidingHood.Core;
using RedRidingHood.Graphics;
using System.Collections.Generic;

namespace RedRidingHood.Entities
{
    public class Player : Character
    {
        private Texture2D _texture;
        private Sprite _shooting;
        private bool _shot;
        private float _timer;
        public Inventory Inventory { get; set; }
        public int MaxHp { get; } = 5;
        public int CurrentHp { get; set; } = 5;

        public Direction LastShotDir { get; set; }
        public Player(Location startLocation, Texture2D texture) : base(startLocation)
        {
            _texture = texture;
            _shooting = new Sprite(_texture, 144, 80, 16, 18);
            Inventory = new Inventory();
            _sprites = new Sprite[]
            {
                new Sprite(texture, 96, 0, 16, 18),
                new Sprite(texture, 96, 18, 16, 18),
                new Sprite(texture, 96, 54, 16, 18),
                new Sprite(texture, 96, 36, 16, 18)
            };

            _animations = new SpriteAnimation[] 
            {
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 80, 0, 16, 18),
                        new Sprite(texture, 96, 0, 16, 18),
                        new Sprite(texture, 112, 0, 16, 18),
                        new Sprite(texture, 128, 0, 16, 18),
                        new Sprite(texture, 128, 0, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 80, 18, 16, 18),
                        new Sprite(texture, 96, 18, 16, 18),
                        new Sprite(texture, 112, 18, 16, 18),
                        new Sprite(texture, 128, 18, 16, 18),
                        new Sprite(texture, 128, 18, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 80, 54, 16, 18),
                        new Sprite(texture, 96, 54, 16, 18),
                        new Sprite(texture, 112, 54, 16, 18),
                        new Sprite(texture, 128, 54, 16, 18),
                        new Sprite(texture, 128, 54, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 80, 36, 16, 18),
                        new Sprite(texture, 96, 36, 16, 18),
                        new Sprite(texture, 112, 36, 16, 18),
                        new Sprite(texture, 128, 36, 16, 18),
                        new Sprite(texture, 128, 36, 16, 18)
                    }
                    )
            };
        }

        public void Shoot(EntityManager entityManager, Direction direction)
        {
            Sprite bulletSprite = direction switch
            {
                Direction.North     =>  new Sprite(_texture, 180, 85, 4, 8),
                Direction.South     =>  new Sprite(_texture, 184, 86, 4, 8),
                Direction.East      =>  new Sprite(_texture, 184, 80, 8, 4),
                Direction.West      =>  new Sprite(_texture, 176, 80, 8, 4)
            };

            entityManager.Add(new Bullet(bulletSprite, entityManager, Position, Depth, direction));
            LastShotDir = direction;
            _shot = true;
            _timer = 0;
        }

        private void DrawShoot(SpriteBatch spriteBatch)
        {
            Sprite toDraw = LastShotDir switch
            {
                Direction.West  =>  _shooting,
                Direction.East  =>  new Sprite(_texture, 160, 80, 16, 18),
                Direction.North =>  new Sprite(_texture, 160, 97, 16, 18),
                Direction.South =>  new Sprite(_texture, 144, 97, 16, 19),
                _               =>  _shooting
            };


            toDraw.Draw(spriteBatch, Position + _offset, Depth);
        }

        public override void Update(GameTime gameTime)
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

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
            switch (State)
            {
                case CharacterState.Idle:
                    if (_shot)
                        DrawShoot(spriteBatch);
                    else
                        _sprites[CurrentDirection].Draw(spriteBatch, Position + _offset, Depth);
                    break;

                case CharacterState.Moving:
                    if (_shot)
                        DrawShoot(spriteBatch);
                    else
                        _animations[CurrentDirection].Draw(spriteBatch, Position + _offset, Depth);
                    break;

                case CharacterState.Dialogue:
                    _sprites[CurrentDirection].Draw(spriteBatch, Position + _offset, Depth);
                    break;
            }

            if(_timer >= 0.1f)
                _shot = false;

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

    }

    public class Inventory
    {
        public List<InventoryItem> Items { get; } = new List<InventoryItem>();

        public Inventory()
        {
            Items.Add(new Bread());
            Items.Add(new Honey());
            Items.Add(new Bread());
            Items.Add(new Honey());
            Items.Add(new Honey());
        }

        public void Add(InventoryItem item)
        {
            Items.Add(item);
        }
        public void Remove(InventoryItem item)
        {
            Items.Remove(item);
        }
    }

    public class InventoryItem
    {

    }

    public class Honey : InventoryItem
    {

    }

    public class Bread : InventoryItem
    {

    }
}
