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
        Texture2D _speechBubble;
        DialogueType Type;
        SpriteFont _font;
        Color _color = new Color(27, 3, 38);

        KeyboardState keyboardState, lastKeyboardState;

        int kue = 0;

        public DialogueBoard(Player player, RedGirl redGirl, Texture2D speechBubble, SpriteFont font)
        {
            _font = font;
            _speechBubble = speechBubble;
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
                        0   =>  "Hello there Mister!\nSuch a lovely day...",
                        1   =>  "Will you help me\nwith a simple task?",
                        2   =>  "My grandma is\n starving...",
                        3   =>  "Would you go and\n hunt for me?",
                        _   =>  ""
                    };

                    if (kue < 4)
                    {
                        spriteBatch.Draw(_speechBubble, new Vector2(_redGirl.Position.X + 20, _redGirl.Position.Y - 80), new Rectangle(0, 0, 64, 32), Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0.95f);
                        spriteBatch.DrawString(_font, renderText, new Vector2(_redGirl.Position.X + 27, _redGirl.Position.Y - 60), _color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    }
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
