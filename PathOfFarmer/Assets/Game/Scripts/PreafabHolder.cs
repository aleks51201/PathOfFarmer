using Assets.Game.Scripts.Plants;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class PreafabHolder : ScriptableObject
    {
        [SerializeField] private PlantView[] _plantViews;

        public PlantView[] PlantViews => _plantViews;
    }
}
