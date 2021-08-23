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

        public static Location operator +(Location a, Location b) => new Location(a.Row + b.Row, a.Column + b.Column, a.Floor + b.Floor);

        public static bool operator ==(Location one, Location two)
        {
            return (one.Row == two.Row && one.Column == two.Column && one.Floor == two.Floor);
        }
        public static bool operator !=(Location one, Location two)
        {
            return !(one.Row == two.Row && one.Column == two.Column && one.Floor == two.Floor);
        }

        public override string ToString()
        {
            return $"Row: {Row}\nColumn: {Column}\nFloor: {Floor}";
        }
    }
}
