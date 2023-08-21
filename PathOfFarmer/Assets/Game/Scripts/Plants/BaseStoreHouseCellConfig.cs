using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Game.Scripts.Plants
{
    public abstract class BaseStoreHouseCellConfig : ScriptableObject
    {
        [SerializeField, BoxGroup("Base")] private string _name;
        [SerializeField, BoxGroup("Base")] private string _description;
        [SerializeField, BoxGroup("Base")] private Sprite _icon;
        [SerializeField, BoxGroup("Base")] private int _weight;
        [SerializeField, BoxGroup("Base")] private int _value;


        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public int Weight => _weight;
        public int Value => _value;
    }
}