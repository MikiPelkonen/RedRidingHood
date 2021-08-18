using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Graphics;

namespace RedRidingHood.Entities
{
    public class WorldBuilder
    {
        public World CreateWorld(Texture2D spriteSheet, Texture2D world)
        {
            Cell[,] cells = new Cell[25, 22];

            

            for (int row = 0; row < cells.GetLength(0); row++)
            {
                for (int column = 0; column < cells.GetLength(1); column++)
                {
                    cells[row, column] = new Cell(new Location(row, column, 0), CellType.Empty);
                }
            }

            Sprite One = new Sprite(spriteSheet, 64, 48, 16, 32);
            Sprite Two = new Sprite(spriteSheet, 64, 80, 16, 32);


            // HOUSES

            // RedridingHood
            cells[8, 2] = new Cell(new Location(8, 2, 0), CellType.Lodge, new Sprite(spriteSheet, 0, 160, 48, 32));
            cells[8, 3] = new Cell(new Location(8, 3, 0), CellType.Lodge);
            cells[8, 4] = new Cell(new Location(8, 4, 0), CellType.Lodge);
            cells[5, 2] = new Cell(new Location(5, 2, 0), CellType.Roof, new Sprite(spriteSheet, 0, 128, 48, 32));
            // Grandma
            cells[3, 16] = new Cell(new Location(3, 16, 0), CellType.Lodge, new Sprite(spriteSheet, 0, 160, 48, 32));
            cells[3, 17] = new Cell(new Location(3, 17, 0), CellType.Lodge);
            cells[3, 18] = new Cell(new Location(3, 18, 0), CellType.Lodge);
            cells[0, 16] = new Cell(new Location(0, 16, 0), CellType.Roof, new Sprite(spriteSheet, 0, 128, 48, 32));
            // Hunter/Player
            cells[18, 14] = new Cell(new Location(18, 14, 0), CellType.Lodge, new Sprite(spriteSheet, 0, 160, 48, 32));
            cells[18, 15] = new Cell(new Location(18, 15, 0), CellType.Lodge);
            cells[18, 16] = new Cell(new Location(18, 16, 0), CellType.Lodge);
            cells[15, 14] = new Cell(new Location(15, 14, 0), CellType.Roof, new Sprite(spriteSheet, 0, 128, 48, 32));


            // TREES
            cells[3, 2] = new Cell(new Location(3, 2, 0), CellType.Tree, One);
            cells[2, 10] = new Cell(new Location(2, 10, 0), CellType.Tree, One);
            cells[3, 9] = new Cell(new Location(3, 9, 0), CellType.Tree, Two);
            cells[2, 13] = new Cell(new Location(2, 13, 0), CellType.Tree, Two);
            cells[6, 5] = new Cell(new Location(6, 5, 0), CellType.Tree, Two);
            cells[7, 0] = new Cell(new Location(7, 0, 0), CellType.Tree, One);
            cells[7, 6] = new Cell(new Location(7, 6, 0), CellType.Tree, One);
            cells[6, 7] = new Cell(new Location(6, 7, 0), CellType.Tree, Two);
            cells[5, 10] = new Cell(new Location(5, 10, 0), CellType.Tree, One);
            cells[9, 16] = new Cell(new Location(9, 16, 0), CellType.Tree, Two);
            cells[7, 17] = new Cell(new Location(7, 17, 0), CellType.Tree, One);
            cells[10, 19] = new Cell(new Location(10, 19, 0), CellType.Tree, Two);
            cells[11, 18] = new Cell(new Location(11, 18, 0), CellType.Tree, One);
            cells[12, 15] = new Cell(new Location(12, 15, 0), CellType.Tree, Two);
            cells[13, 8] = new Cell(new Location(13, 8, 0), CellType.Tree, Two);
            cells[14, 12] = new Cell(new Location(14, 12, 0), CellType.Tree, One);
            cells[17, 9] = new Cell(new Location(17, 9, 0), CellType.Tree, Two);
            cells[17, 0] = new Cell(new Location(17, 0, 0), CellType.Tree, One);
            cells[16, 4] = new Cell(new Location(16, 4, 0), CellType.Tree, Two);
            cells[17, 5] = new Cell(new Location(17, 5, 0), CellType.Tree, One);
            cells[19, 18] = new Cell(new Location(19, 18, 0), CellType.Tree, One);
            cells[18, 19] = new Cell(new Location(18, 19, 0), CellType.Tree, Two);


            // TODO: Figure out why row 7 bugged... Solved with right values in tree locations X_x.

            return new World(cells, world);
        }
    }
}
