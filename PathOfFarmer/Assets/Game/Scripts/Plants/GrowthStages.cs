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
}