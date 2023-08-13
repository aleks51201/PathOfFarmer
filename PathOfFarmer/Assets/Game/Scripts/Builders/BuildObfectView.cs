using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Builders
{
    public class BuildObfectView : MonoBehaviour, IBuildObject
    {
        [SerializeField, FoldoutGroup("Components")] private Renderer _renderer;
        [SerializeField, BoxGroup("Materials")] private Material _positive;
        [SerializeField, BoxGroup("Materials")] private Material _negative;

        public GameObject GameObject => gameObject;
        public Renderer Renderer => _renderer;
        public Material Positive => _positive;
        public Material Negative => _negative;

        public event Action<Collider> TriggerEntered = delegate { };
        public event Action<Collider> TriggerExit = delegate { };

        private void OnTriggerEnter(Collider other)
        {
            TriggerEntered.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit.Invoke(other);
        }
    }
}
