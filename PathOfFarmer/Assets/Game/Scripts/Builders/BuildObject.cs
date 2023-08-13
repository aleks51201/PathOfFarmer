using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Builders
{
    public class BuildObject
    {
        private readonly Transform _parentTransform;
        private List<GameObject> _gameObjects = new();

        public BuildObject(BuildObfectView buildObjectView, Transform parentTransform)
        {
            Prefab = buildObjectView ?? throw new System.ArgumentNullException(nameof(buildObjectView));
            _parentTransform = parentTransform ?? throw new System.ArgumentNullException(nameof(parentTransform));
        }

        public BuildObfectView Prefab { get; }
        public BuildObfectView View { get; private set; }

        public BuildObfectView Spawn(Vector3 position)
        {
            View = Object.Instantiate(Prefab, position, Quaternion.identity, _parentTransform);

            Sub();

            return View;
        }

        private void Sub()
        {
            View.TriggerEntered += OnEnter;
            View.TriggerExit += OnExit;
        }

        private void Unsub()
        {
            View.TriggerEntered -= OnEnter;
            View.TriggerExit -= OnExit;
        }

        private void OnExit(Collider collider)
        {
            _gameObjects.Add(collider.gameObject);
        }

        private void OnEnter(Collider collider)
        {
            _gameObjects.Remove(collider.gameObject);
        }

        private void UpdateMaterial()
        {
            for (var i = 0; i < View.Renderer.materials.Length; i++)
            {
                View.Renderer.materials[i] = _gameObjects.Count == 0 ? View.Positive : View.Negative;
            }
        }
    }
}
