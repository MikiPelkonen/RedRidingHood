using Microsoft.Xna.Framework.Graphics;
using RedRidingHood.Graphics;
using System.Collections.Generic;

namespace RedRidingHood.Entities
{
    public class Player : Character
    {
        public Inventory Inventory { get; set; }
        public int MaxHp { get; } = 5;
        public int CurrentHp { get; set; } = 5;
        public Player(Location startLocation, Texture2D texture) : base(startLocation)
        {
            Inventory = new Inventory();
            _sprites = new Sprite[]
            {
                new Sprite(texture, 96, 0, 16, 18),
                new Sprite(texture, 96, 18, 16, 18),
                new Sprite(texture, 96, 54, 16, 18),
                new Sprite(texture, 96, 36, 16, 18)
            };

            _animations = new SpriteAnimation[] 
            {
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 80, 0, 16, 18),
                        new Sprite(texture, 96, 0, 16, 18),
                        new Sprite(texture, 112, 0, 16, 18),
                        new Sprite(texture, 128, 0, 16, 18),
                        new Sprite(texture, 128, 0, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 80, 18, 16, 18),
                        new Sprite(texture, 96, 18, 16, 18),
                        new Sprite(texture, 112, 18, 16, 18),
                        new Sprite(texture, 128, 18, 16, 18),
                        new Sprite(texture, 128, 18, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 80, 54, 16, 18),
                        new Sprite(texture, 96, 54, 16, 18),
                        new Sprite(texture, 112, 54, 16, 18),
                        new Sprite(texture, 128, 54, 16, 18),
                        new Sprite(texture, 128, 54, 16, 18)
                    }
                    ),
                new SpriteAnimation(
                    new Sprite[]
                    {
                        new Sprite(texture, 80, 36, 16, 18),
                        new Sprite(texture, 96, 36, 16, 18),
                        new Sprite(texture, 112, 36, 16, 18),
                        new Sprite(texture, 128, 36, 16, 18),
                        new Sprite(texture, 128, 36, 16, 18)
                    }
                    )
            };
        }
    }

    public class Inventory
    {
        public List<InventoryItem> Items { get; } = new List<InventoryItem>();

        public Inventory()
        {
            Items.Add(new Bread());
            Items.Add(new Honey());
        }

        public void Add(InventoryItem item)
        {
            Items.Add(item);
        }
        public void Remove(InventoryItem item)
        {
            Items.Remove(item);
        }
    }

    public class InventoryItem
    {

    }

    public class Honey : InventoryItem
    {

    }

    public class Bread : InventoryItem
    {

    }
}
