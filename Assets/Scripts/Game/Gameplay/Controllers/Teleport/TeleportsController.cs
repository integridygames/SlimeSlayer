using Game.Gameplay.Factories;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Enemy;
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
        public event Action EndScreenShowingEvent;

        private readonly TeleportInfo _teleportInfo;
        private readonly ApplicationData _applicationData;
        private readonly LevelFactory _levelFactory;
        private readonly CameraContainerView _cameraContainerView;
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;

        public TeleportsController(LevelInfo levelInfo, TeleportInfo teleportInfo, ApplicationData applicationData,
            LevelFactory levelFactory, CameraContainerView cameraContainerView, ActiveEnemiesContainer activeEnemiesContainer) : base(levelInfo) 
        {
            _teleportInfo = teleportInfo;
            _applicationData = applicationData;
            _levelFactory = levelFactory;
            _cameraContainerView = cameraContainerView;
            _activeEnemiesContainer = activeEnemiesContainer;
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

            _teleportInfo.CurrentTeleportView.Value = rxValue.NewValue.TeleportView;
            _teleportInfo.CurrentTeleportView.Value.PlayerEnteredTeleport += TryToMakeTeleportation;
        }

        private void TryToMakeTeleportation(CharacterView characterView) 
        {
            if (!CheckEnemiesExistance() && ControlledEntity.DoesNextLevelExist) 
            {             
                Teleport(characterView);
            }          
        }

        private void Teleport(CharacterView characterView) 
        {
            _levelFactory.NextLevel(true);
            var positionForTP = ControlledEntity.CurrentLevelView.Value.SpawnPointView.transform.position;
            _cameraContainerView.transform.position = new Vector3(positionForTP.x, _cameraContainerView.transform.position.y, positionForTP.z);
            characterView.transform.position = positionForTP;
        }

        private bool CheckEnemiesExistance() 
        {
            return _activeEnemiesContainer.ActiveEnemies.Count > 0;
        }      
    }
}