using System;
using Game.Gameplay.Views.Screens;
using TegridyCore.FiniteStateMachine;
using Zenject;

namespace Game.Gameplay.Transitions
{
    public class StartScreenToInventoryTransition : TransitionBase
    {
        private readonly StartScreenView _startScreenView;

        public StartScreenToInventoryTransition(StateBase stateFrom, StateBase stateTo, StartScreenView startScreenView)
            : base(stateFrom, stateTo)
        {
            _startScreenView = startScreenView;
        }

        public override void OnTransitionAdded()
        {
            _startScreenView.InvButton.OnReleased += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _startScreenView.InvButton.OnReleased -= DoTransition;
        }
    }
}