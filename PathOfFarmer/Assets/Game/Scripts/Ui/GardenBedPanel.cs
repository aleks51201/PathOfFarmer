using UnityEngine;

namespace Assets.Game.Scripts.Ui
{
    public class GardenBedPanel : MonoBehaviour, IUi
    {
        public void Initialize(UiMediator uiMediator)
        {
            uiMediator.StartOpenGardenPanelEvent += OnStartOpen;
        }

        private void OnStartOpen()
        {
            gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void OnDisable()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}