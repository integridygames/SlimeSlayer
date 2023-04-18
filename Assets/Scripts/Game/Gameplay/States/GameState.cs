using Game.Gameplay.Views.UI.Screens;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.States
{
    public class GameState : StateBase
    {
        private readonly GameScreenView _gameScreenView;

        public GameState(GameScreenView gameScreenView)
        {
            _gameScreenView = gameScreenView;
        }

        protected override void OnActivate()
        {
            _gameScreenView.gameObject.SetActive(true);
        }

        protected override void OnDeactivate()
        {
            _gameScreenView.gameObject.SetActive(false);
        }
    }
}