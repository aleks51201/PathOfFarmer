using Assets.Game.Scripts.Seasons;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    public class GrowthStage
    {
        private readonly int _numGrowingSeasons;
        private readonly SeasonController _seasonController;
        private int _currentStage;

        public GrowthStage(GameObject stageObject, int numGrowingSeasons, SeasonController seasonController)
        {
            StageObjects = new GameObject[] { stageObject };
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
            foreach (var stage in StageObjects)
            {
                stage.SetActive(true);
            }

            _seasonController.UpdatedEvent += OnSeasonUpdated;
        }

        public void Deactivate()
        {
            foreach (var stage in StageObjects)
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