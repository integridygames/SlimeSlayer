using System;
using Game.DataBase;
using Game.DataBase.Loot;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Services;
using Game.Gameplay.TrashArchitecture;
using Game.Gameplay.Views.UI.Screens;
using Game.Services;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.GameScreen
{
    public class EndScreenController : ControllerBase<EndScreenView>, IInitializable, IDisposable
    {
        private readonly SpawnerRepository _spawnerRepository;
        private readonly WeaponLootDataBase _weaponLootDataBase;
        private readonly WeaponsService _weaponsService;
        private readonly ApplicationData _applicationData;
        private readonly LevelInfo _levelInfo;

        public EndScreenController(EndScreenView controlledEntity, SpawnerRepository spawnerRepository, 
            WeaponLootDataBase weaponLootDataBase, WeaponsService weaponsService, ApplicationData applicationData, LevelInfo levelInfo) : base(controlledEntity)
        {
            _spawnerRepository = spawnerRepository;
            _weaponLootDataBase = weaponLootDataBase;
            _weaponsService = weaponsService;
            _applicationData = applicationData;
            _levelInfo = levelInfo;
        }

        public void Initialize()
        {
            _spawnerRepository.OnAllWaveCompleted += AllWaveCompletedHandler;
        }

        public void Dispose()
        {
            _spawnerRepository.OnAllWaveCompleted -= AllWaveCompletedHandler;
        }

        private void AllWaveCompletedHandler()
        {
            var playerWeaponsData = _weaponLootDataBase.GetLootForLevel(LevelType.LevelWithDemons);
            ControlledEntity.WeaponCardsView.ShowWeaponCards(playerWeaponsData, _weaponsService);
            
            _applicationData.PlayerData.WeaponsSaveData.AddRange(playerWeaponsData);
            SaveLoadDataService.Save(_applicationData.PlayerData);
            
            ControlledEntity.GoldEarnedValue.text = $"+{_levelInfo.GoldEarned}";
        }
    }
}