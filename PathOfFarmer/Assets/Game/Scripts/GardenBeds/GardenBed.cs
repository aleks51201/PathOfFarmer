﻿using Assets.Game.Scripts.Inventories;
using Assets.Game.Scripts.Plants;
using Assets.Game.Scripts.Seasons;
using System;
using Object = UnityEngine.Object;

namespace Assets.Game.Scripts.GardenBeds
{
    public class GardenBed
    {
        private readonly SeasonController _seasonController;
        private readonly StoreHouse _storeHouse;
        private Plant _plant;
        private PointsForPlantView _points;

        public GardenBed(GardenBedView gardenBedView, SeasonController seasonController, StoreHouse storeHouse)
        {
            GardenBedView = gardenBedView ?? throw new ArgumentNullException(nameof(gardenBedView));
            _seasonController = seasonController;
            _storeHouse = storeHouse;
            gardenBedView.InteractedEvent += OnInteracted;

            _points = Object.Instantiate(GardenBedView.PlantConfig.PointsForPlant,GardenBedView.AnchorForPoint);
        }

        public GardenBedView GardenBedView { get; }
        public Plant Plant => _plant;

        private void OnInteracted()
        {
            if (_plant != null && _plant.GrowthCompleted)
            {
                _storeHouse.AddItem(_plant);
                _plant.Delete();
                _plant = null;
            }
            else if (_plant == null)
            {
                _plant = new Plant(GardenBedView.PlantConfig, _seasonController);
                _plant.Spawn(_points.Points, GardenBedView.transform);
            }
        }
    }
}