﻿using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace Assets.Game.Scripts.Players
{
    public class Rotator
    {
        private readonly Player _player;
        private CustomInput _customInput;
        private Vector2 _direction;

        public Rotator(Player player, CustomInput input)
        {
            _player = player ?? throw new ArgumentNullException(nameof(player));
            _customInput = input;
            Speed = player.PlayerView.RotateSpeed;
        }

        public float Speed { get; set; }

        public void Start()
        {
            _customInput.Player.Look.performed += OnPerfomed;
            _customInput.Player.Look.canceled += OnCanceled;
        }

        public void Stop()
        {
            _customInput.Player.Look.performed -= OnPerfomed;
            _customInput.Player.Look.canceled -= OnCanceled;
        }

        private void OnPerfomed(CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>();
        }

        private void OnCanceled(CallbackContext context)
        {
            _direction = Vector2.zero;
        }

        public void Rotate()
        {
            HorizontalRotate();
        }

        private void HorizontalRotate()
        {
            var direction = new Vector2(_direction.y * -1, _direction.x);

            _player.PlayerView.transform.Rotate(Time.fixedDeltaTime * Speed * direction * Vector2.up);
        }
    }
}
