using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.Level;
using TegridyCore;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class GameToGunCabinetTransition : TransitionBase
    {
        private readonly LevelInfo _levelInfo;

        public GameToGunCabinetTransition(StateBase stateFrom, StateBase stateTo, LevelInfo levelInfo) : base(
            stateFrom, stateTo)
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
                levelRxValue.OldValue.GunCabinetView.OnPlayerEntered -= DoTransition;
            }
            
            levelRxValue.NewValue.GunCabinetView.OnPlayerEntered += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _levelInfo.CurrentLevelView.Value.GunCabinetView.OnPlayerEntered -= DoTransition;
            _levelInfo.CurrentLevelView.OnUpdate -= OnLevelUpdateHandler;
        }
    }
}