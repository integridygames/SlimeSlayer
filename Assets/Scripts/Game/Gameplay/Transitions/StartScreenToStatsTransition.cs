using System;
using Game.Gameplay.Views.UI.Screens;
using TegridyCore.FiniteStateMachine;
using Zenject;

namespace Game.Gameplay.Transitions
{
    public class StartScreenToStatsTransition : TransitionBase
    {
        private readonly StartScreenView _startScreenView;

        public StartScreenToStatsTransition(StateBase stateFrom, StateBase stateTo, StartScreenView startScreenView) : base(stateFrom, stateTo)
        {
            _startScreenView = startScreenView;
        }

        public override void OnTransitionAdded()
        {
            _startScreenView.StatsButton.OnReleased += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _startScreenView.StatsButton.OnReleased -= DoTransition;
        }
    }
}