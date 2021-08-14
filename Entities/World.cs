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
            Sprite.Draw(spriteBatch, Location.ToVector(), Depth);
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

        public override string ToString()
        {
            return $"Row: {Row}\nColumn: {Column}\nFloor: {Floor}";
        }

        public Vector2 ToVector()
        {
            return new Vector2(Column * 32, Row * 32);
        }
    }

    public enum CellType { Empty, Grass, Road, Lake, Lodge, RedLodge, GLodge, Tree }
}
