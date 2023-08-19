using Assets.Game.Scripts.Seasons;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Game.Scripts
{
    public class UiMediator : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _monoBehaviours;

        public SeasonController SeasonController { get; private set; }

        private CustomInput _customInput;

        public event Action StartOpenGardenPanelEvent = delegate { };
        public event Action StartOpenInventoryPanelEvent = delegate { };

        public void Initialize(SeasonController seasonController)
        {
            SeasonController = seasonController;

            _customInput = new CustomInput();
            _customInput.Enable();

            _customInput.Player.Inventory.performed += OnInvenoryOpen;

            DependencyInjections();
        }

        private void OnInvenoryOpen(InputAction.CallbackContext context)
        {
            StartOpenInventoryPanelEvent.Invoke();
        }

        private void DependencyInjections()
        {
            foreach (var monoBehaviour in _monoBehaviours)
            {
                if (monoBehaviour is IUi ui)
                {
                    ui.Initialize(this); ;
                }
            }
        }

        public void OpenGardenBedPanel()
        {
            StartOpenGardenPanelEvent.Invoke();
        }
    }

}
