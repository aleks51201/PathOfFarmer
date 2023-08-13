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

        public bool IsBuildingStage => _builder.IsBuilding;

        public void Start()
        {
            _customInput.Enable();

            _customInput.Player.Build.performed += OnBuildClick;
        }

        public void Stop()
        {
            _customInput.Player.Build.performed -= OnBuildClick;
            _customInput.Player.Fire.performed -= OnFireClick;
            _customInput.Player.Rotate.performed -= OnRotateClick;
        }

        public void Tick()
        {
            _builder.Tick();
        }

        private void OnBuildClick(CallbackContext context)
        {
            _builder.Start();

            _customInput.Player.Build.performed -= OnBuildClick;
            _customInput.Player.Fire.performed += OnFireClick;
            _customInput.Player.Rotate.performed += OnRotateClick;
        }

        private void OnRotateClick(CallbackContext context)
        {
            _builder.Rotate();
        }

        private void OnFireClick(CallbackContext context)
        {
            _builder.Build();

            _customInput.Player.Build.performed += OnBuildClick;
            _customInput.Player.Fire.performed -= OnFireClick;
            _customInput.Player.Rotate.performed -= OnRotateClick;
        }
    }
}

