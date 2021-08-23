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
        KeyboardState keyboardState;
        Player _player;
        World _world;
        EntityManager _entityManager;

        public InputController(Player player, World world, EntityManager entityManager)
        {
            _player = player;
            _world = world;
            _entityManager = entityManager;
        }

        public void ProcessControls(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            
            if (_player.State == CharacterState.Idle)
            {
                //Basic Movement commands
                if (keyboardState.IsKeyDown(Keys.S))
                    _player.Commands[0] = new MoveCommand(Direction.South, _world);
                else if (keyboardState.IsKeyDown(Keys.W))
                    _player.Commands[0] = new MoveCommand(Direction.North, _world);
                else if (keyboardState.IsKeyDown(Keys.A))
                    _player.Commands[0] = new MoveCommand(Direction.West, _world);
                else if (keyboardState.IsKeyDown(Keys.D))
                    _player.Commands[0] = new MoveCommand(Direction.East, _world);

                if (keyboardState.IsKeyDown(Keys.E))
                {
                    Character targetChar = null;
                    foreach (Character character in _entityManager.GetEntitiesOfType<Character>())
                    {
                        if (character is RedGirl)
                            targetChar = character;
                    }

                    _player.Commands[0] = new InteractCommand(targetChar);
                }
            }
        }
    }
}
