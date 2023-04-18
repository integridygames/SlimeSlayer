using System;
using Game.Gameplay.Views.UI.Screens;
using TegridyCore.FiniteStateMachine;
using Zenject;

namespace Game.Gameplay.Transitions
{
    public class StartScreenToWeaponTransition : TransitionBase
    {
        private readonly StartScreenView _startScreenView;

        public StartScreenToWeaponTransition(StateBase stateFrom, StateBase stateTo, StartScreenView startScreenView)
            : base(stateFrom, stateTo)
        {
            _startScreenView = startScreenView;
        }

        public override void OnTransitionAdded()
        {
            _startScreenView.WeaponButton.OnReleased += DoTransition;
        }

        public override void OnTransitionRemoved()
        {
            _startScreenView.WeaponButton.OnReleased -= DoTransition;
        }
    }
}