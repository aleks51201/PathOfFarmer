using UnityEngine;

namespace Assets.Game.Scripts.Players
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float _speed;

        public float Speed => _speed;
    }
}
