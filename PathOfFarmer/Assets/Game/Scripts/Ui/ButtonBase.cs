using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.Ui
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonBase : MonoBehaviour, IUi
    {
        private Button _button;

        public abstract void Initialize(UiMediator uiMediator);
        public abstract void OnClick();

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClickBase);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClickBase);
        }

        private void OnClickBase()
        {
            OnClick();
        }
    }
}
