using Game.DataBase;
using Game.DataBase.Essence;
using Game.DataBase.FX;
using Game.DataBase.Weapon;
using UnityEngine;
using Zenject;

namespace Game.Installers 
{
    public class DataBaseInstaller : MonoInstaller
    {
        [SerializeField] private LevelsDataBase _levelsDataBase;
        [SerializeField] private WeaponsDataBase _weaponsDataBase;
        [SerializeField] private EssenceDataBase _essenceDataBase;
        [SerializeField] private RecyclableParticlesDataBase _recyclableParticlesDataBase;
        [SerializeField] private ProjectileDataBase _projectileDataBase;

        public override void InstallBindings()
        {
            Container.BindInstance(_levelsDataBase).AsSingle();
            Container.BindInstance(_weaponsDataBase).AsSingle();
            Container.BindInstance(_essenceDataBase).AsSingle();
            Container.BindInstance(_recyclableParticlesDataBase).AsSingle();
            Container.BindInstance(_projectileDataBase).AsSingle();
        }
    }
}