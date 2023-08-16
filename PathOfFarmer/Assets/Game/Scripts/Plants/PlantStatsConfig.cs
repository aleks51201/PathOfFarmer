using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    [CreateAssetMenu(fileName ="PlantStatsConfig",menuName = "SO/PlantStatsConfig")]
    public class PlantStatsConfig : ScriptableObject
    {
        [SerializeField] private int _seasons;

        public int Seasons => _seasons;
    }
}