using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Game.Scripts.Inventories
{
    public class StoreHouseView : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Component")] private Transform _container;
        [SerializeField, FoldoutGroup("Component")] private StoreHouseCellView _storeHouseCellView;

        public Transform Container => _container;
        public StoreHouseCellView StoreHouseCellView => _storeHouseCellView; 
    }
}