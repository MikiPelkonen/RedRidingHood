using Microsoft.Xna.Framework;
using RedRidingHood.Commands;
using RedRidingHood.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedRidingHood.Core
{
    public class NPController
    {
        RedGirl _redGirl;
        World _world;
        Random _random = new Random();

        public NPController(World world, RedGirl redGirl)
        {
            _world = world;
            _redGirl = redGirl;
            _redGirl.Move += OnRedGirlMove;
        }

        private void OnRedGirlMove()
        {
            Direction randomDirection = _random.Next(4) switch
            {
                0 => Direction.North,
                1 => Direction.South,
                2 => Direction.East,
                3 => Direction.West
            };
            _redGirl.Commands[0] = new MoveCommand(randomDirection, _world);
        }
    }
}
