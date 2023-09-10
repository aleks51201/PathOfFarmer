using Assets.Game.Scripts.Builders;
using Assets.Game.Scripts.Ui;
using Sirenix.OdinInspector;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.BuildStates
{
    [CreateAssetMenu(fileName ="BuildinObjectConfig", menuName = "SO/BuildinObjectConfig")]
    public class BuildinObjectConfig : ScriptableObject
    {
        [SerializeField] private BuildingItemData[] _builds;
        [SerializeField] private BuildingItemData[] _plants;

        public BuildingItemData[] Builds => _builds;
        public BuildingItemData[] Plants => _plants; 
    }

    [Serializable]
    public struct BuildingItemData
    {
        public string Name;
        [PreviewField] public Sprite Icon;
        public int Cost;
        public BuildObjectView BuildingObjectPrefab;
    }
}