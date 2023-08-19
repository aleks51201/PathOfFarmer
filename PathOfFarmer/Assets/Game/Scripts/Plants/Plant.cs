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
        private List<GrowthStage> _currentStageGo = new();
        private List<GrowthStages> _growthStages = new();
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
            _growthStages.Clear();
            foreach (Transform point in points)
            {
                var go = new GameObject(_plantStatsConfig.Name);
                go.transform.SetParent(parentTransform);
                go.transform.position = point.position;
                go.AddComponent<PlantView>();

                _growthStages.Add(new GrowthStages( SpawnStages(go.transform, point)));

                _plantViews.Add(go.GetComponent<PlantView>());
            }

            foreach (var item in _growthStages)
            {
                var first = item.GetFirst();
                first.Acivate();
                first.StageCompletedEvent += OnStageCompleted;
                _currentStageGo.Add(first);
            }
        }

        private void OnStageCompleted()
        {
            foreach (var item in _currentStageGo)
            {
                item.Deactivate();
                item.StageCompletedEvent -= OnStageCompleted;
            }

            _currentStageGo.Clear();
            foreach (var item in _growthStages)
            {
                if (!item.HaveNext())
                {
                    GrowthCompleted = true;
                    continue;
                }

                _currentStageGo.Add(item.GetNext());
            }


            foreach (var item in _currentStageGo)
            {
                item.Acivate();
                item.StageCompletedEvent += OnStageCompleted;
            }
        }

        private GrowthStage[] SpawnStages(Transform parentTransform, Transform point)
        {
            List<GrowthStage> stages = new();

            for (var i = 0; i < _plantStatsConfig.PlantStages.Length; i++)
            {
                var newStageGo = Object.Instantiate(_plantStatsConfig.PlantStages[i]._prefabStage, point.position, point.rotation, parentTransform);
                newStageGo.SetActive(false);

                var stage = new GrowthStage(newStageGo, _plantStatsConfig.PlantStages[i]._seasons, _seasonController);

                stages.Add(stage);
            }

            return (stages.ToArray());
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