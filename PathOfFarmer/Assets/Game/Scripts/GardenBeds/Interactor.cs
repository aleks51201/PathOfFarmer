using Assets.Game.Scripts.Builders;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Game.Scripts.GardenBeds
{
    public class Interactor : ITick
    {
        private readonly CustomInput _customInput;
        private readonly BuildController _buildController;
        private IInteractable _interactableObject;

        public Interactor(BuildController buildController, CustomInput input)
        {
            _customInput = input ?? throw new ArgumentNullException(nameof(input));
            _buildController = buildController ?? throw new ArgumentNullException(nameof(buildController));
        }

        public void Start()
        {
            _customInput.Enable();

            _customInput.Player.Fire.performed += OnFireClick;
        }

        public void Stop()
        {
            _customInput.Player.Fire.performed -= OnFireClick;
        }

        public void Tick()
        {
            CastRay();
        }

        private void OnFireClick(CallbackContext context)
        {
            if (_interactableObject == null || _buildController.IsBuildingStage) return;

            _interactableObject.Interact();
        }

        private void CastRay()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~(1 << 6)))
            {
                if (hit.transform.TryGetComponent(out IInteractable interactable))
                {
                    _interactableObject = interactable;
                    return;
                }
            }

            _interactableObject = null;
        }
    }
}