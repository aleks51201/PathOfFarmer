using Assets.Game.Scripts.Builders;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Game.Scripts.GardenBeds
{
    public class GardenBed
    {
        private readonly UiMediator _uiMediator;

        public GardenBed(GardenBedView gardenBedView, UiMediator uiMediator)
        {
            GardenBedView = gardenBedView ?? throw new ArgumentNullException(nameof(gardenBedView));
            _uiMediator = uiMediator ?? throw new ArgumentNullException(nameof(uiMediator));

            gardenBedView.InteractedEvent += OnInteracted;
        }

        public GardenBedView GardenBedView { get; }

        private void OnInteracted()
        {
            _uiMediator.OpenGardenBedPanel();
        }
    }
}