using Game.Gameplay.Controllers.Character;
using Zenject;
using Game.Gameplay.Controllers.GameScreen;
using Game.Gameplay.Controllers.Enemy;
using Game.Gameplay.Controllers.GameResources;

namespace Game.Installers.SampleScene
{
    public class ControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindController<GameScreenController>();
            BindController<CharacterHealthController>();
            BindController<ActiveEssencesController>();
            BindController<EnemiesController>();
            BindController<WeaponReloadController>();
            BindController<WeaponScreenController>();
            BindController<WeaponDataController>();
            BindController<CoinsCollectController>();
            BindController<CoinsViewController>();
            BindController<CoinsChangingController>();
            BindController<CraftScreenController>();
        }

        private void BindController<TController>()
        {
            Container.BindInterfacesAndSelfTo<TController>()
                .AsSingle()
                .NonLazy();
        }
    }
}