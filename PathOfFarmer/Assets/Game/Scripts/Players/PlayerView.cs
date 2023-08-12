using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Game.Scripts.Players
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField, FoldoutGroup("Components")] private Camera _camera;
        
        [SerializeField, BoxGroup("Settings")] private float _movementSpeed = 1;
        [SerializeField, BoxGroup("Settings")] private float _rotateSpeed = 1;

        public float MovementSpeed => _movementSpeed;
        public float RotateSpeed => _rotateSpeed;
        public Camera Camera  => _camera; 
    }
}
