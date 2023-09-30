using Assets.Game.Scripts.Builders;
using Assets.Game.Scripts.Plants;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.BuildStates
{
    public class BuildingCellHolder :CellHolderBase 
    {
        private Dictionary<BuildingInventoryCellView, BuildObjects> _buildObjectMap = new();

        public event Action<BuildObjects> BuildObjectSelectedEvent = delegate { };

        protected override void CreateCells()
        {
            foreach (var item in _buildConfig.Builds)
            {
                BuildingInventoryCellView cell = Instantiate(_cellPrefab, _container);
                cell.Name.text = item.Name;
                cell.Cost.text = $"{item.Cost}";
                cell.Image.sprite = item.Icon;

                _buildObjectMap.Add(cell, new BuildObjects(item.GhostPrefab, item.BuilingObjectPrefab));

                cell.ButtonClickedEvent += OnSelected;
            }
        }

        protected override void OnSelected(BuildingInventoryCellView cell)
        {
            _uiMediator.OnBuildObjectSelected(_buildObjectMap[cell]);
            BuildObjectSelectedEvent.Invoke(_buildObjectMap[cell]);
        }
    }

    public struct BuildObjects
    {
        public BuildObjectView GhostPrefab;
        public GameObject BuilingObjectPrefab;

        public BuildObjects(BuildObjectView ghostPrefab, GameObject builingObjectPrefab)
        {
            GhostPrefab = ghostPrefab;
            BuilingObjectPrefab = builingObjectPrefab;
        }
    }
}