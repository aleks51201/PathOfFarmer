using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;
using Object = UnityEngine.Object;

namespace Assets.Game.Scripts.Inventories
{
    public class StoreHouse
    {
        private Inventory _inventory;
        private SlotViewUpdater _slotViewUpdater;

        public StoreHouse(StoreHouseView storeHouseView)
        {
            _inventory = new Inventory();
            _slotViewUpdater = new SlotViewUpdater(_inventory, this);
            StoreHouseView = storeHouseView;
        }

        public StoreHouseView StoreHouseView { get; }

        public void GetItem()
        {

        }

        public void AddItem(IItem item)
        {
            _inventory.AddItem(item);
        }

        public void DeleteItem()
        {

        }
    }

    public class SlotViewUpdater
    {
        private readonly Inventory _inventory;
        private readonly StoreHouse _storeHouse;

        public SlotViewUpdater(Inventory inventory, StoreHouse storeHouse)
        {
            _inventory = inventory;
            _storeHouse = storeHouse;

            inventory.CountSlotsUpdatedEvent += OnCountSlotsUpdated;
        }

        private void OnCountSlotsUpdated(Cell cell)
        {
            var storeHouseCell = new StoreHouseCell(_storeHouse);
        }
    }

    public class StoreHouseCell
    {
        private readonly StoreHouseCellView _prefab;
        private readonly Transform _parentTransform;
        private StoreHouseCellView _cellView;

        public StoreHouseCell(StoreHouse storeHouse)
        {
            _prefab = storeHouse.StoreHouseView.StoreHouseCellView;
            _parentTransform = storeHouse.StoreHouseView.Container;
        }

        public void Spawn()
        {
            _cellView = Object.Instantiate(_prefab, _parentTransform);
        }

        public void UpdateData(Cell cell)
        {
            if(cell == null)
            {
                SetViewEmpty();
                return;
            }

            SetViewFull(cell);
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