using Game.Services;
using Zenject;

namespace Game.Installers.SampleScene
{
    public class ServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<VibrationService>().AsSingle();
        }
    }
}