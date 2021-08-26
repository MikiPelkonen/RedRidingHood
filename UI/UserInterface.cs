using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RedRidingHood.Core;
using RedRidingHood.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.UI
{
    public class UserInterface
    {
        private Player _player;
        private InputController _inputController;
        private HpBar _hpBar;
        private Backbag _backBag;


        private bool _toggleBackbag;
        private KeyboardState keyboardState, lastKeyboardState;

        public UserInterface(Player player, InputController inputController, Texture2D hptext, Texture2D backbagtext)
        {
            _player = player;
            _inputController = inputController;
            _inputController.ToggleBackbag += OnToggleBackbag;
            _hpBar = new HpBar(hptext);
            _backBag = new Backbag(backbagtext);
        }

        public void Update(GameTime gameTime)
        {
            _hpBar.Update(gameTime, _player);
            _backBag.Update(gameTime, _player);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _hpBar.Draw(spriteBatch, gameTime);
            

            if (_toggleBackbag) _backBag.Draw(spriteBatch, gameTime);
        }

        private void OnToggleBackbag()
        {
            _toggleBackbag = _toggleBackbag == false ? true : false;
        }
    }

    public abstract class UIElement
    {
        public Vector2 Position { get; }
        public abstract void Update(GameTime gameTime, Player player);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public UIElement(Vector2 position)
        {
            Position = position;
        }

    }

    public class Backbag : UIElement
    {
        Texture2D _backbagSheet;
        List<InventoryItem> _items;

        public Backbag(Texture2D texture) : base(new Vector2(470, 230))
        {
            _backbagSheet = texture;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            // Draw Backbag
            spriteBatch.Draw(_backbagSheet, Position, new Rectangle(0, 0, 80, 64), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0.95f);

            // Draw Items

            for (int i = 0; i < _items.Count; i++)
            {
                Rectangle sourceRect;

                if (_items[i] is Bread)
                    sourceRect = new Rectangle(80, 16, 16, 16);
                else
                    sourceRect = new Rectangle(80, 0, 16, 16);

                spriteBatch.Draw(_backbagSheet, new Vector2(Position.X + 26 + (38 * i), Position.Y + 28), sourceRect, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);

            }



        }

        public override void Update(GameTime gameTime, Player player)
        {
            _items = player.Inventory.Items;
        }
    }

    public class HpBar : UIElement
    {
        private const int BLOCK_WIDTH = 9;
        private const int BLOCK_OFFSET = 26;
        Texture2D _hpBoard;
        private int _blockCount;
        

        public HpBar(Texture2D texture) : base(new Vector2(10, 310))
        {
            _hpBoard = texture;
        }
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw HP Blocks

            spriteBatch.Draw(_hpBoard, new Vector2(Position.X + BLOCK_OFFSET, Position.Y), new Rectangle(64, 0, _blockCount * BLOCK_WIDTH, 16), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0.99f);

            // Draw HP "container"
            spriteBatch.Draw(_hpBoard, Position, new Rectangle(0, 0, 60, 16), Color.White, 0f,Vector2.Zero, 2f, SpriteEffects.None, 1f);
        }

        public override void Update(GameTime gameTime, Player player)
        {
            _blockCount = player.CurrentHp;
        }
    }
}
