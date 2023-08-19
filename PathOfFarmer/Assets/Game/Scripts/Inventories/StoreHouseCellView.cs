using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Inventories
{
    public class StoreHouseCellView : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Component")] private TMP_Text _name;
        [SerializeField, FoldoutGroup("Component")] private TMP_Text _count;
        [SerializeField, FoldoutGroup("Component")] private Image _image;

        public TMP_Text Name => _name; 
        public TMP_Text Count => _count;
        public Image Image => _image;
    }
}