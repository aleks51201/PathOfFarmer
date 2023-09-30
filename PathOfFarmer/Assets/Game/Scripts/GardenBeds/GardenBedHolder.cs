using Assets.Game.Scripts.Builders;
using Assets.Game.Scripts.Inventories;
using Assets.Game.Scripts.Plants;
using Assets.Game.Scripts.Seasons;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.GardenBeds
{
    public class GardenBedHolder : IHavePlantStatsConfig
    {
        private GardenBedHolderView _holderView;
        private readonly BuildController _buildController;
        private readonly SeasonController _seasonController;
        private readonly StoreHouse _storeHouse;
        private readonly UiMediator _uiMediator;
        private Dictionary<GardenBedView, GardenBed> _gardenBedMap = new();

        public GardenBedHolder(GardenBedHolderView holderView, BuildController buildController,
             SeasonController seasonController, StoreHouse storeHouse, UiMediator uiMediator)
        {
            _holderView = holderView ?? throw new ArgumentNullException(nameof(holderView));
            _buildController = buildController ?? throw new ArgumentNullException(nameof(buildController));
            _seasonController = seasonController ?? throw new ArgumentNullException(nameof(seasonController));
            _storeHouse = storeHouse ?? throw new ArgumentNullException(nameof(storeHouse));
            _uiMediator = uiMediator ?? throw new ArgumentNullException(nameof(uiMediator));

            _buildController.BuildCompletedEvent += OnBuildCompleted;
            _uiMediator.PlantsSelectedEvent += OnPlantSelected;

            CreateCompleted();
        }

        public PlantStatsConfig PlantStatsConfig { get; set; }

        private void OnBuildCompleted(GameObject buildingObject)
        {
            if (buildingObject.GetComponent<GardenBedView>() is GardenBedView gardenBedView)
            {
                OnBuildCompleted(gardenBedView);
            }
        }

        private void OnBuildCompleted(GardenBedView gardenBedView)
        {
            GardenBed gardenBed = Create(gardenBedView);

            AddToCollection(gardenBedView, gardenBed);

            gardenBedView.transform.SetParent(_holderView.transform);
        }

        private void AddToCollection(GardenBedView buildingObject, GardenBed gardenBed)
        {
            if (_gardenBedMap.TryGetValue(buildingObject, out var value))
            {
                throw new ArgumentException("match found");
            }
            _gardenBedMap.Add(buildingObject, gardenBed);
        }

        private GardenBed Create(GardenBedView gardenBedView)
        {
            return new GardenBed(gardenBedView, _seasonController, _storeHouse,this);
        }

        private void CreateCompleted()
        {
            foreach (var bed in _holderView.GardenBeds)
            {
                OnBuildCompleted(bed);
            }
        }


        private void OnPlantSelected(PlantStatsConfig config)
        {
            _uiMediator.CloseBuilderPanel();
            PlantStatsConfig = config;
        }
    }
}