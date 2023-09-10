﻿using Assets.Game.Scripts.Builders;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.BuildStates
{
    public class BuildingCellHolder : MonoBehaviour, IUi
    {
        [SerializeField] private Transform _container;
        [SerializeField] private BuildingInventoryCellView _cellPrefab;

        private BuildinObjectConfig _buildConfig;
        private UiMediator _uiMediator;
        private Dictionary<BuildingInventoryCellView, BuildObjects> _buildObjectMap = new();

        public Transform Container => _container;

        public event Action<BuildObjects> BuildObjectSelectedEvent = delegate { };

        public void Initialize(UiMediator uiMediator)
        {
            _buildConfig = uiMediator.BuildinObjectConfig;
            _uiMediator = uiMediator;

            CreateCells();
        }

        private void CreateCells()
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

        private void OnSelected(BuildingInventoryCellView cell)
        {
            _uiMediator.OnBuildObjectSelected(_buildObjectMap[cell]);
            BuildObjectSelectedEvent.Invoke(_buildObjectMap[cell]);
        }
    }

    public struct BuildObjects
    {
        public BuildObjectView GhostPrefab;
        public BuildObjectView BuilingObjectPrefab;

        public BuildObjects(BuildObjectView ghostPrefab, BuildObjectView builingObjectPrefab)
        {
            GhostPrefab = ghostPrefab;
            BuilingObjectPrefab = builingObjectPrefab;
        }
    }
}