using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    [CreateAssetMenu(fileName = "PlantStatsConfig", menuName = "SO/PlantStatsConfig")]
    public class PlantStatsConfig : BaseStoreHouseCellConfig
    {
        [SerializeField, BoxGroup("GrowthStage")] private PlantStage[] _plantStages;
        [SerializeField, BoxGroup("GrowthStage")] private PointsForPlantView _pointsForPlant;


        public PlantStage[] PlantStages => _plantStages;
        public PointsForPlantView PointsForPlant => _pointsForPlant; 
    }


    [Serializable]
    public struct PlantStage
    {
        public GameObject _prefabStage;
        public int _seasons;
    }
}