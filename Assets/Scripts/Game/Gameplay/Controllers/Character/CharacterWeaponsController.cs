using System;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Weapon;
using Game.Gameplay.WeaponMechanics;
using Game.Services;
using TegridyCore;
using TegridyCore.Base;
using Zenject;

namespace Game.Gameplay.Controllers.Character
{
    public class CharacterWeaponsController : ControllerBase<CharacterWeaponsRepository>, IInitializable, IDisposable
    {
        private readonly WeaponsCharacteristics _weaponsCharacteristics;
        private readonly CharacterCharacteristics _characterCharacteristics;
        private readonly ApplicationData _applicationData;

        public CharacterWeaponsController(CharacterWeaponsRepository controlledEntity, WeaponsCharacteristics weaponsCharacteristics, CharacterCharacteristics characterCharacteristics, ApplicationData applicationData) :
            base(controlledEntity)
        {
            _weaponsCharacteristics = weaponsCharacteristics;
            _characterCharacteristics = characterCharacteristics;
            _applicationData = applicationData;
        }

        public void Initialize()
        {
            ControlledEntity.CurrentWeaponViewLeft.OnUpdate += OnLeftWeaponUpdateHandler;
            ControlledEntity.CurrentWeaponViewRight.OnUpdate += OnRightWeaponUpdateHandler;
            _weaponsCharacteristics.OnUpdate += SavePlayerData;
            _characterCharacteristics.OnUpdate += SavePlayerData;
        }

        public void Dispose()
        {
            ControlledEntity.CurrentWeaponViewLeft.OnUpdate -= OnLeftWeaponUpdateHandler;
            ControlledEntity.CurrentWeaponViewRight.OnUpdate -= OnRightWeaponUpdateHandler;
            _weaponsCharacteristics.OnUpdate -= SavePlayerData;
            _characterCharacteristics.OnUpdate -= SavePlayerData;
        }

        private void OnLeftWeaponUpdateHandler(RxValue<WeaponBase> rxValue)
        {
            _applicationData.PlayerData.CurrentLeftWeaponGuid = rxValue.NewValue.Data._guid;

            if (rxValue.OldValue != null)
            {
                rxValue.OldValue.Data._equipped = false;
            }
            rxValue.NewValue.Data._equipped = true;

            SavePlayerData();
        }

        private void OnRightWeaponUpdateHandler(RxValue<WeaponBase> rxValue)
        {
            _applicationData.PlayerData.CurrentRightWeaponGuid = rxValue.NewValue.Data._guid;

            if (rxValue.OldValue != null)
            {
                rxValue.OldValue.Data._equipped = false;
            }
            rxValue.NewValue.Data._equipped = true;

            SavePlayerData();
        }

        private void SavePlayerData()
        {
            SaveLoadDataService.Save(_applicationData.PlayerData);
        }
    }
}