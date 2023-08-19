using Assets.Game.Scripts.Inventories;
using Assets.Game.Scripts.Seasons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    public class Plant : IItem
    {
        private readonly PlantView _plantViewPreafab;
        private readonly SeasonController _seasonController;
        private List<PlantView> _plantViews = new();
        private int _currenStage;

        public Plant(PlantView plantViewPrefab, SeasonController seasonController)
        {
            _plantViewPreafab = plantViewPrefab;
            _seasonController = seasonController;
            _plantViewPreafab.GrowthStages.Stages[0].SetActive(true);

            _seasonController.UpdatedEvent += OnSeasonUpdated;
        }

        public PlantStatsConfig Stats => _plantViewPreafab.PlantStatsConfig;
        public int CurrentStage => _currenStage;
        public bool GrowthCompleted => _currenStage == _plantViewPreafab.GrowthStages.Stages.Length - 1;
        public BaseStoreHouseCellConfig Config => _plantViewPreafab.PlantStatsConfig;

        public void Spawn(Transform[] points, Transform parentTransform)
        {
            foreach (Transform point in points)
            {
                _plantViews.Add(Object.Instantiate(_plantViewPreafab, point.position, Quaternion.identity, parentTransform));
            }
        }

        public void Delete()
        {
            foreach (PlantView plant in _plantViews)
            {
                Object.Destroy(plant.gameObject);
            }

            _plantViews.Clear();
        }

        private void OnSeasonUpdated(int value)
        {
            ActivateStage(_currenStage, false);

            _currenStage = Mathf.Clamp(_currenStage + 1, 0, _plantViewPreafab.GrowthStages.Stages.Length - 1);

            ActivateStage(_currenStage, true);
        }

        private void ActivateStage(int stage, bool isActive)
        {
            Debug.Log($"ActivateStage {stage}, {isActive}");
            foreach(var plantView in _plantViews)
            {
                plantView.GrowthStages.Stages[stage].SetActive(isActive);
            }
        }
    }
}