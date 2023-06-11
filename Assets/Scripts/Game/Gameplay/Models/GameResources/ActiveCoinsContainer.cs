using System;
using Game.Gameplay.Views.GameResources;

namespace Game.Gameplay.Models.GameResources
{
    public class ActiveCoinsContainer : ActiveObjectsContainerBase<CoinView>
    {
        public event Action<CoinView> OnCoinCollide;

        protected override void OnValueAdded(CoinView value)
        {
            value.OnResourceCollide += OnResourceCollideHandler;
        }

        protected override void OnValueRemoved(CoinView value)
        {
            value.OnResourceCollide -= OnResourceCollideHandler;
        }

        private void OnResourceCollideHandler(GameResourceViewBase gameResourceViewBase)
        {
            OnCoinCollide?.Invoke((CoinView) gameResourceViewBase);
        }
    }
}