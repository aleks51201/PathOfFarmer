using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Game.Scripts.Inventories
{
    public class Inventory
    {
        private List<Cell> _cells = new();

        public event Action<Cell> CountSlotsUpdatedEvent = delegate { };

        public void Create()
        {
            for (var i = 0; i < 10; i++)
            {
                AddCell();
            }
        }

        private Cell AddCell()
        {
            var newCell = new Cell();

            _cells.Add(newCell);

            CountSlotsUpdatedEvent.Invoke(newCell);

            return newCell;
        }

        public void AddItem(IItem item)
        {
            Cell emptyCell = null;
            foreach(var cell in _cells)
            {
                if(cell.Count == 0)
                {
                    emptyCell = cell;
                }
            }

            if (emptyCell == null)
            {
                emptyCell = AddCell();
            }

            emptyCell.Item = item;
            emptyCell.Count = 1;
        }
    }
}