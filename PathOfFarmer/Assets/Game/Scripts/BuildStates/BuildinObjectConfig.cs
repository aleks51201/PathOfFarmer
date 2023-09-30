using Assets.Game.Scripts.Builders;
using Assets.Game.Scripts.Plants;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.BuildStates
{
    [CreateAssetMenu(fileName = "BuildinObjectConfig", menuName = "SO/BuildinObjectConfig")]
    public class BuildinObjectConfig : ScriptableObject
    {
        [SerializeField] private BuildingItemData[] _builds;
        [SerializeField] private PlantsItemData[] _plants;

        public BuildingItemData[] Builds => _builds;
        public PlantsItemData[] Plants => _plants;
    }

    [Serializable]
    public struct BuildingItemData
    {
        public string Name;
        [PreviewField] public Sprite Icon;
        public int Cost;
        public BuildObjectView GhostPrefab;
        public GameObject BuilingObjectPrefab;
    }

    [Serializable]
    public struct PlantsItemData
    {
        public string Name;
        [PreviewField] public Sprite Icon;
        public int Cost;
        public PlantStatsConfig PlantStatsConfig;
    }
}