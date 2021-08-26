using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using RedRidingHood.Commands;
using RedRidingHood.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;

namespace RedRidingHood.Core
{
    public class InputController
    {
        KeyboardState keyboardState, lastKeyboardState;
        Player _player;
        World _world;
        EntityManager _entityManager;

        public event Action ToggleBackbag;
        public event Action Shoot;

        public InputController(Player player, World world, EntityManager entityManager)
        {
            _player = player;
            _world = world;
            _entityManager = entityManager;
        }

        public void ProcessControls(GameTime gameTime)
        {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            // User Interface commands
            if (keyboardState.IsKeyDown(Keys.B) && !lastKeyboardState.IsKeyDown(Keys.B))
            {
                ToggleBackbag?.Invoke();
            }

            if (keyboardState.IsKeyDown(Keys.Left) && !lastKeyboardState.IsKeyDown(Keys.Left))
            {
                _player.Shoot(_entityManager);
                Shoot?.Invoke();
            }

            if (_player.State == CharacterState.Idle)
            {
                // Basic Movement commands
                if (keyboardState.IsKeyDown(Keys.S))
                    _player.Commands[0] = new MoveCommand(Direction.South, _world);
                else if (keyboardState.IsKeyDown(Keys.W))
                    _player.Commands[0] = new MoveCommand(Direction.North, _world);
                else if (keyboardState.IsKeyDown(Keys.A))
                    _player.Commands[0] = new MoveCommand(Direction.West, _world);
                else if (keyboardState.IsKeyDown(Keys.D))
                    _player.Commands[0] = new MoveCommand(Direction.East, _world);


                // Interact command
                if (keyboardState.IsKeyDown(Keys.E))
                {
                    Location scoutLocation = _player.Location + _player.Direction switch
                    {
                        Direction.North => new Location(-1, 0, 0),
                        Direction.South => new Location(1, 0, 0),
                        Direction.East  => new Location(0, 1, 0),
                        Direction.West  => new Location(0, -1, 0)
                    };
                    Character target = _entityManager.CharacterByLocation(scoutLocation);

                    if (target != null && !(target is Furry))
                    {
                        _player.Commands[0] = new InteractCommand(target, scoutLocation);
                    }
                }
            }
        }
    }
}
