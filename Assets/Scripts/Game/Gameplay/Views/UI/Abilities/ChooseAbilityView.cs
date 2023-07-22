using System;
using System.Collections.Generic;
using Game.DataBase.Abilities;
using Game.Gameplay.Models.Abilities;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.UI.Abilities
{
    public class ChooseAbilityView : ViewBase
    {
        public event Action<AbilityRecord> OnAbilitySelected;

        [SerializeField] private List<AbilityView> _abilityViews;

        public IReadOnlyList<AbilityView> AbilityViews => _abilityViews;

        protected override void OnEnable()
        {
            foreach (var abilityView in AbilityViews)
            {
                abilityView.OnAbilitySelected += OnAbilitySelected;
            }
        }

        protected override void OnDisable()
        {
            foreach (var abilityView in AbilityViews)
            {
                abilityView.OnAbilitySelected -= OnAbilitySelected;
            }
        }
    }
}