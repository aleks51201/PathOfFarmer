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

        public BuildObfectView Spawn()
        {
            View = Object.Instantiate(Prefab, _parentTransform.position, Quaternion.identity, _parentTransform);

            Sub();

            UpdateMaterial();

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

        private void OnEnter(Collider collider)
        {
            if (CheckItIsPlane(collider)) return;

            _gameObjects.Add(collider.gameObject);

            Debug.Log($"OnEnter {_gameObjects.Count}");

            UpdateMaterial();
        }

        private void OnExit(Collider collider)
        {
            if (CheckItIsPlane(collider)) return;

            _gameObjects.Remove(collider.gameObject);

            Debug.Log($"OnExit {_gameObjects.Count}");

            UpdateMaterial();
        }

       private bool CheckItIsPlane(Collider collider)
        {
            return collider.CompareTag("Plane");
        }

        private void UpdateMaterial()
        {
            View.Renderer.material = _gameObjects.Count == 0 ? View.Positive : View.Negative;
        }
    }
}
