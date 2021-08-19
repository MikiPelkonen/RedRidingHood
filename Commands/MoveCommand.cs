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
            if (IsLegalMove(locationToCome, character.StartLocation, Direction, character.Type))
            {
                

                if (World.GetCellByLocation(locationToCome).Type == CellType.Door)
                {
                    character.TargetLocation = new Location(locationToCome.Row, locationToCome.Column, 1);
                }
                else if (World.GetCellByLocation(locationToCome).Type == CellType.DoorOut)
                {
                    character.TargetLocation = new Location(locationToCome.Row, locationToCome.Column, 0);
                }
                else
                {
                    character.TargetLocation = locationToCome;
                }
                character.State = CharacterState.Moving;
            }
        }

        bool IsLegalMove(Location loc, Location charLocation, Direction direction, CharacterType ctype)
        {
            if (IsInsideMap(loc, ctype) && IsLegalCell(loc, charLocation, direction))
                return true;
            return false;
        }


        bool IsInsideMap(Location loc, CharacterType ctype) 
        {
            if (ctype == CharacterType.RedGirl)
                return loc.Row >= 5 && loc.Row <= 11 && loc.Column >= 1 && loc.Column <= 5;
            else
                return loc.Row >= 1 && loc.Row <= 21 && loc.Column >= 0 && loc.Column <= 19;
        }

        bool IsLegalCell(Location loc, Location charLocation, Direction direction)
        {
            Cell targetCell = World.GetCellByLocation(loc);
            Cell currentCell = World.GetCellByLocation(charLocation);

            if (targetCell.Type == CellType.Tree && direction == Direction.North) return false;
            if (currentCell.Type == CellType.Tree && direction == Direction.South) return false;
            if (targetCell.Type == CellType.Lodge) return false;
            if (currentCell.Type == CellType.Floor && targetCell.Type == CellType.Empty) return false;

            return true;
        }
    }
}
