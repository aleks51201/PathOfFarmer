using Assets.Game.Scripts.Seasons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    public class Plant
    {
        private readonly PlantView _plantViewPreafab;
        private readonly SeasonController _seasonController;
        private List<PlantView> _plantViews;
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

        public void Spawn(Transform[] points, Transform parentTransform)
        {
            foreach (Transform point in points)
            {
                _plantViews.Add(Object.Instantiate(_plantViewPreafab, point.position, Quaternion.identity, parentTransform));
            }
        }

        private void OnSeasonUpdated(int value)
        {
            ActivateStage(_currenStage, false);

            _currenStage = Mathf.Clamp(_currenStage + 1, 0, _plantViewPreafab.GrowthStages.Stages.Length - 1);

            ActivateStage(_currenStage, true);
        }

        private void ActivateStage(int stage, bool isActive)
        {
            _plantViewPreafab.GrowthStages.Stages[stage].SetActive(isActive);
        }
    }
}