using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class GameToPauseScreenTransition : TransitionBase
    {
        public GameToPauseScreenTransition(StateBase stateFrom, StateBase stateTo) : base(stateFrom, stateTo)
        {
        }

        public override void OnTransitionAdded()
        {

        }

        public override void OnTransitionRemoved()
        {

        }
    }
}