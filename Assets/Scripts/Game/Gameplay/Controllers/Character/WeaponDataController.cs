using System;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.WeaponMechanics;
using Game.Services;
using TegridyCore;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Character
{
    public class WeaponDataController : ControllerBase<CurrentCharacterWeaponsData>, IInitializable, IDisposable
    {
        private readonly ApplicationData _applicationData;

        public WeaponDataController(CurrentCharacterWeaponsData controlledEntity, ApplicationData applicationData) :
            base(controlledEntity)
        {
            _applicationData = applicationData;
        }

        public void Initialize()
        {
            ControlledEntity.CurrentWeaponViewLeft.OnUpdate += SaveLeft;
            ControlledEntity.CurrentWeaponViewRight.OnUpdate += SaveRight;
        }

        public void Dispose()
        {
            ControlledEntity.CurrentWeaponViewLeft.OnUpdate -= SaveLeft;
            ControlledEntity.CurrentWeaponViewRight.OnUpdate -= SaveRight;
        }

        private void SaveLeft(RxValue<WeaponBase> rxValue)
        {
            _applicationData.PlayerData.CurrentLeftWeaponIndex =
                _applicationData.PlayerData.WeaponsSaveData.IndexOf(rxValue.NewValue.Data);

            SavePlayerData();
        }

        private void SaveRight(RxValue<WeaponBase> rxValue)
        {
            _applicationData.PlayerData.CurrentRightWeaponIndex =
                _applicationData.PlayerData.WeaponsSaveData.IndexOf(rxValue.NewValue.Data);

            SavePlayerData();
        }

        private void SavePlayerData()
        {
            SaveLoadDataService.Save(_applicationData.PlayerData);
        }
    }
}