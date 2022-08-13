using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Gameplay.Views.Input.RaycasterUI 
{
    public class RaycasterUI : ViewBase
    {
        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private EventSystem _eventSystem;

        private PointerEventData _pointerEventData;
        private List<RaycastResult> _raycastResults;

        private void Start()
        {
            _pointerEventData = new PointerEventData(_eventSystem);
        }

        public bool DetectIfMouseOverUI()
        {
            _pointerEventData.position = UnityEngine.Input.mousePosition;

            _raycastResults = new List<RaycastResult>();
            _raycaster.Raycast(_pointerEventData, _raycastResults);

            if (_raycastResults.Count > 0)
                return true;
            else
                return false;
        }
    }   
}