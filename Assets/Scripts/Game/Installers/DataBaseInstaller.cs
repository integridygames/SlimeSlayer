using Game.DataBase;
using Game.DataBase.Abilities;
using Game.DataBase.Character;
using Game.DataBase.Enemies;
using Game.DataBase.Essence;
using Game.DataBase.FX;
using Game.DataBase.Loot;
using Game.DataBase.Weapon;
using Game.Services;
using UnityEngine;
using UnityEngine.Serialization;
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
        [SerializeField] private AbilitiesDataBase _abilitiesDataBase;
        [SerializeField] private HealthBarsDataBase _healthBarsDataBase;
        [SerializeField] private EnemiesSpawnSettingsDataBase _enemiesSpawnSettingsDataBase;
        [FormerlySerializedAs("_lootDataBase")] [SerializeField] private WeaponLootDataBase weaponLootDataBase;

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
            Container.BindInstance(_abilitiesDataBase).AsSingle();
            Container.BindInstance(_healthBarsDataBase).AsSingle();
            Container.BindInstance(_enemiesSpawnSettingsDataBase).AsSingle();
            Container.BindInstance(weaponLootDataBase).AsSingle();
        }
    }
}