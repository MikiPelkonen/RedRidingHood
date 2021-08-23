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
        Player _player;
        Random _random = new Random();

        public int CurrentFloor => _player.Location.Floor;

        public NPController(World world, RedGirl redGirl, Player player)
        {
            _world = world;
            _player = player;
            _redGirl = redGirl;
            _redGirl.Move += OnRedGirlMove;
            _redGirl.DialogueOver += OnRedGirlDialogueOver;
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

        private void OnRedGirlDialogueOver()
        {
            _player.State = CharacterState.Idle;
            _redGirl.State = CharacterState.Idle;
        }
        
        public void Update()
        {
            _redGirl.PlayerFloor = CurrentFloor;
        }
    }
}
