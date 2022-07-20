using Game.Services;
using TegridyCore.RequiredUnityComponents;
using UnityEngine;
using Zenject;

namespace Game.Installers.SampleScene
{
    public class MonoBehaviorInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineStarter _coroutineStarter;
        [SerializeField] private SoundService _soundService;

        public override void InstallBindings()
        {
            Container.BindInstance(_coroutineStarter).AsSingle();
            Container.BindInstance(_soundService).AsSingle();
        }
    }
}