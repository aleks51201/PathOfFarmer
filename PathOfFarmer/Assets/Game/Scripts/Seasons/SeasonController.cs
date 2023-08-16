using System;
using UnityEngine.InputSystem;

namespace Assets.Game.Scripts.Seasons
{
    public class SeasonController 
    {
        private int _season;
        private CustomInput _customInput;

        public SeasonController()
        {
            _customInput = new CustomInput();
            _customInput.Enable();

            _customInput.Player.Season.performed += OnPerformed;
        }

        public int Season 
        {
            get => _season;
            private set
            {
                _season = value;

                UpdatedEvent.Invoke(_season);
            }
        }

        public event Action<int> UpdatedEvent = delegate { };

        private void OnPerformed(InputAction.CallbackContext context)
        {
            Increase();
        }

        public void Increase()
        {
            Season++;
        }
    }
}