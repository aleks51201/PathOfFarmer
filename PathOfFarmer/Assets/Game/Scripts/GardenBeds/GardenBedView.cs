using System;
using UnityEngine;

namespace Assets.Game.Scripts.GardenBeds
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class GardenBedView : MonoBehaviour,IInteractable
    {
        [SerializeField] private Transform[] _points;

        public Transform[] Points => _points;

        public event Action InteractedEvent = delegate { };

        public void Interact()
        {
            InteractedEvent.Invoke();
        }
    }
}