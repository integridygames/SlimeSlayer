using UnityEngine;

namespace Game.Gameplay.Views.Character.Targets 
{
    public class HandTargetView : MonoBehaviour
    {
        [SerializeField] private bool _isLeft;

        public bool IsLeft => _isLeft;

        public Vector3 StartLocalPosition { get; private set; }

        private void Awake()
        {
            StartLocalPosition = transform.localPosition;
        }
    }
}