using UnityEngine;

namespace Assets.Game.Scripts.Players
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotateSpeed;

        public float MovementSpeed => _movementSpeed;
        public float RotateSpeed => _rotateSpeed; 
    }
}
