using Game.Gameplay.Factories;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Models.Teleport;
using Game.Gameplay.Views.CameraContainer;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Level;
using System;
using TegridyCore;
using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Controllers.Teleport 
{
    public class TeleportsController : ControllerBase<LevelInfo>, IInitializable, IDisposable
    {
        private readonly TeleportInfo _teleportInfo;
        private readonly ApplicationData _applicationData;
        private readonly LevelFactory _levelFactory;
        private readonly CameraContainerView _cameraContainerView;

        public TeleportsController(LevelInfo levelInfo, TeleportInfo teleportInfo, ApplicationData applicationData,
            LevelFactory levelFactory, CameraContainerView cameraContainerView) : base(levelInfo) 
        {
            _teleportInfo = teleportInfo;
            _applicationData = applicationData;
            _levelFactory = levelFactory;
            _cameraContainerView = cameraContainerView;
        }

        public void Initialize()
        {
            ControlledEntity.CurrentLevelView.OnUpdate += GetTeleport;
        }

        public void Dispose()
        {
            ControlledEntity.CurrentLevelView.OnUpdate -= GetTeleport;
        }

        private void GetTeleport(RxValue<LevelView> rxValue) 
        {
            if(_teleportInfo.CurrentTeleportView.Value != null) 
                _teleportInfo.CurrentTeleportView.Value.PlayerEnteredTeleport -= TryToMakeTeleportation;

            _teleportInfo.CurrentTeleportView = rxValue.NewValue.TeleportView;
            _teleportInfo.CurrentTeleportView.Value.PlayerEnteredTeleport += TryToMakeTeleportation;
        }

        private void TryToMakeTeleportation(CharacterView characterView) 
        {
            switch (ControlledEntity.DoesNextLevelExist)
            {
                case false:

                    break;
                case true:
                    Teleport(characterView);
                    break;
            }
           
        }

        private void Teleport(CharacterView characterView) 
        {
            ControlledEntity.CurrentLevelView = ControlledEntity.NextLevelView;
            ControlledEntity.NextLevelView = _levelFactory.NextLevel(out bool doesNextLevelExist);
            ControlledEntity.DoesNextLevelExist = doesNextLevelExist;
            _applicationData.PlayerData.CurrentLevel++;

            var positionForTP = ControlledEntity.NextLevelView.Value.SpawnPointView.transform.position;
            _cameraContainerView.transform.position = new Vector3(positionForTP.x, _cameraContainerView.transform.position.y, positionForTP.z);
            characterView.transform.position = positionForTP;
        }
    }
}