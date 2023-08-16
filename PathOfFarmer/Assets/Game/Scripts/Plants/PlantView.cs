using Assets.Game.Scripts.GardenBeds;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    public class PlantView : MonoBehaviour, IInteractable
    {
        [SerializeField, FoldoutGroup("Components")] private GrowthStages _growthStages;
        [SerializeField, FoldoutGroup("Components")] private PlantStatsConfig _plantStatsConfig;

        public GrowthStages GrowthStages => _growthStages;
        public PlantStatsConfig PlantStatsConfig => _plantStatsConfig;

        public void Interact()
        {
        }
    }
}