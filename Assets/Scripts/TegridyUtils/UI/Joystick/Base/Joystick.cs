using UnityEngine;

namespace TegridyUtils.UI.Joystick.Base
{
    public class Joystick : MonoBehaviour
    {
        [SerializeField] private UiRaycaster _uiRaycaster;

        public float Horizontal => snapX ? SnapFloat(input.x, AxisOptions.Horizontal) : input.x;

        public float Vertical => snapY ? SnapFloat(input.y, AxisOptions.Vertical) : input.y;

        public Vector2 Direction => new Vector2(Horizontal, Vertical);

        public float HandleRange
        {
            get => handleRange;
            set { handleRange = Mathf.Abs(value); }
        }

        public float DeadZone
        {
            get => deadZone;
            set { deadZone = Mathf.Abs(value); }
        }

        public AxisOptions AxisOptions
        {
            get => AxisOptions;
            set { axisOptions = value; }
        }

        public bool SnapX
        {
            get => snapX;
            set => snapX = value;
        }

        public bool SnapY
        {
            get => snapY;
            set => snapY = value;
        }

        [SerializeField] private float handleRange = 1;
        [SerializeField] private float deadZone = 0;
        [SerializeField] private AxisOptions axisOptions = AxisOptions.Both;
        [SerializeField] private bool snapX = false;
        [SerializeField] private bool snapY = false;
        [SerializeField] private Vector2 startAnchoredPosition;

        [SerializeField] protected RectTransform background = null;
        [SerializeField] private RectTransform handle = null;

        private RectTransform baseRect = null;

        private Canvas canvas;
        private Camera cam;

        private Vector2 input = Vector2.zero;

        private bool _needToCallPressedDown = true;
        private int _uiLayer;
        
        public virtual void ClearInput()
        {
            input = Vector2.zero;
            handle.anchoredPosition = startAnchoredPosition;
        }

        protected virtual void Start()
        {
            HandleRange = handleRange;
            DeadZone = deadZone;
            baseRect = GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            if (canvas == null)
                Debug.LogError("The Joystick is not placed inside a canvas");

            Vector2 center = new Vector2(0.5f, 0.5f);
            background.pivot = center;
            handle.anchorMin = center;
            handle.anchorMax = center;
            handle.pivot = center;
            handle.anchoredPosition = startAnchoredPosition;

            _uiLayer = LayerMask.NameToLayer("UI");
        }

        private void OnEnable()
        {
            _needToCallPressedDown = true;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _needToCallPressedDown = true;
            }

            if (Input.GetMouseButton(0))
            {
                if (_uiRaycaster.CheckIfOveredObjectInLayer(_uiLayer))
                {
                    _needToCallPressedDown = true;
                    return;
                }
                
                var eventPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                if (_needToCallPressedDown)
                {
                    _needToCallPressedDown = false;
                    OnPressedDown(eventPosition);
                }

                HandleDrag(eventPosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnReleased();
            }
        }

        public virtual void OnPressedDown(Vector2 eventPosition)
        {
        }

        public virtual void OnReleased()
        {
            input = Vector2.zero;
            handle.anchoredPosition = startAnchoredPosition;
        }

        private void HandleDrag(Vector2 eventPosition)
        {
            cam = null;
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                cam = canvas.worldCamera;

            Vector2 position = RectTransformUtility.WorldToScreenPoint(cam, background.position);
            Vector2 radius = background.sizeDelta / 2;
            input = (eventPosition - position) / (radius * canvas.scaleFactor);
            FormatInput();
            HandleInput(input.magnitude, input.normalized, radius);
            handle.anchoredPosition = input * radius * handleRange;
        }

        protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius)
        {
            if (magnitude > deadZone)
            {
                if (magnitude > 1)
                    input = normalised;
            }
            else
                input = Vector2.zero;
        }

        private void FormatInput()
        {
            if (axisOptions == AxisOptions.Horizontal)
                input = new Vector2(input.x, 0f);
            else if (axisOptions == AxisOptions.Vertical)
                input = new Vector2(0f, input.y);
        }

        private float SnapFloat(float value, AxisOptions snapAxis)
        {
            if (value == 0)
                return value;

            if (axisOptions == AxisOptions.Both)
            {
                float angle = Vector2.Angle(input, Vector2.up);
                if (snapAxis == AxisOptions.Horizontal)
                {
                    if (angle < 22.5f || angle > 157.5f)
                        return 0;
                    return (value > 0) ? 1 : -1;
                }

                if (snapAxis == AxisOptions.Vertical)
                {
                    if (angle > 67.5f && angle < 112.5f)
                        return 0;
                    return (value > 0) ? 1 : -1;
                }

                return value;
            }

            if (value > 0)
                return 1;
            if (value < 0)
                return -1;

            return 0;
        }

        protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            Vector2 localPoint = Vector2.zero;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, cam, out localPoint))
            {
                Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
                return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
            }

            return Vector2.zero;
        }
    }

    public enum AxisOptions
    {
        Both,
        Horizontal,
        Vertical
    }
}