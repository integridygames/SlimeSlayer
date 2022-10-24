using Game.Gameplay.Controllers.Character;
using Zenject;
using Game.Gameplay.Controllers.GameScreen;
using Game.Gameplay.Controllers.Essence;
using Game.Gameplay.Controllers.Enemy;
using Game.Gameplay.Controllers.Zone.ZoneTransit;

namespace Game.Installers.SampleScene
{
    public class ControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindController<GameScreenController>();
            BindController<CharacterHealthController>();
            BindController<ActiveEssencesController>();
            BindController<EssenceImageViewsController>();
            BindController<EnemiesController>();
            BindController<ZoneTransitController>();
            BindController<WeaponReloadController>();
        }

        private void BindController<TController>()
        {
            Container.BindInterfacesAndSelfTo<TController>()
                .AsSingle()
                .NonLazy();
        }
    }
}