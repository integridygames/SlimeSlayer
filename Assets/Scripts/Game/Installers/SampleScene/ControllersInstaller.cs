using Game.Gameplay.Controllers.SampleScene;
using Zenject;

namespace Game.Installers.SampleScene
{
    public class ControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DayTimeController>().AsSingle();
        }
    }
}