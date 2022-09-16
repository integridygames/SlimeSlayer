using TegridyCore.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.Essence 
{
    public class EssenceImageView : ViewBase
    {
        [SerializeField] private TMP_Text _quantityTMPText;
        [SerializeField] private Image _essenceImage;

        public TMP_Text QuantityTMPText => _quantityTMPText;
        public Image EssenceImage => _essenceImage;
    }
}