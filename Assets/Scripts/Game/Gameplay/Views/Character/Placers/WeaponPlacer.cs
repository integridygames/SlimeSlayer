using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Views.Character.Placers
{
    public class WeaponPlacer : ViewBase
    {
        [SerializeField] private bool _isLeft;

        public bool IsLeft => _isLeft;
    }
}