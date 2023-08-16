using Assets.Game.Scripts.Builders;
using Assets.Game.Scripts.Plants;
using Assets.Game.Scripts.Seasons;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Game.Scripts.GardenBeds
{
    public class GardenBed
    {
        private readonly SeasonController _seasonController;
        private List<Plant> _plants = new();

        public GardenBed(GardenBedView gardenBedView, SeasonController seasonController)
        {
            GardenBedView = gardenBedView ?? throw new ArgumentNullException(nameof(gardenBedView));
            _seasonController = seasonController;

            gardenBedView.InteractedEvent += OnInteracted;
        }

        public GardenBedView GardenBedView { get; }

        private void OnInteracted()
        {
            if (_plants.Count > 0) return;

            foreach(var place in GardenBedView.Points)
            {
                var plantView = Object.Instantiate(GardenBedView.Prefab, place.position, Quaternion.identity, GardenBedView.transform);

                _plants.Add(new Plant(plantView, _seasonController));
            }
        }
    }
}