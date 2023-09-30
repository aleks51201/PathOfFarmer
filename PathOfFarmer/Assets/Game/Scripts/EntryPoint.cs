using Assets.Game.Scripts.Builders;
using Assets.Game.Scripts.BuildStates;
using Assets.Game.Scripts.GardenBeds;
using Assets.Game.Scripts.Inventories;
using Assets.Game.Scripts.Players;
using Assets.Game.Scripts.Seasons;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private UiMediator _uiMediator;
        [SerializeField] private BuildObjectView _buildObjectViewPrefab;
        [SerializeField] private Transform _spawnHolder;
        [SerializeField] private GardenBedView _gardenBedView;
        [SerializeField] private StoreHouseView _storeHouseView;
        [SerializeField] private BuildinObjectConfig _buildingObjectConfig;
        [SerializeField] private GardenBedHolderView _gardenBedHolderView;

        private CustomInput _customInput;
        private SeasonController _seasonController;
        private Player _player;
        private GardenBed _gardenBed;
        private BuildController _buildController;
        private Interactor _interactor;
        private StoreHouse _storeHouse;
        private GardenBedHolder _gardenBedHolder;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _customInput = new CustomInput();
            _customInput.Enable();

            _seasonController = new SeasonController(_customInput);
            _player = new Player(_playerView, _customInput);
            _buildController = new BuildController(_spawnHolder, _uiMediator, _customInput);
            _interactor = new Interactor(_buildController, _customInput);
            _storeHouse = new StoreHouse(_storeHouseView);
            _gardenBedHolder = new GardenBedHolder(_gardenBedHolderView,_buildController, _seasonController, _storeHouse, _uiMediator);
        }

        private void Start()
        {
            _uiMediator.Initialize(_customInput, _seasonController, _buildingObjectConfig, _player.PlayerView.Camera);
            _player.Start();
            _buildController.Start();
            _interactor.Start();
        }

        private void Update()
        {
            _player.Tick();
            _buildController.Tick();
            _interactor.Tick();
        }

        private void FixedUpdate()
        {
        }

        private void OnDestroy()
        {

        }
    }
}
