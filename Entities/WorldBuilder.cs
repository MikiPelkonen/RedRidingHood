using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Graphics;

namespace RedRidingHood.Entities
{
    public class WorldBuilder
    {
        public World CreateWorld(Texture2D spriteSheet)
        {
            Cell[,] cells = new Cell[10, 10];

            for (int row = 0; row < cells.GetLength(0); row++)
            {
                for (int column = 0; column < cells.GetLength(1); column++)
                {
                    cells[row, column] = new Cell(new Location(row, column, 0), CellType.Grass, new Sprite(spriteSheet, 0, 0, 16, 16));
                }
            }

            return new World(cells);
        }
    }
}
