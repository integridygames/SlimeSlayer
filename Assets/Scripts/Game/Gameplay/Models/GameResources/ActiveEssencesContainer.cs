using System;
using Game.Gameplay.Views.GameResources;

namespace Game.Gameplay.Models.GameResources
{
    public class ActiveEssencesContainer : ActiveObjectsContainerBase<EssenceView>
    {
        public event Action<EssenceView> OnEssenceCollide;

        protected override void OnValueAdded(EssenceView value)
        {
            value.OnResourceCollide += OnResourceCollideHandler;
        }

        protected override void OnValueRemoved(EssenceView value)
        {
            value.OnResourceCollide -= OnResourceCollideHandler;
        }

        private void OnResourceCollideHandler(GameResourceViewBase gameResourceViewBase)
        {
            OnEssenceCollide?.Invoke((EssenceView) gameResourceViewBase);
        }
    }
}