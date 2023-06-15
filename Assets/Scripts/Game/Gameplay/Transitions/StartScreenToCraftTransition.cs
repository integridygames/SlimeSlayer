using Game.Gameplay.Views.UI.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class StartScreenToCraftTransition : TransitionBase
    {
        private readonly StartScreenView _startScreenView;

        public StartScreenToCraftTransition(StateBase stateFrom, StateBase stateTo, StartScreenView startScreenView) : base(stateFrom, stateTo)
        {
            _startScreenView = startScreenView;
        }

        public override void OnTransitionAdded()
        {
            _startScreenView.CraftButton.OnReleased += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _startScreenView.CraftButton.OnReleased -= DoTransition;
        }
    }
}