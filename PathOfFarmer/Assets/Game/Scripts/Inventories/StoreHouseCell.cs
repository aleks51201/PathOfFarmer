﻿using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Game.Scripts.Inventories
{
    public class StoreHouseCell
    {
        private readonly StoreHouseCellView _prefab;
        private readonly Transform _parentTransform;
        private readonly Cell _cell;
        private StoreHouseCellView _cellView;

        public StoreHouseCell(StoreHouse storeHouse, Cell cell)
        {
            _prefab = storeHouse.StoreHouseView.StoreHouseCellView;
            _parentTransform = storeHouse.StoreHouseView.Container;
            _cell = cell;

            _cell.ItemCountUpdatedEvent += UpdateData;
            _cell.ItemUpdatedEvent += UpdateData;
        }

        private void UpdateData(int value)
        {
            if (value == 0)
            {
                SetViewEmpty();
            }

            SetViewFull(_cell);
        }

        private void UpdateData(IItem item)
        {
            if (item == null)
            {
                SetViewEmpty();
            }

            SetViewFull(_cell);
        }

        public void Spawn()
        {
            _cellView = Object.Instantiate(_prefab, _parentTransform);
        }

        private void SetViewFull(Cell cell)
        {
            _cellView.Name.gameObject.SetActive(true);
            _cellView.Name.text = cell.Item.Config.Name;

            _cellView.Count.gameObject.SetActive(true);
            _cellView.Count.text = $"{cell.Count}";

            _cellView.Image.gameObject.SetActive(true);
            _cellView.Image.sprite = cell.Item.Config.Icon;
        }

        private void SetViewEmpty()
        {
            _cellView.Name.gameObject.SetActive(false);

            _cellView.Count.gameObject.SetActive(false);

            _cellView.Image.gameObject.SetActive(false);
        }
    }
}