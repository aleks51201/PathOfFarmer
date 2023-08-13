using UnityEngine;

namespace Assets.Game.Scripts.Builders
{
    public class MaterialChanger
    {
        private readonly Renderer _renderer;
        private readonly Material _defaultMaterial;

        public MaterialChanger(Renderer renderer)
        {
            _renderer = renderer ?? throw new System.ArgumentNullException(nameof(renderer));
            _defaultMaterial = renderer.material;
        }

        public void Change(Material material)
        {
            _renderer.material = material;
        }

        public void Reset()
        {
            _renderer.material = _defaultMaterial;
        }
    }
}
