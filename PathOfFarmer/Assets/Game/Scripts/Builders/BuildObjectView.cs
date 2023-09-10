using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Builders
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class BuildObjectView : MonoBehaviour, IBuildObject
    {
        [SerializeField, FoldoutGroup("Components")] private Renderer _renderer;
        [SerializeField, BoxGroup("Materials")] private Material _positive;
        [SerializeField, BoxGroup("Materials")] private Material _negative;

        public GameObject GameObject => gameObject;
        public Renderer Renderer => _renderer;
        public Material Positive => _positive;
        public Material Negative => _negative;

        public event Action<Collider> TriggerEnteredEvent = delegate { };
        public event Action<Collider> TriggerExitEvent = delegate { };

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnteredEvent.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExitEvent.Invoke(other);
        }
    }
}
