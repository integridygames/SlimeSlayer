using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.UnityComponents
{
    [RequireComponent(typeof(Toggle))]
    public class UiSwitcher : MonoBehaviour
    {
        public event Action<bool> OnValueChanged;

        private Toggle _toggle;

        [SerializeField] private Image _image;
        [SerializeField] private Sprite _onState;
        [SerializeField] private Sprite _offState;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnToggleValueChangedHandler);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleValueChangedHandler);
        }

        private void OnToggleValueChangedHandler(bool value)
        {
            SetState(value);

            OnValueChanged?.Invoke(value);
        }

        public void SetState(bool value)
        {
            _image.sprite = value ? _onState : _offState;
        }
    }
}