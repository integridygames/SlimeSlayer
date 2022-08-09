using UnityEngine;

namespace Game.Gameplay.Views.Character.Targets 
{
    public class HandTargetView : MonoBehaviour
    {
        [SerializeField] private bool _isLeft;

        public bool IsLeft => _isLeft;
    }
}