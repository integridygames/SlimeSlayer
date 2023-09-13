using System;
using Game.Gameplay.Views.GameResources;
using Object = UnityEngine.Object;

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

        public void Clear()
        {
            foreach (var coinView in ValuesInternal)
            {
                Object.Destroy(coinView.gameObject);
            }

            ValuesInternal.Clear();
        }
    }
}