using Assets.Game.Scripts.Seasons;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    public class GrowthStages
    {
        private readonly GrowthStage[] _stages;
        private int _currentIndex;

        public GrowthStages(GrowthStage[] stages)
        {
            _stages = stages;
        }

        public bool HaveNext()
        {
            return _currentIndex < _stages.Length - 1;
        }

        public GrowthStage GetFirst()
        {
            _currentIndex = 0;
            return _stages[0];
        }

        public GrowthStage GetNext()
        {
            _currentIndex = Mathf.Clamp(_currentIndex + 1, 0, _stages.Length - 1);
            return _stages[_currentIndex];
        }
    }

    public class GrowthStage
    {
        private readonly int _numGrowingSeasons;
        private readonly SeasonController _seasonController;
        private int _currentStage;

        public GrowthStage(GameObject stageObject, int numGrowingSeasons, SeasonController seasonController)
        {
            StageObjects = new GameObject[]{ stageObject };
            _numGrowingSeasons = numGrowingSeasons;
            _seasonController = seasonController;
        }
        public GrowthStage(GameObject[] stageObjects, int numGrowingSeasons, SeasonController seasonController)
        {
            StageObjects = stageObjects;
            _numGrowingSeasons = numGrowingSeasons;
            _seasonController = seasonController;
        }

        public GameObject[] StageObjects { get; }
        public bool StageCompleted => _currentStage >= _numGrowingSeasons;

        public event Action StageCompletedEvent = delegate { };

        public void Acivate()
        {
            foreach(var stage in StageObjects)
            {
                stage.SetActive(true);
            }

            _seasonController.UpdatedEvent += OnSeasonUpdated;
        }

        public void Deactivate()
        {
            foreach(var stage in StageObjects)
            {
                stage.SetActive(false);
            }

            _seasonController.UpdatedEvent -= OnSeasonUpdated;
        }

        private void OnSeasonUpdated(int _)
        {
            _currentStage++;

            if (StageCompleted)
            {
                StageCompletedEvent.Invoke();
            }
        }
    }
}