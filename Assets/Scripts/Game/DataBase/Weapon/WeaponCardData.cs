using System;
using Game.Gameplay.Views.UI;
using TegridyUtils.Attributes;

namespace Game.DataBase.Weapon
{
    [Serializable]
    public class WeaponCardData
    {
        [ArrayKey]
        public RarityType _rarityType;
        public WeaponCardView _weaponCardView;
        public WeaponCardView _weaponCardViewMini;
    }
}