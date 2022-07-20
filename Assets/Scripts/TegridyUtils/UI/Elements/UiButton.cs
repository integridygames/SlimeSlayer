using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TegridyUtils.UI.Elements
{
    [DisallowMultipleComponent]
    [SelectionBase]
    [AddComponentMenu("Tegridy/UI/UiButton", 30)]
    public class UiButton : Selectable
    {
        public event Action OnReleased;

        public event Action OnPressedDown;

        public override void OnPointerDown(PointerEventData eventData)
        {
            if (!IsInteractable())
                return;

            base.OnPointerDown(eventData);
            OnPressedDown?.Invoke();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            if (!IsInteractable())
                return;

            base.OnPointerUp(eventData);
            OnReleased?.Invoke();
        }
    }
}