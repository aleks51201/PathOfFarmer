using UnityEngine;

namespace Assets.Game.Scripts.GardenBeds
{
    public class GardenBedHolderView : MonoBehaviour
    {
        [SerializeField] private GardenBedView[] _gardenBeds;

        public GardenBedView[] GardenBeds => _gardenBeds; 
    }
}