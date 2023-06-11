using TegridyCore.Base;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Views.UI
{
    public class CoinsView : ViewBase
    {
        [SerializeField] private TMP_Text _coinsCountText;

        public void SetValue(string value)
        {
            _coinsCountText.text = value;
        }

    }
}