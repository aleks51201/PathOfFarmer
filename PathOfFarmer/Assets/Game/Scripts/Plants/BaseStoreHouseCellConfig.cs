using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    public abstract class BaseStoreHouseCellConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _weight;
        [SerializeField] private int _value;


        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public int Weight => _weight;
        public int Value => _value;
    }
}