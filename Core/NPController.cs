using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        EntityManager _entityManager;
        World _world;
        Player _player;
        Random _random = new Random();

        Texture2D _spriteSheet;

        float _timer;

        public int CurrentFloor => _player.Location.Floor;

        public NPController(Texture2D spriteSheet, World world, RedGirl redGirl, Player player, EntityManager entityManager)
        {
            _entityManager = entityManager;
            _spriteSheet = spriteSheet;
            _world = world;
            _player = player;
            _redGirl = redGirl;
        }

        
        public void Update(GameTime gameTime)
        {
            if (_timer > 5f)
            {
                SpawnFurry();
                _timer = 0;
            }

            _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void SpawnFurry()
        {
            Furry furry = null;

            furry = new Furry(new Location(20, 7, 0), _spriteSheet, _world, _player);

            _entityManager.Add(furry);
        }
    }
}
