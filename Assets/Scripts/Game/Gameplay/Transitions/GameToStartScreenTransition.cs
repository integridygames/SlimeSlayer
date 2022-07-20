using Game.Gameplay.Views.SampleScene.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class GameToStartScreenTransition : TransitionBase
    {
        private readonly GameScreenView _gameScreenView;

        public GameToStartScreenTransition(StateBase stateFrom, StateBase stateTo, GameScreenView gameScreenView) :
            base(stateFrom, stateTo)
        {
            _gameScreenView = gameScreenView;
        }

        public override void OnTransitionAdded()
        {
            _gameScreenView.ToStartScreenButton.OnReleased += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _gameScreenView.ToStartScreenButton.OnReleased -= DoTransition;
        }
    }
}