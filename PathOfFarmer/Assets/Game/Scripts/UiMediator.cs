﻿using UnityEngine;

namespace Assets.Game.Scripts
{
    public class UiMediator : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _monoBehaviours;

        public void Initialize()
        {

            DependencyInjections();
        }

        private void DependencyInjections()
        {
            foreach (var monoBehaviour in _monoBehaviours)
            {
                if (monoBehaviour is IUi ui)
                {
                    ui.Initialize(this); ;
                }
            }
        }
    }
}