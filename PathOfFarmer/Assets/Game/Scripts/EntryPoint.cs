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
        [SerializeField] private BuildObfectView _buildObjectViewPrefab;
        [SerializeField] private Transform _spawnHolder;
        [SerializeField] private GardenBedView _gardenBedView;
        [SerializeField] private StoreHouseView _storeHouseView;
        [SerializeField] private BuildinObjectConfig _buildingObjectConfig;

        private SeasonController _seasonController;
        private Player _player;
        private GardenBed _gardenBed;
        private BuildController _buildController;
        private Interactor _interactor;
        private StoreHouse _storeHouse;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _seasonController = new SeasonController();

            _player = new Player(_playerView);
            _buildController = new BuildController(_buildObjectViewPrefab, _spawnHolder);
            _interactor = new Interactor(_buildController);
            _storeHouse = new StoreHouse(_storeHouseView);
            _gardenBed = new GardenBed(_gardenBedView, _seasonController, _storeHouse);
        }

        private void Start()
        {
            _uiMediator.Initialize(_seasonController, _buildingObjectConfig);
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
