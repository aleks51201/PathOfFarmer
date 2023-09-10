using Assets.Game.Scripts.BuildStates;
using Assets.Game.Scripts.Seasons;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Game.Scripts
{
    public class UiMediator : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _monoBehaviours;

        private CustomInput _customInput;

        public SeasonController SeasonController { get; private set; }
        public BuildinObjectConfig BuildinObjectConfig { get; private set; }
        public CustomInput CustomInput => _customInput;

        public event Action StartOpenGardenPanelEvent = delegate { };
        public event Action StartOpenInventoryPanelEvent = delegate { };
        public event Action StartOpenBuilderPanelEvent = delegate { };
        public event Action StartCloseBuilderPanelEvent = delegate { };
        public event Action<BuildObjects> BuildObjectSelectedEvent = delegate { };

        public void Initialize(CustomInput input, SeasonController seasonController, BuildinObjectConfig buildingObjectConfig)
        {
            SeasonController = seasonController ?? throw new ArgumentNullException(nameof(seasonController));
            BuildinObjectConfig = buildingObjectConfig ?? throw new ArgumentNullException(nameof(buildingObjectConfig));

            _customInput = input;

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

        public void OpenBuilderPanel()
        {
            StartOpenBuilderPanelEvent.Invoke();
        }
        public void CloseBuilderPanel()
        {
            StartCloseBuilderPanelEvent.Invoke();
        }

        public void OnBuildObjectSelected(BuildObjects prefabs)
        {
            BuildObjectSelectedEvent.Invoke(prefabs);
        }
    }
}
