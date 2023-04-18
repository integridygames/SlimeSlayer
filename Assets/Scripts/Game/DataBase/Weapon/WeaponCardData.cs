using System;
using Game.Gameplay.Views.UI;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class WeaponCardData
    {
        public RarityType _rarityType;
        public WeaponCardView _weaponCardView;
    }
}