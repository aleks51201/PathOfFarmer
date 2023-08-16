using Assets.Game.Scripts.Seasons;
using System;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class UiMediator : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _monoBehaviours;

        public SeasonController SeasonController { get; private set; }


        public event Action StartOpenGardenPanelEvent = delegate { };

        public void Initialize(SeasonController seasonController)
        {
            SeasonController = seasonController;

            DependencyInjections();
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
