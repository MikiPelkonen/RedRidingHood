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
            
            if (_player.State == CharacterState.Idle)
            {
                //Basic Movement commands
                if (keyboardState.IsKeyDown(Keys.S))
                    _player.Commands[0] = new MoveCommand(Direction.South);
                else if (keyboardState.IsKeyDown(Keys.W))
                    _player.Commands[0] = new MoveCommand(Direction.North);
                else if (keyboardState.IsKeyDown(Keys.A))
                    _player.Commands[0] = new MoveCommand(Direction.West);
                else if (keyboardState.IsKeyDown(Keys.D))
                    _player.Commands[0] = new MoveCommand(Direction.East);
            }
        }
    }
}
