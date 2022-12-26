using Game.DataBase;
using Game.Gameplay.Models;
using Game.Services;
using JetBrains.Annotations;
using Zenject;

namespace Game.Gameplay.Factories
{
    [UsedImplicitly]
    public class ApplicationDataFactory : IFactory<ApplicationData>
    {
        public ApplicationData Create()
        {
            var applicationData = new ApplicationData
            {
                PlayerData = SaveLoadDataService.Load(new PlayerData(0, new())),
                PlayerSettings = SaveLoadDataService.Load(new PlayerSettings())
            };

            return applicationData;
        }     
    }
}