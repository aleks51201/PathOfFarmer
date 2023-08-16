using Assets.Game.Scripts.Seasons;
using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    public class Plant
    {
        private readonly PlantView _plantView;
        private readonly SeasonController _seasonController;
        private int _currenStage;

        public Plant(PlantView plantView, SeasonController seasonController)
        {
            _plantView = plantView;
            _seasonController = seasonController;

            _plantView.GrowthStages.Stages[0].SetActive(true);

            _seasonController.UpdatedEvent += OnSeasonUpdated;
        }

        private void OnSeasonUpdated(int value)
        {
            _plantView.GrowthStages.Stages[_currenStage].SetActive(false);

            _currenStage = Mathf.Clamp(_currenStage + 1, 0, _plantView.GrowthStages.Stages.Length - 1);

            _plantView.GrowthStages.Stages[_currenStage].SetActive(true);
        }
    }
}