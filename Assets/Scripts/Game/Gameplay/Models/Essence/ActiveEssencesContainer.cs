using Game.Gameplay.Views.Essence;
using System;
using System.Collections.Generic;

namespace Game.Gameplay.Models.Essence 
{
    public class ActiveEssencesContainer
    {
        public event Action<EssenceView> OnEssenceCollide;

        private readonly List<EssenceView> _activeEssences = new();

        public IReadOnlyList<EssenceView> ActiveEssences => _activeEssences;

        public void AddEssence(EssenceView essenceView)
        {
            essenceView.OnEssenceCollide += OnEssenceCollide;

            _activeEssences.Add(essenceView);
        }

        public void RemoveEssence(EssenceView essenceView)
        {
            essenceView.OnEssenceCollide -= OnEssenceCollide;

            _activeEssences.Remove(essenceView);
        }
    }  
}