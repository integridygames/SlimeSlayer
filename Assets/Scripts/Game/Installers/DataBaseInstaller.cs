using Game.DataBase;
using Game.DataBase.Enemies;
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
        [SerializeField] private EnemyDataBase _enemyDataBase;

        public override void InstallBindings()
        {
            Container.BindInstance(_levelsDataBase).AsSingle();
            Container.BindInstance(_weaponsDataBase).AsSingle();
            Container.BindInstance(_essenceDataBase).AsSingle();
            Container.BindInstance(_recyclableParticlesDataBase).AsSingle();
            Container.BindInstance(_projectileDataBase).AsSingle();
            Container.BindInstance(_enemyDataBase).AsSingle();
        }
    }
}