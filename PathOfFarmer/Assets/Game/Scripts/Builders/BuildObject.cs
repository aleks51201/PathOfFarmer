﻿using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Game.Scripts.Builders
{
    public class BuildObject
    {
        private readonly Transform _parentTransform;
        private readonly BuildObjectCollisionHolder _collisionHolder;
        private MaterialChanger _materialChanger;
        private bool _isCompleted;
        private BuildObjectView _ghostPrefab;

        public BuildObject(BuildObjectView ghostPrefab,GameObject buildObjectViewPrefab, Transform parentTransform)
        {
            _ghostPrefab = ghostPrefab ?? throw new System.ArgumentNullException(nameof(ghostPrefab));
            Prefab = buildObjectViewPrefab ?? throw new System.ArgumentNullException(nameof(buildObjectViewPrefab));
            _parentTransform = parentTransform ?? throw new System.ArgumentNullException(nameof(parentTransform));
            _collisionHolder = new BuildObjectCollisionHolder();
        }


        public GameObject Prefab { get; }
        public BuildObjectView View { get; private set; }

        public event Action<GameObject> BuildCompletedEvent = delegate { };
        public event Action BuildCanceledEvent = delegate { };

        public BuildObjectView Spawn()
        {
            View = Object.Instantiate(_ghostPrefab, _parentTransform.position, _ghostPrefab.transform.rotation, _parentTransform);

            _collisionHolder.Reset();

            _materialChanger = new MaterialChanger(View.Renderer);

            _isCompleted = false;

            Sub();

            UpdateMaterial();

            return View;
        }

        public void Complete()
        {
            if (_collisionHolder.CollisionCount != 0)
            {
                Object.Destroy(View.gameObject);
                BuildCanceledEvent.Invoke();
                return;
            }

            var newObject = Object.Instantiate(Prefab, View.transform.position, View.transform.rotation, _parentTransform);
            Object.Destroy(View.gameObject);

            Unsub();

            ChangeLayer();

            ResetMaterial();

            _isCompleted = true;
            BuildCompletedEvent.Invoke(newObject);
        }

        public void Cancel()
        {
            if (!_isCompleted)
            {
                Object.Destroy(View.gameObject);
            }

            Unsub();
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
            Debug.Log($"OnEnter");
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

        private void ChangeLayer()
        {
            var components = View.GetComponentsInChildren<Transform>();

            foreach (var component in components)
            {
                component.gameObject.layer = 0;
            }
        }
    }
}
