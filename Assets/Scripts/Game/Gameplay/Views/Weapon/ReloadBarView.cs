using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.Weapon
{
    public class ReloadBarView : MonoBehaviour
    {
        [SerializeField] private Image _reloadImage;

        public void SetReloadProgress(float value)
        {
            _reloadImage.fillAmount = value;
        }
    }
}