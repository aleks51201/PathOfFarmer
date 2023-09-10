using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    public class PointsForPlantView : MonoBehaviour
    {
        [SerializeField] private Transform[] _points;

        public Transform[] Points => _points;
    }
}