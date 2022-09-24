using TegridyCore.Base;
using Game.Gameplay.Models.Zone;
using Game.Gameplay.Views.Zone;
using Game.Gameplay.Views.SampleScene.Screens;
using Game.ScriptableObjects;
using Game.Gameplay.Utils.Essences;
using UnityEngine;

namespace Game.Gameplay.Systems.Zone.ZoneTransitMenu 
{
    public class ZoneTransitMenuChangingVisualSystem : IUpdateSystem
    {
        private readonly ZoneTransitInfo _zoneTransitInfo;
        private ZoneTransitView _previousZoneTransitView;
        private readonly GameScreenView _gameScrennView;
        private readonly EssenceDataBase _essenceDataBase;

        public ZoneTransitMenuChangingVisualSystem(ZoneTransitInfo zoneTransitInfo, GameScreenView gameScrennView, EssenceDataBase essenceDataBase) 
        {
            _zoneTransitInfo = zoneTransitInfo;
            _previousZoneTransitView = _zoneTransitInfo.NearestZoneTransitView;
            _gameScrennView = gameScrennView;
            _essenceDataBase = essenceDataBase;
        }

        public void Update()
        {
            if (Condition()) 
            {
                _previousZoneTransitView = _zoneTransitInfo.NearestZoneTransitView;
                SetVisual();
            }
        }

        private bool Condition() 
        {
            return _gameScrennView.gameObject.activeInHierarchy && 
                   _zoneTransitInfo.NearestZoneTransitView != _previousZoneTransitView &&
                   _zoneTransitInfo.NearestZoneTransitView != null;
        }

        private void SetVisual() 
        {
            SetUnactiveAllViews();

            for (int i = 0; i < _zoneTransitInfo.NearestZoneTransitView.EssenceData.Length; i++) 
            {
                if (ConditionForKeyExistence(_zoneTransitInfo.NearestZoneTransitView.EssenceData[i].EssenceType))
                {
                    SetQuantity(_zoneTransitInfo.NearestZoneTransitView.EssenceData[i]);
                    SetActiveView(_zoneTransitInfo.NearestZoneTransitView.EssenceData[i]);
                }
            }
        }

        private void SetUnactiveAllViews() 
        {
            foreach (var essenceImageView in _zoneTransitInfo.ZoneTransitMenuView.EssenceImageViewsList)
                essenceImageView.gameObject.SetActive(false);
        }

        private void SetQuantity(ZoneTransitEssenceData essenceData) 
        {
            _zoneTransitInfo.ZoneTransitMenuView.
                     EssenceImageViewsDictionary[essenceData.EssenceType].
                     Quantity.text = essenceData.Quantity.ToString();
        }

        private void SetActiveView(ZoneTransitEssenceData essenceData) 
        {
            _zoneTransitInfo.ZoneTransitMenuView.
                 EssenceImageViewsDictionary[essenceData.EssenceType].gameObject.SetActive(true);
        }

        private bool ConditionForKeyExistence(EssenceType type) 
        {
            return _zoneTransitInfo.ZoneTransitMenuView.
                   EssenceImageViewsDictionary.
                   ContainsKey(type);
        }
    }
}