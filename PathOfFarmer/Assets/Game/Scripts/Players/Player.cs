using UnityEngine;

namespace Assets.Game.Scripts.Players
{
    public class Player
    {
        public Player()
        {

        }

        public PlayerView PlayerView { get; set; }
    }

    public class Mover
    {
        private readonly Player _player;

        public Mover(Player player)
        {
            _player = player;
        }

        public void Move()
        {
            _player.PlayerView.transform.position += new Vector3();
        }
    }
}
