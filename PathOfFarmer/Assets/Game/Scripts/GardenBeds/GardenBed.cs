﻿using Assets.Game.Scripts.Inventories;
using Assets.Game.Scripts.Plants;
using Assets.Game.Scripts.Seasons;
using System;

namespace Assets.Game.Scripts.GardenBeds
{
    public class GardenBed
    {
        private readonly SeasonController _seasonController;
        private readonly StoreHouse _storeHouse;
        private Plant _plant;

        public GardenBed(GardenBedView gardenBedView, SeasonController seasonController, StoreHouse storeHouse)
        {
            GardenBedView = gardenBedView ?? throw new ArgumentNullException(nameof(gardenBedView));
            _seasonController = seasonController;
            _storeHouse = storeHouse;
            gardenBedView.InteractedEvent += OnInteracted;
        }

        public GardenBedView GardenBedView { get; }
        public Plant Plant => _plant;

        private void OnInteracted()
        {
            if (_plant == null) return;

            if (_plant.GrowthCompleted)
            {

                _plant = null;
            }
            else
            {
                _plant = new Plant(GardenBedView.Prefab, _seasonController);
                _plant.Spawn(GardenBedView.Points, GardenBedView.transform);
            }
        }
    }
}