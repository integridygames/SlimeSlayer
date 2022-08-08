using UnityEngine;

namespace Game.Gameplay.Views.Character.Targets 
{
    public class HandRotationCenterView : MonoBehaviour
    {
        [SerializeField] private bool _isLeft;

        public bool IsLeft => _isLeft;
    }
}