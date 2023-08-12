using Assets.Game.Scripts.Players;
using UnityEngine;

public class EntryPoint: MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    private Player _player;

    private void Awake()
    {
        _player = new Player(_playerView);
    }

    private void Start()
    {
        _player.Start();
    }

    private void Update()
    {
        _player.Tick();
    }

    private void FixedUpdate()
    {
        
    }

    private void OnDestroy()
    {
        
    }
}
