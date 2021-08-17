using RedRidingHood.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Commands
{
    public class MoveCommand : ICommand
    {
        public Direction Direction { get; }
        public World World { get; }
        public MoveCommand(Direction direction, World world)
        {
            Direction = direction;
            World = world;
        }
        public void Run(Character character)
        {
            character.StartLocation = character.Location;
            Location locationToCome = Direction switch
            {
                Direction.North => new Location(character.Location.Row - 1, character.Location.Column, character.Location.Floor),
                Direction.South => new Location(character.Location.Row + 1, character.Location.Column, character.Location.Floor),
                Direction.East => new Location(character.Location.Row, character.Location.Column + 1, character.Location.Floor),
                Direction.West => new Location(character.Location.Row, character.Location.Column - 1, character.Location.Floor)
            };

            character.Direction = Direction;
            if (IsLegalCell(locationToCome, character.StartLocation, Direction))
            {
                character.TargetLocation = locationToCome;
                
                character.State = CharacterState.Moving;
            }
        }

        bool IsLegalCell(Location loc, Location charLocation, Direction direction)
        {
            Cell targetCell = World.GetCellByLocation(loc);
            Cell currentCell = World.GetCellByLocation(charLocation);

            if (targetCell.Type == CellType.Tree && direction == Direction.North) return false;
            if (currentCell.Type == CellType.Tree && direction == Direction.South) return false;

            return true;
        }
    }
}
