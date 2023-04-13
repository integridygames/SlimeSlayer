using Game.Gameplay.Views.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class GameToPauseScreenTransition : TransitionBase
    {
        private readonly GameScreenView _gameScreenView;

        public GameToPauseScreenTransition(StateBase stateFrom, StateBase stateTo, GameScreenView gameScreenView) : base(stateFrom, stateTo)
        {
            _gameScreenView = gameScreenView;
        }

        public override void OnTransitionAdded()
        {
            _gameScreenView.ToPauseScreenButton.OnReleased += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _gameScreenView.ToPauseScreenButton.OnReleased -= DoTransition;
        }
    }
}