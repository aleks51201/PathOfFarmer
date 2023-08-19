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
            StageObject = stageObject;
            _numGrowingSeasons = numGrowingSeasons;
            _seasonController = seasonController;
        }

        public GameObject StageObject { get; }
        public bool StageCompleted => _currentStage >= _numGrowingSeasons;

        public event Action StageCompletedEvent = delegate { };

        public void Acivate()
        {
            Debug.Log("Acivate");
            StageObject.SetActive(true);
            _seasonController.UpdatedEvent += OnSeasonUpdated;
        }

        public void Deactivate()
        {
            Debug.Log("Deactivate");
            StageObject.SetActive(false);
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