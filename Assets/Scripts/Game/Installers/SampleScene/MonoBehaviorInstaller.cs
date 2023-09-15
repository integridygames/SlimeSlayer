using CartoonFX;
using Game.Services;
using TegridyCore.RequiredUnityComponents;
using TegridyUtils.UI.Joystick.Base;
using UnityEngine;
using Zenject;

namespace Game.Installers.SampleScene
{
    public class MonoBehaviorInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineStarter _coroutineStarter;
        [SerializeField] private SoundService _soundService;
        [SerializeField] private Joystick _joystick;
        [SerializeField] private CameraShake _cameraShake;

        public override void InstallBindings()
        {
            Container.BindInstance(_coroutineStarter).AsSingle();
            Container.BindInstance(_soundService).AsSingle();
            Container.BindInstance(_joystick).AsSingle();
            Container.BindInstance(_cameraShake).AsSingle();
        }
    }
}