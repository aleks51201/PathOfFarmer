using Assets.Game.Scripts.Players;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private UiMediator _uiMediator;
        private Player _player;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _player = new Player(_playerView);
        }

        private void Start()
        {
            _uiMediator.Initialize();
            _player.Start();
        }

        private void Update()
        {
        }

        private void FixedUpdate()
        {
            _player.Tick();
        }

        private void OnDestroy()
        {

        }
    }
}
