using Game.Gameplay.Controllers.SampleScene;
using Game.Gameplay.Controllers.Character.IK;
using Zenject;

namespace Game.Installers.SampleScene
{
    public class ControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<DayTimeController>().AsSingle();
            Container.BindInterfacesAndSelfTo<InverseKinematicsLeftHandController>().AsSingle();
            Container.BindInterfacesAndSelfTo<InverseKinematicsRightHandController>().AsSingle();
        }
    }
}