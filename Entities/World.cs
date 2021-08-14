using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Entities
{
    public class World : IGameEntity
    {
        private readonly Cell[,] _world;

        public World(Cell[,] world)
        {
            _world = world;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Cell c in _world)
                c.Draw(spriteBatch);
        }

        public void Update(GameTime gameTime)
        {

        }
    }

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

    public class Cell
    {
        public Location Location { get; }
        public CellType Type { get; }
        public Sprite Sprite { get; }

        public float Depth => Location.Floor * 0.1f + Location.Row * 0.01f;

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

        public Vector2 ToVector()
        {
            return new Vector2(Column * 32, Row * 32);
        }
    }

    public enum CellType { Empty, Grass, Road, Lake, Lodge, RedLodge, GrandLodge, Tree }
}
