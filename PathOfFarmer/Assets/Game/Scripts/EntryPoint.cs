using Assets.Game.Scripts.Builders;
using Assets.Game.Scripts.Players;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private UiMediator _uiMediator;
        [SerializeField] private BuildObfectView _buildObjectViewPrefab;
        [SerializeField] private Transform _spawnHolder;
        private Player _player;
        private BuildController _buildController;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _player = new Player(_playerView);
            _buildController = new BuildController(_buildObjectViewPrefab, _spawnHolder);
        }

        private void Start()
        {
            _uiMediator.Initialize();
            _player.Start();
            _buildController.Start();
        }

        private void Update()
        {
            _player.Tick();
            _buildController.Tick();
        }

        private void FixedUpdate()
        {
        }

        private void OnDestroy()
        {

        }
    }
}
