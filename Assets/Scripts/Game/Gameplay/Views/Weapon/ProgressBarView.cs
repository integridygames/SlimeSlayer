using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.Weapon
{
    public class ProgressBarView : MonoBehaviour
    {
        [SerializeField] private Image _progressImage;

        public void SetProgress(float value)
        {
            _progressImage.fillAmount = value;
        }
    }
}