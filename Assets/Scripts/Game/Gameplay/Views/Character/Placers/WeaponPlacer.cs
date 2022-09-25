using UnityEngine;

namespace Game.Gameplay.Views.Character.Placers
{
    public class WeaponPlacer : MonoBehaviour
    {
        [SerializeField] private bool _isLeft;

        public bool IsLeft => _isLeft;
    }
}