using Game.Gameplay.Views.UI;
using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Installers
{
    public class ViewsInstaller : MonoInstaller
    {
        [SerializeField] private DamageFx _damageFx;

        public override void InstallBindings()
        {
            Container.BindInstance(_damageFx).AsSingle();

            InstallViews();
        }

        private void InstallViews()
        {
            var views = FindObjectsOfType<ViewBase>(true);

            foreach (var view in views)
            {
                var binder = Container.Bind(view.GetType()).FromInstance(view);

                if (view.IsCached)
                {
                    binder.AsCached();
                }
                else
                {
                    binder.AsSingle();
                }
            }
        }
    }
}