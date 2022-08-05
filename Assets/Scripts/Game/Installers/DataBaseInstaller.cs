using Game.ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Game.Installers 
{
    public class DataBaseInstaller : MonoInstaller
    {
        [SerializeField] private LevelsDataBase _levelsDataBase;
        [SerializeField] private WeaponsDataBase _weaponsDataBase;
        [SerializeField] private AmmoDataBase _ammoDataBase;

        public override void InstallBindings()
        {
            Container.BindInstance(_levelsDataBase).AsSingle();
            Container.BindInstance(_weaponsDataBase).AsSingle();
            Container.BindInstance(_ammoDataBase).AsSingle();
        }
    }
}