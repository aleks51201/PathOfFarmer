using UnityEngine;

namespace Assets.Game.Scripts.BuildStates
{
    public abstract class CellHolderBase : MonoBehaviour, IUi
    {
        [SerializeField] protected Transform _container;
        [SerializeField] protected BuildingInventoryCellView _cellPrefab;

        protected BuildinObjectConfig _buildConfig;
        protected UiMediator _uiMediator;

        public Transform Container => _container;

        protected abstract void CreateCells();
        protected abstract void OnSelected(BuildingInventoryCellView cell);

        public virtual void Initialize(UiMediator uiMediator)
        {
            _buildConfig = uiMediator.BuildinObjectConfig;
            _uiMediator = uiMediator;

            CreateCells();
        }
    }
}