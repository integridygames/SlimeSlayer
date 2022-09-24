using Game.Gameplay.Utils.Essences;
using TegridyCore.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.Essence 
{
    public class EssenceImageView : ViewBase
    {
        [SerializeField] private EssenceType _essenceType;
        [SerializeField] private TMP_Text _quantityTMPText;
        [SerializeField] private Image _essenceImage;

        public EssenceType EssenceType => _essenceType;
        public TMP_Text QuantityTMPText => _quantityTMPText;
        public Image EssenceImage => _essenceImage;

        public void SetType(EssenceType essenceType) 
        {
            _essenceType = essenceType;
        }
    }
}