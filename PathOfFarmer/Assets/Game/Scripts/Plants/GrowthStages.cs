using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    public class GrowthStages : MonoBehaviour
    {
        [SerializeField] private GameObject[] _stages;

        public GameObject[] Stages => _stages;
    }
}