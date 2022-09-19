using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Zone;
using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Essence;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Zone.ZoneTransit 
{
    public class ZoneTransitOpeningSystem : IUpdateSystem, IFixedUpdateSystem
    {
        private readonly ZoneTransitInfo _zoneTransitInfo;
        private readonly CharacterEssencesData _characterEssencesData;

        private bool IsAllowedToProcessOpening;
        private ZoneTransitEssenceData[] _currentEssenceDatas;
        private float _elapsedTime;

        private const float MaxElapsedTime = 1f;
        private const float OpeningSpeed = 1f;

        public ZoneTransitOpeningSystem(ZoneTransitInfo zoneTransitInfo, CharacterEssencesData characterEssencesData) 
        {
            _zoneTransitInfo = zoneTransitInfo;
            _characterEssencesData = characterEssencesData;
            IsAllowedToProcessOpening = false;
            _elapsedTime = 0;
        }

        public void FixedUpdate()
        {
            if (IsAllowedToProcessOpening) 
            {
                _elapsedTime += Time.fixedDeltaTime * OpeningSpeed;

                if(_elapsedTime >= MaxElapsedTime) 
                {
                    foreach (var essenceData in _currentEssenceDatas) 
                    {
                        if (_characterEssencesData.FindEssence(essenceData.EssenceType, out var characterEssence)) 
                        {
                            if(characterEssence.Quantity > 0 && essenceData.Quantity > 0) 
                            {                               
                                characterEssence.Quantity = Mathf.Clamp(characterEssence.Quantity, 0, --characterEssence.Quantity);
                                characterEssence.EssenceImageView.QuantityTMPText.text = characterEssence.Quantity.ToString();                              
                                essenceData.Quantity--;                              
                            }

                            if (FindEssenceImageView(essenceData.EssenceType, out var essenceImageView))
                            {
                                essenceImageView.QuantityTMPText.text = essenceData.Quantity.ToString();
                            }
                        }                 
                    }

                    _elapsedTime = 0;
                }
            }
        }

        public void Update()
        {
            if (_zoneTransitInfo.IsCharacterInNearestZoneTrigger && _zoneTransitInfo.WasButtonClicked) 
            {
                IsAllowedToProcessOpening = true;
                _currentEssenceDatas = _zoneTransitInfo.NearestZoneTransitView.EssenceData;
            }
            else 
            {
                IsAllowedToProcessOpening = false;
                _currentEssenceDatas = new ZoneTransitEssenceData[0];
                _elapsedTime = 0;
            }
          
            if(_zoneTransitInfo.NearestZoneTransitView != null) 
            {
                bool isEveryParamNull = true;
                foreach (var essenceData in _zoneTransitInfo.NearestZoneTransitView.EssenceData) 
                {
                    if (essenceData.Quantity > 0)
                        isEveryParamNull = false;
                }

                if (isEveryParamNull) 
                {
                    _zoneTransitInfo.NearestZoneTransitView.Open();
                    _zoneTransitInfo.NearestZoneTransitView.gameObject.SetActive(false);
                }                
            }
        }

        private ZoneTransitEssenceData FindEssenceData(EssenceType essenceType)
        {
            foreach (var esssenceData in _zoneTransitInfo.NearestZoneTransitView.EssenceData)
            {
                if (esssenceData.EssenceType == essenceType)
                    return esssenceData;
            }

            return null;
        }

        private bool FindEssenceImageView(EssenceType essenceType, out EssenceImageView returnedEssenceImageView) 
        {
            foreach(var essenceImageView in _zoneTransitInfo.ZoneTransitMenuView.EssenceImageViews) 
            {
                if(essenceImageView.EssenceType == essenceType) 
                {
                    returnedEssenceImageView = essenceImageView;
                    return true;
                }
            }
            returnedEssenceImageView = null;

            return false;
        }
    }
}