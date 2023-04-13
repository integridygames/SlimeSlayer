using Game.Gameplay.Views.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.States
{
    public class StatsScreenState : StateBase
    {
        private readonly StatsScreenView _statsScreenView;

        public StatsScreenState(StatsScreenView statsScreenView)
        {
            _statsScreenView = statsScreenView;
        }

        protected override void OnActivate()
        {
            _statsScreenView.gameObject.SetActive(true);
        }

        protected override void OnDeactivate()
        {
            _statsScreenView.gameObject.SetActive(false);
        }
    }
}