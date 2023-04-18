using Game.Gameplay.Views.UI.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class StartScreenToGameTransition : TransitionBase
    {
        private readonly StartScreenView _startScreenView;

        public StartScreenToGameTransition(StateBase stateFrom, StateBase stateTo, StartScreenView startScreenView) :
            base(stateFrom, stateTo)
        {
            _startScreenView = startScreenView;
        }

        public override void OnTransitionAdded()
        {
            _startScreenView.StartGameButton.OnReleased += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _startScreenView.StartGameButton.OnReleased -= DoTransition;
        }
    }
}