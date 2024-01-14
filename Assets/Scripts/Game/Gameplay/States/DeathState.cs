using Game.Gameplay.Views.UI.Screens.Gameplay;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.States
{
    public class DeathState : StateBase
    {
        private readonly DeathScreenView _deathScreenView;

        public DeathState(DeathScreenView deathScreenView)
        {
            _deathScreenView = deathScreenView;
        }

        protected override void OnActivate()
        {
            _deathScreenView.gameObject.SetActive(true);
        }


        protected override void OnDeactivate()
        {
            _deathScreenView.gameObject.SetActive(false);
        }
    }
}