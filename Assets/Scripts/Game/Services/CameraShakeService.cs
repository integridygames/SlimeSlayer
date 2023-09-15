using System;
using System.Collections.Generic;
using CartoonFX;
using UnityEngine;
using Zenject;

namespace Game.Services
{
    public class CameraShakeService : ITickable, IInitializable, IDisposable
    {
        private readonly CameraShake _cameraShake;
        private readonly List<ShakeData> _shakeData = new();

        public CameraShakeService(CameraShake cameraShake)
        {
            _cameraShake = cameraShake;
        }

        public void Shake(float time, float shakeStrength)
        {
            _shakeData.Add(new ShakeData
            {
                Time = time,
                CurrentTime = 0,
                ShakeStrength = shakeStrength
            });

            if (!_cameraShake.IsShaking)
            {
                _cameraShake.FetchCameras();
            }

            _cameraShake.StartShake();
        }

        public void Tick()
        {
            ShakeData mostImportantShake = null;

            for (var i = _shakeData.Count - 1; i >= 0; i--)
            {
                var shakeData = _shakeData[i];
                shakeData.CurrentTime += Time.deltaTime;

                if (shakeData.CurrentTime >= shakeData.Time)
                {
                    _shakeData.RemoveAt(i);
                    continue;
                }

                if (mostImportantShake == null || mostImportantShake.ShakeStrength < shakeData.ShakeStrength)
                {
                    mostImportantShake = shakeData;
                }
            }

            if (mostImportantShake != null)
            {
                _cameraShake.Animate(mostImportantShake.Time,
                    new Vector3(mostImportantShake.ShakeStrength, mostImportantShake.ShakeStrength,
                        mostImportantShake.ShakeStrength));
            }
        }

        public void Initialize()
        {
            _cameraShake.OnShakeStopped += OnShakeStoppedHandler;
        }

        public void Dispose()
        {
            _cameraShake.OnShakeStopped -= OnShakeStoppedHandler;
        }

        private void OnShakeStoppedHandler()
        {
        }

        private class ShakeData
        {
            public float Time { get; set; }
            public float CurrentTime { get; set; }
            public float ShakeStrength { get; set; }
        }
    }
}