using Assets.Game.Scripts.Inventories;
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
        private readonly IHavePlantStatsConfig _plantStatsConfigHolder;
        private Plant _plant;
        private PointsForPlantView _points;

        public GardenBed(GardenBedView gardenBedView, SeasonController seasonController, StoreHouse storeHouse, IHavePlantStatsConfig plantStatsConfigHolder)
        {
            GardenBedView = gardenBedView ?? throw new ArgumentNullException(nameof(gardenBedView));
            _seasonController = seasonController ?? throw new ArgumentNullException(nameof(seasonController));
            _storeHouse = storeHouse ?? throw new ArgumentNullException(nameof(storeHouse));
            _plantStatsConfigHolder = plantStatsConfigHolder ?? throw new ArgumentNullException(nameof(plantStatsConfigHolder));

            gardenBedView.InteractedEvent += OnInteracted;

            _points = Object.Instantiate(GardenBedView.PlantConfig.PointsForPlant, GardenBedView.AnchorForPoint);
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
                if (_plantStatsConfigHolder.PlantStatsConfig == null) return;

                _plant = new Plant(_plantStatsConfigHolder.PlantStatsConfig, _seasonController);
                _plant.Spawn(_points.Points, GardenBedView.transform);
            }
        }
    }
}