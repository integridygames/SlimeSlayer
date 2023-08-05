using Game.DataBase.FX;
using Game.Gameplay.EnemiesMechanics;
using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Services;

namespace Game.Gameplay.AbilitiesMechanics
{
    public class EnemyDeathExplosionAbility : AbilityBase
    {
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly WeaponMechanicsService _weaponMechanicsService;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        public EnemyDeathExplosionAbility(ActiveEnemiesContainer activeEnemiesContainer,
            WeaponMechanicsService weaponMechanicsService,
            CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _activeEnemiesContainer = activeEnemiesContainer;
            _weaponMechanicsService = weaponMechanicsService;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public override void OnStart()
        {
            _activeEnemiesContainer.OnEnemyDied += OnEnemyDiedHandler;
        }

        public override void OnEnd()
        {
            _activeEnemiesContainer.OnEnemyDied -= OnEnemyDiedHandler;
        }

        private void OnEnemyDiedHandler(EnemyBase enemy)
        {
            _characterCharacteristicsRepository.TryGetAbilityCharacteristic(AbilityCharacteristicType.EnemyExplosionDamage, out float damage);
            _weaponMechanicsService.DoExplosion(RecyclableParticleType.GrenadeExplosion, enemy.Position, damage);
        }
    }
}