using Game.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Game.Installers 
{
    public class DataBaseInstaller : MonoInstaller
    {
        [SerializeField] private LevelsDataBase _levelsDataBase;
        [SerializeField] private WeaponsDataBase _weaponsDataBase;

        public override void InstallBindings()
        {
            Container.BindInstance(_levelsDataBase).AsSingle();
            Container.BindInstance(_weaponsDataBase).AsSingle();
        }
    }
}