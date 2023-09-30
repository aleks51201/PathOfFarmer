using Assets.Game.Scripts.Plants;
using System;
using System.Collections.Generic;

namespace Assets.Game.Scripts.BuildStates
{
    public class PlantsCellHolder : CellHolderBase
    {
        private Dictionary<BuildingInventoryCellView, PlantStatsConfig> _buildObjectMap = new();

        public event Action<PlantStatsConfig> BuildObjectSelectedEvent = delegate { };

        protected override void CreateCells()
        {
            foreach (var item in _buildConfig.Plants)
            {
                BuildingInventoryCellView cell = Instantiate(_cellPrefab, _container);
                cell.Name.text = item.Name;
                cell.Cost.text = $"{item.Cost}";
                cell.Image.sprite = item.Icon;

                _buildObjectMap.Add(cell, item.PlantStatsConfig);

                cell.ButtonClickedEvent += OnSelected;
            }
        }

        protected override void OnSelected(BuildingInventoryCellView cell)
        {
            _uiMediator.OnPlantsSelected(_buildObjectMap[cell]);
            BuildObjectSelectedEvent.Invoke(_buildObjectMap[cell]);
        }
    }
}