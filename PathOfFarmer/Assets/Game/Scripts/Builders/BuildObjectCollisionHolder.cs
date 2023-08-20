using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Builders
{
    public class BuildObjectCollisionHolder
    {
        private List<GameObject> _gameObjects;

        public BuildObjectCollisionHolder()
        {
            _gameObjects = new();
        }

        public int CollisionCount => _gameObjects.Count;

        public event Action UpdatedEvent = delegate { };

        public void Add(Collider collider)
        {
            if (CheckItIsPlane(collider)) return;

            _gameObjects.Add(collider.gameObject);

            UpdatedEvent.Invoke();
        }

        public void Remove(Collider collider)
        {
            if (CheckItIsPlane(collider)) return;

            _gameObjects.Remove(collider.gameObject);

            UpdatedEvent.Invoke();
        }

        public void Reset()
        {
            _gameObjects.Clear();
        }

        private bool CheckItIsPlane(Collider collider)
        {
            return collider.CompareTag("Plane");
        }
    }
}
