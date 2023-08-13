using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Builders
{
    public class BuildObject
    {
        private readonly Transform _parentTransform;
        private List<GameObject> _gameObjects;

        public BuildObject(BuildObfectView buildObjectView, Transform parentTransform)
        {
            Prefab = buildObjectView ?? throw new System.ArgumentNullException(nameof(buildObjectView));
            _parentTransform = parentTransform ?? throw new System.ArgumentNullException(nameof(parentTransform));
        }

        public BuildObfectView Prefab { get; }
        public BuildObfectView View { get; private set; }

        private MaterialChanger _materialChanger;

        public BuildObfectView Spawn()
        {
            View = Object.Instantiate(Prefab, _parentTransform.position, Quaternion.identity, _parentTransform);

            _gameObjects = new();

            _materialChanger = new MaterialChanger(View.Renderer);

            Sub();

            UpdateMaterial();

            return View;
        }

        public void Complete()
        {
            if (_gameObjects.Count != 0)
            {
                Debug.Log("Construction is impossible");

                Object.Destroy(View.gameObject);

                return;
            }

            Unsub();

            ResetMaterial();
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

            UpdateMaterial();
        }

        private void OnExit(Collider collider)
        {
            if (CheckItIsPlane(collider)) return;

            _gameObjects.Remove(collider.gameObject);

            UpdateMaterial();
        }

        private bool CheckItIsPlane(Collider collider)
        {
            return collider.CompareTag("Plane");
        }

        private void UpdateMaterial()
        {
            _materialChanger.Change(_gameObjects.Count == 0 ? View.Positive : View.Negative);
        }

        private void ResetMaterial()
        {
            _materialChanger.Reset();
        }
    }
}
