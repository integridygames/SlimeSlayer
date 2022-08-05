using Game.Gameplay.Models.Level;
using Game.Gameplay.Models.Ammo;
using Game.Gameplay.Models.Weapon;
using Zenject;

namespace Game.Installers.SampleScene
{
    public class DataInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelInfo>().AsSingle();
            Container.Bind<WeaponsInfo>().AsSingle();
            Container.Bind<AmmoInfo>().AsSingle();
        }
    }
}