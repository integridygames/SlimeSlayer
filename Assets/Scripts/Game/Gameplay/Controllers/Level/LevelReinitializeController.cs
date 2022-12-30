using Game.Gameplay.Factories;
using Game.Gameplay.Models;
using Game.Gameplay.Models.Level;
using Game.Gameplay.Views.CameraContainer;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.SampleScene.Screens;
using System;
using TegridyCore.Base;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Controllers.Level 
{
    public class LevelReinitializeController : ControllerBase<LevelInfo>, IInitializable, IDisposable
    {
        private readonly EndScreenView _endScreenView;
        private readonly ApplicationData _applicationData;
        private readonly LevelFactory _levelFactory;
        private readonly CharacterView _characterView;
        private readonly CameraContainerView _cameraContainerView;

        public LevelReinitializeController(LevelInfo levelInfo, EndScreenView endScreenView, 
            ApplicationData applicationData, LevelFactory levelFactory, CharacterView characterView,
            CameraContainerView cameraContainerView) : base(levelInfo) 
        {
            _endScreenView = endScreenView;
            _applicationData = applicationData;
            _levelFactory = levelFactory;
            _characterView = characterView;
            _cameraContainerView = cameraContainerView;
        }

        public void Initialize()
        {
            _endScreenView.OnShow += SubToCompleteButton;
        }

        public void Dispose()
        {
            _endScreenView.OnShow -= SubToCompleteButton;
        }

        private void SubToCompleteButton() 
        {
            _endScreenView.CompleteButton.OnReleased += StartFromFirstLevel;
        }

        private void StartFromFirstLevel() 
        {
            _endScreenView.CompleteButton.OnReleased -= StartFromFirstLevel;
            _applicationData.PlayerData.CurrentLevel = 0;
            ControlledEntity.CurrentLevelView.Value = _levelFactory.Create();
            _levelFactory.NextLevel(false);
            ResetPlayerPosition();

            if (ControlledEntity.CurrentLevelView.Value != null) 
            {
                /*Object.Destroy(ControlledEntity.CurrentLevelView.Value.gameObject);*/
            }
        }

        private void ResetPlayerPosition() 
        {
            var position = ControlledEntity.CurrentLevelView.Value.SpawnPointView.transform.position;
            _cameraContainerView.transform.position = new Vector3(position.x, _cameraContainerView.transform.position.y, position.z);
            _characterView.transform.position = position;
        }


    }
}