using Microsoft.Xna.Framework;

namespace RedRidingHood.Entities
{
    public class Location
    {
        public int Row { get; }
        public int Column { get; }
        public int Floor { get; }

        public Location(int row, int column, int floor)
        {
            Row = row;
            Column = column;
            Floor = floor;
        }

        public static implicit operator Vector2(Location loc) => new Vector2(loc.Column * 32, loc.Row * 32);

        public override string ToString()
        {
            return $"Row: {Row}\nColumn: {Column}\nFloor: {Floor}";
        }
    }
}
