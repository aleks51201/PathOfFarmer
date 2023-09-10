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

        public BuildObject(BuildObjectView buildObjectView, Transform parentTransform)
        {
            Prefab = buildObjectView ?? throw new System.ArgumentNullException(nameof(buildObjectView));
            _parentTransform = parentTransform ?? throw new System.ArgumentNullException(nameof(parentTransform));
            _collisionHolder = new BuildObjectCollisionHolder();
        }

        public BuildObjectView Prefab { get; }
        public BuildObjectView View { get; private set; }


        public BuildObjectView Spawn()
        {
            View = Object.Instantiate(Prefab, _parentTransform.position, Quaternion.identity, _parentTransform);

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

                return;
            }

            Unsub();

            ChangeLayer();

            ResetMaterial();

            _isCompleted = true;
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
