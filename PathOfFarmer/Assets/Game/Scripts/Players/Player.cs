using System;

namespace Assets.Game.Scripts.Players
{
    public class Player : IDisposable
    {
        private Mover _mover;

        public Player(PlayerView playerView)
        {
            PlayerView = playerView;
            _mover = new Mover(this);
        }

        public PlayerView PlayerView { get; set; }

        public void Tick()
        {
            _mover.Move();
        }

        public void Start()
        {
            _mover.Start();
        }

        public void Delete()
        {

        }

        public void Dispose()
        {
            _mover.Stop();

            UnityEngine.Object.Destroy(PlayerView);
        }
    }
}
