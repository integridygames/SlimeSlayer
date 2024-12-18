﻿using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Services;
using Game.Services;
using Zenject;

namespace Game.Installers.SampleScene
{
    public class ServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<VibrationService>().AsSingle();
            Container.Bind<CharacterRespawnService>().AsSingle();
            Container.Bind<WeaponMechanicsService>().AsSingle();
            Container.Bind<WeaponsService>().AsSingle();
            Container.Bind<AbilitiesDistributorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraShakeService>().AsSingle();
            Container.Bind<DamageFxService>().AsSingle();
            Container.Bind<LevelDestroyService>().AsSingle();
        }
    }
}