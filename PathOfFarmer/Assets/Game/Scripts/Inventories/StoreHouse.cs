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

        public void AddItem(IItem item, bool merge = true)
        {
            _inventory.AddItem(item, merge);
        }

        public void DeleteItem()
        {

        }
    }
}