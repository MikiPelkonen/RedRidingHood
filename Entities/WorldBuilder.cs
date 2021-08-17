using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Graphics;

namespace RedRidingHood.Entities
{
    public class WorldBuilder
    {
        public World CreateWorld(Texture2D spriteSheet, Texture2D world)
        {
            Cell[,] cells = new Cell[20, 22];

            

            for (int row = 0; row < cells.GetLength(0); row++)
            {
                for (int column = 0; column < cells.GetLength(1); column++)
                {
                    cells[row, column] = new Cell(new Location(row, column, 0), CellType.Empty);
                }
            }

            Sprite One = new Sprite(spriteSheet, 64, 48, 16, 32);
            Sprite Two = new Sprite(spriteSheet, 64, 80, 16, 32);

            cells[2, 2] = new Cell(new Location(2, 2, 0), CellType.Tree, One);
            cells[1, 10] = new Cell(new Location(1, 10, 0), CellType.Tree, One);
            cells[2, 9] = new Cell(new Location(2, 9, 0), CellType.Tree, Two);
            cells[1, 13] = new Cell(new Location(1, 13, 0), CellType.Tree, Two);
            cells[5, 5] = new Cell(new Location(5, 5, 0), CellType.Tree, Two);
            cells[7, 0] = new Cell(new Location(7, 0, 0), CellType.Tree, One);
            cells[8, 6] = new Cell(new Location(6, 6, 0), CellType.Tree, One);

            return new World(cells, world);
        }
    }
}
