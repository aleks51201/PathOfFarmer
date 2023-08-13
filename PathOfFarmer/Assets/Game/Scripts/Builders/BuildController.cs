using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Game.Scripts.Builders
{
    public class BuildController : ITick
    {
        private readonly Builder _builder;
        private readonly CustomInput _customInput;

        public BuildController(BuildObfectView prefab, Transform parentTransform)
        {
            _builder = new Builder(prefab, parentTransform);

            _customInput = new CustomInput();
        }

        public void Start()
        {
            _customInput.Player.Build.performed += OnPerformed;
        }

        public void Stop()
        {
            _customInput.Player.Build.performed -= OnPerformed;
        }

        public void Tick()
        {
            _builder.Tick();
        }

        private void OnPerformed(CallbackContext context)
        {
            _builder.Start();
        }
    }
}

