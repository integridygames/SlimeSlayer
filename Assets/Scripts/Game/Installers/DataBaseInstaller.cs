using Game.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Game.Installers 
{
    public class DataBaseInstaller : MonoInstaller
    {
        [SerializeField] private LevelsDataBase _levelsDataBase;

        public override void InstallBindings()
        {
            Container.BindInstance(_levelsDataBase).AsSingle();
        }
    }
}