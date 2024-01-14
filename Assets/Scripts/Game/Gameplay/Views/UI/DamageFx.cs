using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TegridyCore.Base;
using TMPro;
using UnityEngine;

namespace Game.Gameplay.Views.UI
{
    public class DamageFx : ViewBase
    {
        [SerializeField] private TMP_Text _value;
        [SerializeField] private float _duration = 0.6f;

        private CancellationTokenSource _cancellationTokenSource;

        public void StartFx(string value, Action onComplete)
        {
            _value.text = $"-{value}";

            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            _cancellationTokenSource = new CancellationTokenSource();

            StartMovingAsync(onComplete, _cancellationTokenSource.Token).Forget();
        }

        private async UniTaskVoid StartMovingAsync(Action onComplete, CancellationToken cancellationToken)
        {
            try
            {
                float startTime = 0;
                var valueColor = _value.color;
                valueColor.a = 1;
                _value.color = valueColor;

                while (startTime <= _duration)
                {
                    transform.position += Vector3.up * Time.deltaTime * 800f;

                    startTime += Time.deltaTime;
                    valueColor.a = 1 - startTime / _duration;

                    _value.color = valueColor;

                    await UniTask.Yield(cancellationToken);
                }

                onComplete?.Invoke();
            }
            catch (OperationCanceledException _)
            {
            }
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}