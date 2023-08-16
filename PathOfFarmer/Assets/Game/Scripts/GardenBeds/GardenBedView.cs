using Assets.Game.Scripts.Plants;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.GardenBeds
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class GardenBedView : MonoBehaviour,IInteractable
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private PlantView _prefab;

        public Transform[] Points => _points;
        public PlantView Prefab => _prefab; 

        public event Action InteractedEvent = delegate { };

        public void Interact()
        {
            InteractedEvent.Invoke();
        }
    }
}