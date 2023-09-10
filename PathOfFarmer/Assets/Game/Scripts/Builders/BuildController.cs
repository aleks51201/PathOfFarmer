using Assets.Game.Scripts.BuildStates;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Game.Scripts.Builders
{
    public class BuildController : ITick
    {
        private readonly Builder _builder;
        private readonly CustomInput _customInput;
        private readonly UiMediator _uiMediator;

        public BuildController(Transform parentTransform, UiMediator uiMediator, CustomInput input)
        {
            _uiMediator = uiMediator ?? throw new ArgumentNullException(nameof(uiMediator));
            _customInput = input ?? throw new ArgumentNullException(nameof(input));

            _builder = new Builder(parentTransform);
        }

        public bool IsBuildingStage => _builder.IsBuilding;

        public void Start()
        {
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
            _uiMediator.OpenBuilderPanel();
            _customInput.Player.Disable();

            _uiMediator.BuildObjectSelectedEvent += OnSelected;
            _customInput.Player.Build.performed -= OnBuildClick;
        }

        private void OnSelected(BuildObjects prefabs)
        {
            _uiMediator.CloseBuilderPanel();
            _customInput.Player.Enable();

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _builder.ChangeBuildObject(prefabs.GhostPrefab, prefabs.BuilingObjectPrefab);
            _builder.Start();

            _customInput.Player.Enable();

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

