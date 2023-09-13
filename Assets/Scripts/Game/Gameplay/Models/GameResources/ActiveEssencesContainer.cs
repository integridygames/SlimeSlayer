using System;
using Game.Gameplay.Views.GameResources;
using Object = UnityEngine.Object;

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

        public void Clear()
        {
            foreach (var view in ValuesInternal)
            {
                Object.Destroy(view.gameObject);
            }

            ValuesInternal.Clear();
        }
    }
}