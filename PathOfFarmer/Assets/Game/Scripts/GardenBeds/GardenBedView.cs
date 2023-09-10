using Assets.Game.Scripts.Plants;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.GardenBeds
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class GardenBedView : MonoBehaviour, IInteractable
    {
        //[SerializeField] private Transform[] _points;
        [SerializeField] private Transform _anchorForPoint;
        [SerializeField] private PlantStatsConfig _plantConfig;

        //public Transform[] Points => _points;
        public PlantStatsConfig PlantConfig => _plantConfig;
        public Transform AnchorForPoint => _anchorForPoint;

        public event Action InteractedEvent = delegate { };

        public void Interact()
        {
            InteractedEvent.Invoke();
        }
    }
}