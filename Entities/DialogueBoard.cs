using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Entities
{
    public class DialogueBoard : IGameEntity
    {
        Player _player;
        RedGirl _redGirl;
        Texture2D _spriteSheet;
        DialogueType Type;
        SpriteFont _font;

        KeyboardState keyboardState, lastKeyboardState;

        int kue = 0;

        public DialogueBoard(Player player, RedGirl redGirl, Texture2D spriteSheet, SpriteFont font)
        {
            _font = font;
            _spriteSheet = spriteSheet;
            _player = player;
            _redGirl = redGirl;
            _redGirl.DialogueStart += OnRedGirlDialogueStart;
            Type = DialogueType.Empty;
            
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (Type)
            {
                case DialogueType.Empty:
                    kue = 0;
                    break;
                case DialogueType.RedGirlNormal:
                    string renderText = kue switch
                    {
                        0   =>  "Hello there Mr. Hunter!\nIt's a lovely day!",
                        1   =>  "Will you help me\nwith a simple task?",
                        2   =>  "My grandma is starving...",
                        3   =>  "Would you hunt for me?"
                    };

                    spriteBatch.Draw(_spriteSheet, new Rectangle((int)_redGirl.Position.X - 10, (int)_redGirl.Position.Y - 90, 200, 60), new Rectangle(0, 16, 16, 16), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.95f);
                    spriteBatch.DrawString(_font, renderText, new Vector2(_redGirl.Position.X - 6, _redGirl.Position.Y - 75), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    if (keyboardState.IsKeyDown(Keys.Space) && !lastKeyboardState.IsKeyDown(Keys.Space))
                    {
                        kue++;
                    }

                    if (kue == 4)
                    {
                        _player.State = CharacterState.Idle;
                        _redGirl.State = CharacterState.Idle;
                        Type = DialogueType.Empty;
                    }

                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            
        }

        public void OnRedGirlDialogueStart()
        {
            Type = DialogueType.RedGirlNormal;
        }
    }

    public enum DialogueType { Empty, RedGirlNormal }
}
