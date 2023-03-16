using Game.DataBase;
using Game.DataBase.Essence;
using TegridyCore.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.Essence 
{
    public class EssenceImageView : ViewBase
    {
        [SerializeField] private EssenceType _essenceType;
        [SerializeField] private TMP_Text _quantity;
        [SerializeField] private Image _essenceImage;

        public EssenceType EssenceType => _essenceType;
        public TMP_Text Quantity => _quantity;
        public Image EssenceImage => _essenceImage;

        public void SetType(EssenceType essenceType) 
        {
            _essenceType = essenceType;
        }
    }
}