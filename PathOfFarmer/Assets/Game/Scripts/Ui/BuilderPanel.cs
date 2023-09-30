using Cinemachine;
using UnityEngine;

namespace Assets.Game.Scripts.Ui
{
    public class BuilderPanel : MonoBehaviour, IUi
    {
        private UiMediator _uiMediator;
        private CustomInput _customInput;
        private CinemachineVirtualCamera _camera;

        public void Initialize(UiMediator uiMediator)
        {
            _uiMediator = uiMediator;
            _customInput = _uiMediator.CustomInput;
            _camera = _uiMediator.Camera;

            uiMediator.StartOpenBuilderPanelEvent += OnStartOpen;
        }

        private void OnStartOpen()
        {
            gameObject.SetActive(true);

            _customInput.Player.Disable();
            _camera.enabled = false;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _uiMediator.StartOpenBuilderPanelEvent -= OnStartOpen;
            _uiMediator.StartCloseBuilderPanelEvent += OnStartClose;
        }

        private void OnStartClose()
        {
            gameObject.SetActive(false);

            _customInput.Player.Enable();
            _camera.enabled = true;

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