using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TegridyUtils.UI.Elements
{
    public class UiToggle : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private bool _isInteractable = true;
        
        [SerializeField] private bool _value;

        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
                DoTransition();
                OnChangeValue?.Invoke(_value);
            }
        }
        
        [SerializeField] private RectTransform _disabledObject;
        [SerializeField] private RectTransform _enabledObject;
        
        public event Action<bool> OnChangeValue;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isInteractable == false)
                return;

            _value = !_value;
            
            DoTransition();
            
            OnChangeValue?.Invoke(_value);
        }

        private void DoTransition()
        {
            _disabledObject.gameObject.SetActive(!_value);
            _enabledObject.gameObject.SetActive(_value);
        }
    }
}
