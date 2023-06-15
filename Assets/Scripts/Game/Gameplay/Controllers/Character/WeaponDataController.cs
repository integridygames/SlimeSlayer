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
        private readonly WeaponsCharacteristics _weaponsCharacteristics;
        private readonly ApplicationData _applicationData;

        public WeaponDataController(CurrentCharacterWeaponsData controlledEntity, WeaponsCharacteristics weaponsCharacteristics, ApplicationData applicationData) :
            base(controlledEntity)
        {
            _weaponsCharacteristics = weaponsCharacteristics;
            _applicationData = applicationData;
        }

        public void Initialize()
        {
            ControlledEntity.CurrentWeaponViewLeft.OnUpdate += SaveLeft;
            ControlledEntity.CurrentWeaponViewRight.OnUpdate += SaveRight;
            _weaponsCharacteristics.OnUpdate += SavePlayerData;
        }

        public void Dispose()
        {
            ControlledEntity.CurrentWeaponViewLeft.OnUpdate -= SaveLeft;
            ControlledEntity.CurrentWeaponViewRight.OnUpdate -= SaveRight;
            _weaponsCharacteristics.OnUpdate -= SavePlayerData;
        }

        private void SaveLeft(RxValue<WeaponBase> rxValue)
        {
            _applicationData.PlayerData.CurrentLeftWeaponGuid = rxValue.NewValue.Data._guid;

            SavePlayerData();
        }

        private void SaveRight(RxValue<WeaponBase> rxValue)
        {
            _applicationData.PlayerData.CurrentRightWeaponGuid =
                _applicationData.PlayerData.CurrentRightWeaponGuid = rxValue.NewValue.Data._guid;

            SavePlayerData();
        }

        private void SavePlayerData()
        {
            SaveLoadDataService.Save(_applicationData.PlayerData);
        }
    }
}