using RedRidingHood.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Commands
{
    public class MoveCommand : ICommand
    {
        public Direction Direction { get; }
        public MoveCommand(Direction direction)
        {
            Direction = direction;
        }
        public void Run(Character character)
        {
            character.StartLocation = character.Location;
            character.TargetLocation = Direction switch
            {
                Direction.North =>  new Location(character.Location.Row - 1, character.Location.Column, character.Location.Floor),
                Direction.South =>  new Location(character.Location.Row + 1, character.Location.Column, character.Location.Floor),
                Direction.East =>  new Location(character.Location.Row, character.Location.Column + 1, character.Location.Floor),
                Direction.West =>  new Location(character.Location.Row, character.Location.Column - 1, character.Location.Floor)
            };


            character.Direction = Direction;
            character.State = CharacterState.Moving;
        }
    }
}
