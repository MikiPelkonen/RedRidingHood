using RedRidingHood.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Entities
{
    public class Character
    {
        Sprite sprite;
        public Location Location { get; set; }
        public CharacterState State { get; set; }
        public Direction Direction { get; set; }
        public float Depth => Location.Floor * 0.1f + Location.Row * 0.01f + 0.01f;

    }

    public enum CharacterState { Idle, Moving, Dead }
    public enum Direction { North, South, East, West }
}
