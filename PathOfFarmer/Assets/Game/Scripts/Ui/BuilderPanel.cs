using UnityEngine;

namespace Assets.Game.Scripts.Ui
{
    public class BuilderPanel : MonoBehaviour, IUi
    {
        private UiMediator _uiMediator;

        public void Initialize(UiMediator uiMediator)
        {
            _uiMediator = uiMediator;
            uiMediator.StartOpenBuilderPanelEvent += OnStartOpen;
        }

        private void OnStartOpen()
        {
            gameObject.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _uiMediator.StartOpenBuilderPanelEvent -= OnStartOpen;
            _uiMediator.StartCloseBuilderPanelEvent += OnStartClose;
        }

        private void OnStartClose()
        {
            gameObject.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _uiMediator.StartOpenBuilderPanelEvent += OnStartOpen;
            _uiMediator.StartCloseBuilderPanelEvent -= OnStartClose;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}