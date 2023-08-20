namespace Assets.Game.Scripts.Inventories
{
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
            var storeHouseCell = new StoreHouseCell(_storeHouse, cell);
            storeHouseCell.Spawn();
        }
    }
}