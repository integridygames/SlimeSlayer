using UnityEngine;

namespace Game.Gameplay.Views.Weapon
{
    public class ProgressBarView : MonoBehaviour
    {
        [SerializeField] private RectTransform _progressImage;
        [SerializeField] private RectTransform _parent;
        [SerializeField] private ScaleType _scaleType;

        public void SetProgress(float value)
        {
            var progressImageSizeDelta = _progressImage.sizeDelta;

            if (_scaleType == ScaleType.ByWidth)
            {
                progressImageSizeDelta.x = _parent.rect.width * value;
            }
            else
            {
                progressImageSizeDelta.y = _parent.rect.height * value;
            }

            _progressImage.sizeDelta = progressImageSizeDelta;
        }

        private enum ScaleType
        {
            ByWidth,
            ByHeight,
        }
    }
}