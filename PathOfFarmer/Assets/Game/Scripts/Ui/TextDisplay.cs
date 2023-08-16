using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.Ui
{
    public abstract class TextDisplay : MonoBehaviour, IUi
    {
        [SerializeField] private protected TMP_Text _text;
        public abstract void Initialize(UiMediator uiMediator);
    }
}
