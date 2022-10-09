using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Zone;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Zone.ZoneTransit
{
    public class ZoneTransitCharacterEssenceTransferingSystem : IUpdateSystem
    {
        private readonly ZoneTransitInteractionInfo _zoneTransitInteractionInfo;
        private readonly ZoneTransitInfo _zoneTransitInfo;
        private readonly CharacterEssencesData _characterEssencesData;

        public ZoneTransitCharacterEssenceTransferingSystem(ZoneTransitInteractionInfo zoneTransitInteractionInfo, ZoneTransitInfo zoneTransitInfo,
            CharacterEssencesData characterEssencesData)
        {
            _zoneTransitInteractionInfo = zoneTransitInteractionInfo;
            _zoneTransitInfo = zoneTransitInfo;
            _characterEssencesData = characterEssencesData;
        }

        public void Update()
        {
            if (ConditionForTransfer())
            {
                CalculateElapsedTime();

                if (ConditionForElapsedTime())
                {
                    TryToTranfer();
                    _zoneTransitInteractionInfo.ChangeElapsedTime(0);
                }
            }
        }

        private void TryToTranfer()
        {
            foreach (var essenceData in _zoneTransitInteractionInfo.CurrentEssenceDataset)
            {
                if (ConditionForCharacterEssences(essenceData))
                {
                    TransferEssence(essenceData);

                    TryToSetEssenceTextUI(essenceData);
                }
            }
        }

        private bool ConditionForCharacterEssences(ZoneTransitEssenceData zoneTransitEssenceData)
        {
            return _characterEssencesData.GetEssenceQuantity(zoneTransitEssenceData.EssenceType) > 0 && zoneTransitEssenceData.Quantity > 0;
        }

        private void TransferEssence(ZoneTransitEssenceData essenceData)
        {
            _characterEssencesData.RemoveEssence(essenceData.EssenceType, 1);
            essenceData.Quantity--;
        }

        private void TryToSetEssenceTextUI(ZoneTransitEssenceData essenceData)
        {
            if (_zoneTransitInfo.ZoneTransitMenuView.EssenceImageViewsDictionary.ContainsKey(essenceData.EssenceType))
            {
                _zoneTransitInfo.ZoneTransitMenuView.
                    EssenceImageViewsDictionary[essenceData.EssenceType].
                    Quantity.text = essenceData.Quantity.ToString();
            }
        }

        private bool ConditionForTransfer()
        {
            return _zoneTransitInteractionInfo.IsAllowedToProcessOpening;
        }
        private bool ConditionForElapsedTime()
        {
            return _zoneTransitInteractionInfo.ElapsedTime >= _zoneTransitInteractionInfo.MaxElapsedTime;
        }

        private void CalculateElapsedTime()
        {
            var elapsedTime = _zoneTransitInteractionInfo.ElapsedTime + Time.fixedDeltaTime * _zoneTransitInteractionInfo.OpeningSpeed;
            _zoneTransitInteractionInfo.ChangeElapsedTime(elapsedTime);
        }
    }
}