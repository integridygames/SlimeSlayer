﻿using Game.Gameplay.Views.UI.Screens;
using Game.Gameplay.Views.UI.Screens.Weapons;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class WeaponScreenToStartTransition : TransitionBase
    {
        private readonly WeaponScreenView _weaponScreenView;
        protected override bool SmoothTransition => true;

        public WeaponScreenToStartTransition(StateBase stateFrom, StateBase stateTo,
            WeaponScreenView weaponScreenView) : base(stateFrom, stateTo)
        {
            _weaponScreenView = weaponScreenView;
        }

        public override void OnTransitionAdded()
        {
            _weaponScreenView.CloseButton.OnReleased += OnCloseButtonPressedHandler;
        }

        public override void OnTransitionRemoved()
        {
            _weaponScreenView.CloseButton.OnReleased -= OnCloseButtonPressedHandler;
        }

        private void OnCloseButtonPressedHandler()
        {
            DoTransition();
        }
    }
}