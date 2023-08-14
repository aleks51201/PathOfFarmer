using System;

namespace Assets.Game.Scripts.Seasons
{
    public class SeasonController 
    {
        private int _season;
        public int Season 
        {
            get => _season;
            private set
            {
                _season = value;

                UpdatedEvent.Invoke(_season);
            }
        }

        public event Action<int> UpdatedEvent = delegate { };


        public void Increase()
        {
            Season++;
        }
    }
}