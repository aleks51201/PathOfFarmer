using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Inventories
{
    public class Inventory
    {
        private List<Cell> _cells = new();
        private int _masSlots;

        public int MaxSlots
        {
            get => _masSlots;
            private set
            {
                _masSlots = value;

                CountSlotsUpdatedEvent.Invoke(_masSlots);
            }
        }

        public event Action<int> CountSlotsUpdatedEvent = delegate { };

        public void AddSlots(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException($"Value = {count}. Value cannot be less zero");

            MaxSlots += count;
        }

        public void Spawn()
        {
            for(var i = 0; i < MaxSlots; i++)
            {
                _cells.Add(new Cell());
            }
        }
    }
}