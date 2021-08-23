using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Graphics;

namespace RedRidingHood.Entities
{
    public class Cell
    {
        public Location Location { get; }
        public CellType Type { get; }
        public Sprite Sprite { get; }
        public float Depth => (float)(Location.Row * 0.01f + 0.015f);
        public bool HasCharacter { get; set; }


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
            Vector2 offset = new Vector2(0, 0);
            float depth = Depth;
            if (Type == CellType.Tree)
            {
                offset = new Vector2(0, -32);
                depth -= 0.01f;
            }
            if (Type == CellType.Lodge)
            {
                offset = new Vector2(0, -32);
                depth -= 0.01f;
            }
            if (Type == CellType.Roof)
                depth += 0.2f;
            if (Type == CellType.Blanket)
                depth += 0.005f;

            Sprite.Draw(spriteBatch, Location + offset, depth);
        }
    }
}
