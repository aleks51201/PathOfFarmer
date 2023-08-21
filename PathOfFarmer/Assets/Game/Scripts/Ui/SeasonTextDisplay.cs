namespace Assets.Game.Scripts.Ui
{
    public class SeasonTextDisplay : TextDisplay
    {
        public override void Initialize(UiMediator uiMediator)
        {
            uiMediator.SeasonController.UpdatedEvent += OnValueUpdated;

            OnValueUpdated(0);
        }

        private void OnValueUpdated(int value)
        {
            _text.text = $"{value}";
        }
    }
}
