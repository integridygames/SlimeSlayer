using UnityEngine;

namespace TegridyUtils.UI.Joystick.Joysticks
{
    public class DynamicJoystick : Base.Joystick
    {
        public float MoveThreshold { get { return moveThreshold; } set { moveThreshold = Mathf.Abs(value); } }

        [SerializeField] private float moveThreshold = 1;

        protected override void Start()
        {
            MoveThreshold = moveThreshold;
            base.Start();
            background.gameObject.SetActive(false);
        }

        public override void OnPressedDown(Vector2 eventPosition)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventPosition);
            background.gameObject.SetActive(true);
        }

        public override void ClearInput()
        {
            background.gameObject.SetActive(false);
            base.ClearInput();
        }

        public override void OnReleased()
        {
            background.gameObject.SetActive(false);
            base.OnReleased();
        }

        protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius)
        {
            if (magnitude > moveThreshold)
            {
                Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
                background.anchoredPosition += difference;
            }
            base.HandleInput(magnitude, normalised, radius);
        }
    }
}