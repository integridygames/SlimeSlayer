using Game.Gameplay.Controllers.Bullets;
using Game.Gameplay.Controllers.Character;
using Zenject;
using Game.Gameplay.Controllers.GameScreen;
using Game.Gameplay.Controllers.Essence;
using Game.Gameplay.Controllers.Enemy;

namespace Game.Installers.SampleScene
{
    public class ControllersInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindController<GameScreenController>();
            BindController<BulletsController>();
            BindController<CharacterHealthController>();
            BindController<EssencesController>();
            BindController<EnemiesController>();
        }

        private void BindController<TController>()
        {
            Container.BindInterfacesAndSelfTo<TController>()
                .AsSingle()
                .NonLazy();
        }
    }
}