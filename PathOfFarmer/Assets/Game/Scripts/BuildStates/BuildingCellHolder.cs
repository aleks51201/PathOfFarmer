using UnityEngine;

namespace Assets.Game.Scripts.BuildStates
{
    public class BuildingCellHolder : MonoBehaviour, IUi
    {
        [SerializeField] private Transform _container;
        [SerializeField] private BuildingInventoryCellView _cellPrefab;

        private BuildinObjectConfig _buildConfig;

        public Transform Container => _container;

        public void Initialize(UiMediator uiMediator)
        {
            _buildConfig = uiMediator.BuildinObjectConfig;

            CreateCells();
        }

        private void CreateCells()
        {
            foreach(var item in _buildConfig.Builds)
            {
                var cell = Instantiate(_cellPrefab, _container);
                cell.Name.text = item.Name;
                cell.Cost.text = $"{item.Cost}";
                cell.Image.sprite = item.Icon;
            }
        }
    }
}