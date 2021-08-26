using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.UI
{
    public class UserInterface
    {
        private Player _player;
        private Texture2D _uiTexture;
        private HpBar _hpBar;

        public UserInterface(Player player, Texture2D texture)
        {
            _player = player;
            _uiTexture = texture;
            _hpBar = new HpBar(texture);
        }

        public void Update(GameTime gameTime)
        {
            _hpBar.Update(gameTime, _player);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _hpBar.Draw(spriteBatch, gameTime);
        }
    }

    public abstract class UIElement
    {
        public Vector2 Position { get; protected set; }
        public abstract void Update(GameTime gameTime, Player player);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

    }

    public class HpBar : UIElement
    {
        private const int BLOCK_WIDTH = 9;
        Texture2D _hpBoard;
        private int _blockCount;
        

        public HpBar(Texture2D texture)
        {
            _hpBoard = texture;
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw HP Blocks

            spriteBatch.Draw(_hpBoard, new Vector2(Position.X + 26, Position.Y), new Rectangle(64, 0, _blockCount * BLOCK_WIDTH, 16), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0.99f);

            // Draw HP "container"
            spriteBatch.Draw(_hpBoard, Position, new Rectangle(0, 0, 60, 16), Color.White, 0f,Vector2.Zero, 2f, SpriteEffects.None, 1f);
        }

        public override void Update(GameTime gameTime, Player player)
        {
            Position = new Vector2(10, 310);
            _blockCount = player.CurrentHp;

        }
    }
}
