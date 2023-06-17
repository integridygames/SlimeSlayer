using Game.DataBase;
using Game.DataBase.Character;
using Game.DataBase.Enemies;
using Game.DataBase.Essence;
using Game.DataBase.FX;
using Game.DataBase.Weapon;
using Game.Services;
using UnityEngine;
using Zenject;

namespace Game.Installers 
{
    public class DataBaseInstaller : MonoInstaller
    {
        [SerializeField] private LevelsDataBase _levelsDataBase;
        [SerializeField] private WeaponsDataBase _weaponsDataBase;
        [SerializeField] private GameResourcesDataBase _gameResourcesDataBase;
        [SerializeField] private RecyclableParticlesDataBase _recyclableParticlesDataBase;
        [SerializeField] private ProjectileDataBase _projectileDataBase;
        [SerializeField] private EnemyDataBase _enemyDataBase;
        [SerializeField] private WeaponCardsDataBase _weaponCardsDataBase;
        [SerializeField] private ResourceShortFormsDataBase _resourceShortFormsDataBase;
        [SerializeField] private CharacterCharacteristicsDataBase _characterCharacteristicsDataBase;

        public override void InstallBindings()
        {
            Container.BindInstance(_levelsDataBase).AsSingle();
            Container.BindInstance(_weaponsDataBase).AsSingle();
            Container.BindInstance(_gameResourcesDataBase).AsSingle();
            Container.BindInstance(_recyclableParticlesDataBase).AsSingle();
            Container.BindInstance(_projectileDataBase).AsSingle();
            Container.BindInstance(_enemyDataBase).AsSingle();
            Container.BindInstance(_weaponCardsDataBase).AsSingle();
            Container.BindInstance(_resourceShortFormsDataBase).AsSingle();
            Container.BindInstance(_characterCharacteristicsDataBase).AsSingle();
        }
    }
}