using Game.Gameplay.Views.UI.Screens.Gameplay;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class DeathToStartScreenTransition : TransitionBase
    {
        private readonly DeathScreenView _deathScreenView;

        public DeathToStartScreenTransition(StateBase stateFrom, StateBase stateTo, DeathScreenView deathScreenView) : base(stateFrom, stateTo)
        {
            _deathScreenView = deathScreenView;
        }

        public override void OnTransitionAdded()
        {
            _deathScreenView.OnCloseButtonPressed += DoTransition;
        }


        public override void OnTransitionRemoved()
        {
            _deathScreenView.OnCloseButtonPressed -= DoTransition;
        }
    }
}