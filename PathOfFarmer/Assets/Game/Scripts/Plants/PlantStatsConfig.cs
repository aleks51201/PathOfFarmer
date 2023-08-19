using System;
using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    [CreateAssetMenu(fileName ="PlantStatsConfig",menuName = "SO/PlantStatsConfig")]
    public class PlantStatsConfig: BaseStoreHouseCellConfig
    {
        [SerializeField] private PlantStage[] _plantStages;

        public PlantStage[] PlantStages => _plantStages; 
    }


    [Serializable]
    public struct PlantStage
    {
        public GameObject _prefabStage;
        public int _seasons;
    }
}