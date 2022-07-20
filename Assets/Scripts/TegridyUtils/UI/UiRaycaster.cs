using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TegridyUtils.UI
{
    public class UiRaycaster : MonoBehaviour
    {
        [SerializeField] private GraphicRaycaster _raycaster;
        [SerializeField] private EventSystem _eventSystem;
        
        private PointerEventData _pointerEventData;
        private List<RaycastResult> _raycastResults;

        private void Awake()
        {
            _pointerEventData = new PointerEventData(_eventSystem);
        }

        public bool CheckIfOveredObjectInLayer(int layer)
        {
            _pointerEventData.position = Input.mousePosition;

            _raycastResults = new List<RaycastResult>();
            _raycaster.Raycast(_pointerEventData, _raycastResults);

            for (var i = 0; i < _raycastResults.Count; i++)
            {
                var result = _raycastResults[i];
                if (result.gameObject.layer == layer)
                {
                    return true;
                }
            }

            return false;
        }
    }
}