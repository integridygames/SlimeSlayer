using TegridyCore.Base;
using Zenject;

namespace Game.Installers
{
    public class ViewsInstaller : MonoInstaller
    {      
        public override void InstallBindings()
        {
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