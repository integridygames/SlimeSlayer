using System;
using UnityEngine;

namespace TegridyCore.Base
{
    public abstract class ViewBase : MonoBehaviour
    {
        [SerializeField] private bool _isCached;

        public bool IsCached => _isCached;

        public event Action OnShow;
        
        public event Action OnHide;
        
        protected virtual void OnEnable()
        {
            OnShow?.Invoke();
        }
        
        protected virtual void OnDisable()
        {
            OnHide?.Invoke();
        }
    }
}