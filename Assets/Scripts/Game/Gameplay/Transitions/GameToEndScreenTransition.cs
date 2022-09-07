using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.Level;
using TegridyCore;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class GameToEndScreenTransition : TransitionBase
    {
        private readonly LevelInfo _levelInfo;

        public GameToEndScreenTransition(StateBase stateFrom, StateBase stateTo, LevelInfo levelInfo) : base(stateFrom, stateTo)
        {
            _levelInfo = levelInfo;
        }

        public override void OnTransitionAdded()
        {
            _levelInfo.CurrentLevelView.OnUpdate += OnLevelUpdateHandler;
        }

        private void OnLevelUpdateHandler(RxValue<LevelView> levelRxValue)
        {
            if (levelRxValue.OldValue != null)
            {
                levelRxValue.OldValue.FinishView.OnPlayerEntered -= DoTransition;
            }
            
            levelRxValue.NewValue.FinishView.OnPlayerEntered += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _levelInfo.CurrentLevelView.Value.FinishView.OnPlayerEntered -= DoTransition;
            _levelInfo.CurrentLevelView.OnUpdate -= OnLevelUpdateHandler;
        }
    }
}