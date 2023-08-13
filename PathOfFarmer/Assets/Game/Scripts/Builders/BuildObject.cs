using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Game.Scripts.Builders
{
    public class BuildObject
    {
        private readonly Transform _parentTransform;
        private readonly BuildObjectCollisionHolder _collisionHolder;

        public BuildObject(BuildObfectView buildObjectView, Transform parentTransform)
        {
            Prefab = buildObjectView ?? throw new System.ArgumentNullException(nameof(buildObjectView));
            _parentTransform = parentTransform ?? throw new System.ArgumentNullException(nameof(parentTransform));
            _collisionHolder = new BuildObjectCollisionHolder();
        }

        public BuildObfectView Prefab { get; }
        public BuildObfectView View { get; private set; }

        private MaterialChanger _materialChanger;

        public BuildObfectView Spawn()
        {
            View = Object.Instantiate(Prefab, _parentTransform.position, Quaternion.identity, _parentTransform);

            _collisionHolder.Reset();

            _materialChanger = new MaterialChanger(View.Renderer);

            Sub();

            UpdateMaterial();

            return View;
        }

        public void Complete()
        {
            if (_collisionHolder.CollisionCount != 0)
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
            View.TriggerEnteredEvent += OnEnter;
            View.TriggerExitEvent += OnExit;
            _collisionHolder.UpdatedEvent += OnCollisionsUpdated;
        }

        private void OnCollisionsUpdated()
        {
            UpdateMaterial();
        }

        private void Unsub()
        {
            View.TriggerEnteredEvent -= OnEnter;
            View.TriggerExitEvent -= OnExit;
        }

        private void OnEnter(Collider collider)
        {
            _collisionHolder.Add(collider);
        }

        private void OnExit(Collider collider)
        {
            _collisionHolder.Remove(collider);
        }

        private void UpdateMaterial()
        {
            _materialChanger.Change(_collisionHolder.CollisionCount == 0 ? View.Positive : View.Negative);
        }

        private void ResetMaterial()
        {
            _materialChanger.Reset();
        }
    }
}
