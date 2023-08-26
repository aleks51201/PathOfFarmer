using Assets.Game.Scripts.Ui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.BuildStates
{
    public class BuildingInventoryCellView : ButtonBase
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _cost;
        [SerializeField] private Image _image;

        public TMP_Text Name => _name;
        public TMP_Text Cost => _cost; 
        public Image Image => _image;

        public override void Initialize(UiMediator uiMediator)
        {
        }

        public override void OnClick()
        {
        }
    }
}