using System;
using UnityEngine;

namespace Assets.Game.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ItemsConfig", menuName = "SO/ItemsConfig")]
    public class ItemsConfig : ScriptableObject
    {
        [SerializeField] private ItemData[] _items;

        public ItemData[] Items => _items;
    }

    [Serializable]
    public struct ItemData
    {
        public Sprite Icon;
        public GameObject Prefab;
    }
}