using RedRidingHood.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Commands
{
    public class InteractCommand : ICommand
    {
        private Character _target;

        public InteractCommand(Character target)
        {
            _target = target;
        }
        public void Run(Character character)
        {
            if (IsInFrontOf(character))
            {
                _target.State = CharacterState.Dialogue;
                character.State = CharacterState.Dialogue;
            }
        }

        bool IsInFrontOf(Character character)
        {
            if (character.Direction == Direction.North && _target.Location == new Location(character.Location.Row - 1, character.Location.Column, character.Location.Floor))
            {
                _target.Direction = Direction.South;
                return true;
            }

            if (character.Direction == Direction.South && _target.Location == new Location(character.Location.Row + 1, character.Location.Column, character.Location.Floor))
            {
                _target.Direction = Direction.North;
                return true;
            }

            if (character.Direction == Direction.East && _target.Location == new Location(character.Location.Row, character.Location.Column + 1, character.Location.Floor))
            {
                _target.Direction = Direction.West;
                return true;
            }

            if (character.Direction == Direction.West && _target.Location == new Location(character.Location.Row, character.Location.Column - 1, character.Location.Floor))
            {
                _target.Direction = Direction.East;
                return true;
            }

            return false;
        }
    }
}
