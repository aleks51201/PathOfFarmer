using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Game.Scripts.Inventories
{
    public class Inventory
    {
        private List<Cell> _cells = new();

        public Inventory()
        {
        }

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

        public void AddItem(IItem item,bool merge)
        {
            Cell emptyCell = null;

            if (merge)
            {
                if (HaveEqualItem(item))
                {
                    emptyCell = FindCellWithEqualItem(item);
                }
            }
            else
            {
                foreach (var cell in _cells)
                {
                    if (cell.Count == 0)
                    {
                        emptyCell = cell;
                    }
                }
            }

            if (emptyCell == null)
            {
                emptyCell = AddCell();
            }

            emptyCell.Item = item;
            emptyCell.Count += 1;
        }

        private bool HaveEqualItem(IItem item)
        {
            return _cells.Any(cell => cell.Item.Config.Name == item.Config.Name);
        }

        private Cell FindCellWithEqualItem(IItem item)
        {
            return _cells.First(cell => cell.Item.Config.Name == item.Config.Name);
        }
    }
}