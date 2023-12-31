using System;

namespace Assets.Game.Scripts.Players
{
    public class Player : IDisposable
    {
        private Mover _mover;
        private Rotator _rotator;

        public Player(PlayerView playerView, CustomInput input)
        {
            PlayerView = playerView;
            _mover = new Mover(this, input);
            _rotator = new Rotator(this, input);
        }

        public PlayerView PlayerView { get; set; }

        public void Tick()
        {
            _mover.Move();
            _rotator.Rotate();
        }

        public void Start()
        {
            _mover.Start();
            _rotator.Start();
        }

        public void Delete()
        {

        }

        public void Dispose()
        {
            _mover.Stop();
            _rotator.Stop();

            UnityEngine.Object.Destroy(PlayerView);
        }
    }
}
