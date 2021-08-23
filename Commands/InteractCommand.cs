using RedRidingHood.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Commands
{
    public class InteractCommand : ICommand
    {
        private Character _target;
        private Location _scoutLocation;

        public InteractCommand(Character target, Location scoutLocation)
        {
            _target = target;
            _scoutLocation = scoutLocation;
        }
        public void Run(Character character)
        {
            _target.State = CharacterState.Dialogue;
            character.State = CharacterState.Dialogue;
            ChangeTargetDir(character);

        }

        void ChangeTargetDir(Character character)
        {
            if (character.Direction == Direction.North && _target.Location == _scoutLocation)
            {
                _target.Direction = Direction.South;
            }

            if (character.Direction == Direction.South && _target.Location == _scoutLocation)
            {
                _target.Direction = Direction.North;
            }

            if (character.Direction == Direction.East && _target.Location == _scoutLocation)
            {
                _target.Direction = Direction.West;
            }

            if (character.Direction == Direction.West && _target.Location == _scoutLocation)
            {
                _target.Direction = Direction.East;
            }
        }
    }
}
