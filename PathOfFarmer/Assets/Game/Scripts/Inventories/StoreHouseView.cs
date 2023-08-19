using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Game.Scripts.Inventories
{
    public class StoreHouseView : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Component")] private Transform _container;

        public Transform Container => _container; 
    }
}