using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Game.Scripts.Players
{
    public class Mover
    {
        private readonly Player _player;
        private CustomInput _customInput;
        private Vector2 _direction;

        public Mover(Player player)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _customInput = new CustomInput();
            Speed = player.PlayerView.Speed;
        }

        public float Speed { get; set; }

        public void Start()
        {
            _customInput.Enable();

            _customInput.Player.Move.performed += OnPerfomed;
            _customInput.Player.Move.canceled += OnCanceled;
        }

        public void Stop()
        {
            _customInput.Player.Move.performed -= OnPerfomed;
            _customInput.Player.Move.canceled -= OnCanceled;
        }

        private void OnPerfomed(CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
        }

        private void OnCanceled(CallbackContext context)
        {
            _direction = Vector2.zero;
        }

        public void Move()
        {
            var direction = new Vector3(_direction.x, 0, _direction.y);

            _player.PlayerView.transform.position += Time.fixedDeltaTime * Speed * direction;
        }
    }
}
