using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace RedRidingHood.Entities
{
    public class EntityManager
    {
        // Lists for storing, adding and removing entities 
        private readonly List<IGameEntity> _entities = new List<IGameEntity>();
        private readonly List<IGameEntity> _entitiesToAdd = new List<IGameEntity>();
        private readonly List<IGameEntity> _entitiesToRemove = new List<IGameEntity>();

        public IEnumerable<IGameEntity> Entities => new ReadOnlyCollection<IGameEntity>(_entities);

        public void Update(GameTime gameTime)
        {
            foreach (IGameEntity entity in _entities)
                entity.Update(gameTime);

            foreach (IGameEntity entity in _entitiesToAdd)
                _entities.Add(entity);

            foreach (IGameEntity entity in _entitiesToRemove)
                _entities.Remove(entity);

            _entitiesToAdd.Clear();
            _entitiesToRemove.Clear();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (IGameEntity entity in _entities)
                entity.Draw(spriteBatch, gameTime);
        }

        public void Add(IGameEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "Null cannot be added as entity.");

            _entitiesToAdd.Add(entity);
        }

        public void Remove(IGameEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity), "Null is not a valid entity.");

            _entitiesToRemove.Add(entity);
        }

        public void Clear()
        {
            _entitiesToRemove.AddRange(_entities);
        }

        public IEnumerable<T> GetEntitiesOfType<T>() where T : IGameEntity
        {
            return _entities.OfType<T>();
        }

        public Character CharacterByLocation(Location loc)
        {
            var targetChars = from c in GetEntitiesOfType<Character>()
                              where c.Location == loc
                              select c;

            List<Character> charList = new List<Character>();

            foreach (Character c in targetChars)
                charList.Add(c);

            if (charList.Count > 0)
                return charList[0];

            return null;
        }

    }
}
