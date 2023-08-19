using Assets.Game.Scripts.Plants;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Inventories
{
    public class Cell
    {
        private int _count;
        private IItem _item;

        public Cell()
        { 
        }

        public IItem Item
        {
            get => _item;
            set
            {
                _item = value;

                ItemUpdatedEvent.Invoke(_item);
            }
        }

        public int Count
        {
            get => _count;
            set
            {
                _count = Mathf.Max(0, value);

                ItemCountUpdatedEvent.Invoke(_count);
            }
        }

        public event Action<IItem> ItemUpdatedEvent= delegate { };
        public event Action<int> ItemCountUpdatedEvent = delegate { };
    }

    public interface IItem
    {
        public BaseStoreHouseCellConfig Config { get; }
    }
}