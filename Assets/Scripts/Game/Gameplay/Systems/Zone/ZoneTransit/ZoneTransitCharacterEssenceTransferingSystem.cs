using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Zone;
using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Essence;
using TegridyCore.Base;
using UnityEngine;

public class ZoneTransitCharacterEssenceTransferingSystem : IFixedUpdateSystem
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

    public void FixedUpdate()
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
            if (TryToFindEssenceInCharacterEssenceData(essenceData.EssenceType, out var characterEssence))
            {
                TryToSetEssenceQuantity(characterEssence, essenceData);
                TryToSetEssenceTextUI(essenceData);
            }
        }
    }

    private bool TryToFindEssenceInCharacterEssenceData(EssenceType essenceType, out CharacterEssence returnedCharacterEssence) 
    {
        returnedCharacterEssence = null;
        return _characterEssencesData.FindEssence(essenceType, out returnedCharacterEssence);
    }

    private void TryToSetEssenceTextUI(ZoneTransitEssenceData essenceData) 
    {
        if (FindEssenceImageView(essenceData.EssenceType, out var essenceImageView))
        {
            essenceImageView.QuantityTMPText.text = essenceData.Quantity.ToString();
        }
    }

    private void TryToSetEssenceQuantity(CharacterEssence characterEssence, ZoneTransitEssenceData essenceData) 
    {
        if (characterEssence.Quantity > 0 && essenceData.Quantity > 0)
        {
            characterEssence.Quantity = Mathf.Clamp(characterEssence.Quantity, 0, --characterEssence.Quantity);
            characterEssence.EssenceImageView.QuantityTMPText.text = characterEssence.Quantity.ToString();
            essenceData.Quantity--;
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
        foreach (var essenceImageView in _zoneTransitInfo.ZoneTransitMenuView.EssenceImageViews)
        {
            if (essenceImageView.EssenceType == essenceType)
            {
                returnedEssenceImageView = essenceImageView;
                return true;
            }
        }
        returnedEssenceImageView = null;

        return false;
    }
}