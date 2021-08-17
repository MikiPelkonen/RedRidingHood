using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Graphics;

namespace RedRidingHood.Entities
{
    public class Cell
    {
        public Location Location { get; }
        public CellType Type { get; }
        public Sprite Sprite { get; }

        public float Depth => Location.Floor * 0.1f + Location.Row * 0.01f + 0.02f;

        public Cell(Location location, CellType type)
        {
            Location = location;
            Type = type;
        }
        public Cell(Location location, CellType type, Sprite sprite)
        {
            Location = location;
            Type = type;
            Sprite = sprite;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Location, Depth);
        }
    }
}
