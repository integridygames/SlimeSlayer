using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.UI
{
    public class WeaponCardView : MonoBehaviour
    {
        [SerializeField] private Image _weaponImage;

        public void SetWeaponSprite(Sprite sprite)
        {
            _weaponImage.sprite = sprite;
        }
    }
}