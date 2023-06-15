using Game.Gameplay.Services;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Weapon
{
    public class WeaponInitializeSystem : IInitializeSystem
    {
        private readonly WeaponsService _weaponsService;


        public WeaponInitializeSystem(WeaponsService weaponsService)
        {
            _weaponsService = weaponsService;
        }

        public void Initialize()
        {
            _weaponsService.RefreshWeaponsInHands();
        }
    }
}