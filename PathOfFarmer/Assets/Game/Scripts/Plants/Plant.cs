using Assets.Game.Scripts.Inventories;
using Assets.Game.Scripts.Seasons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    public class Plant : IItem
    {
        private readonly PlantStatsConfig _plantStatsConfig;
        private readonly SeasonController _seasonController;
        private List<PlantView> _plantViews = new();
        private GrowthStage _currentStageGo;
        private GrowthStages _growthStages;
        private int _currenStage;

        public Plant(PlantStatsConfig plantStatsConfig, SeasonController seasonController)
        {
            _plantStatsConfig = plantStatsConfig;
            _seasonController = seasonController;
        }

        public PlantStatsConfig Stats => _plantStatsConfig;
        public int CurrentStage => _currenStage;
        public bool GrowthCompleted { get; private set; }
        public BaseStoreHouseCellConfig Config => _plantStatsConfig;

        public void Spawn(Transform[] points, Transform parentTransform)
        {
            foreach (Transform point in points)
            {
                var go = new GameObject(_plantStatsConfig.Name);
                go.transform.SetParent(parentTransform);
                go.transform.position = point.position;
                go.AddComponent<PlantView>();

                _plantViews.Add(go.GetComponent<PlantView>());
            }

            SpawnStages(points);

            _currentStageGo = _growthStages.GetFirst();
            _currentStageGo.Acivate();
            _currentStageGo.StageCompletedEvent += OnStageCompleted;
        }

        private void OnStageCompleted()
        {
            if (GrowthCompleted) return;

            if (!_growthStages.HaveNext())
            {
                GrowthCompleted = true;

                _currentStageGo.StageCompletedEvent -= OnStageCompleted;

                return;
            }

            _currentStageGo.Deactivate();
            _currentStageGo.StageCompletedEvent -= OnStageCompleted;

            _currentStageGo = _growthStages.GetNext();

            _currentStageGo.Acivate();
            _currentStageGo.StageCompletedEvent += OnStageCompleted;
        }

        private void SpawnStages(Transform[] points)
        {
            var growthStages = new List<GrowthStage>();

            for (var i = 0; i < _plantStatsConfig.PlantStages.Length; i++)
            {
                var stageGameObjects = new List<GameObject>();

                for (var j = 0; j < points.Length; j++)
                {
                    var newStageGo = Object.Instantiate(
                        _plantStatsConfig.PlantStages[i]._prefabStage,
                        points[j].position,
                        _plantStatsConfig.PlantStages[i]._prefabStage.transform.rotation,
                        _plantViews[j].transform);

                    newStageGo.SetActive(false);

                    stageGameObjects.Add(newStageGo);
                }

                growthStages.Add(new GrowthStage(stageGameObjects.ToArray(), _plantStatsConfig.PlantStages[i]._seasons, _seasonController));
            }

            _growthStages = new GrowthStages(growthStages.ToArray());
        }

        public void Delete()
        {
            foreach (PlantView plant in _plantViews)
            {
                Object.Destroy(plant.gameObject);
            }

            _plantViews.Clear();
        }
    }
}