using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RedRidingHood.Commands;
using RedRidingHood.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Core
{
    public class InputController
    {
        KeyboardState keyboardState, lastKeyboardState;
        Player _player;
        World _world;

        public InputController(Player player, World world)
        {
            _player = player;
            _world = world;
        }

        public void ProcessControls(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            switch (_player.State)
            {
                case CharacterState.Idle:
                    if (keyboardState.IsKeyDown(Keys.Down))
                    {
                        _player.Commands[0] = new MoveCommand(Direction.South);
                    }
                    break;
                case CharacterState.Moving:
                    break;
            }
            
        }
    }
}
