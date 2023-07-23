using System;
using Game.Gameplay.AbilitiesMechanics;
using Game.Gameplay.Models.Abilities;
using Zenject;

namespace Game.Gameplay.Factories
{
    public class AbilityFactory
    {
        private readonly DiContainer _container;

        public AbilityFactory(DiContainer container)
        {
            _container = container;
        }

        public AbilityBase CreateAbility(AbilityType abilityType)
        {
            AbilityBase result = abilityType switch
            {
                AbilityType.MultipleGrenade => _container.Instantiate<MultipleGrenadeAbility>(),
                AbilityType.EnemyDeathExplosion => _container.Instantiate<EnemyDeathExplosionAbility>(),
                AbilityType.PeriodicGrenadeToRandomEnemy =>
                    _container.Instantiate<PeriodicGrenadeToRandomEnemyAbility>(),
                AbilityType.FireTrail => _container.Instantiate<FireTrailAbility>(),
                AbilityType.PushingImpulse => _container.Instantiate<PushingImpulseAbility>(),
                AbilityType.SpeedFire => _container.Instantiate<SpeedFireAbility>(),
                AbilityType.SpeedReload => _container.Instantiate<SpeedReloadAbility>(),
                AbilityType.ManyRandomMissiles => _container.Instantiate<ManyRandomMissilesAbility>(),
                AbilityType.HealthRegeneration => _container.Instantiate<HealthRegenerationAbility>(),
                AbilityType.MoreHealth => _container.Instantiate<MoreHealthAbility>(),
                AbilityType.MovementSpeed => _container.Instantiate<MovementSpeedAbility>(),
                AbilityType.HardCurrencyChanceImprovement => _container
                    .Instantiate<HardCurrencyChanceImprovementAbility>(),
                AbilityType.ChargeImprovement => _container.Instantiate<ChargeImprovementAbility>(),
                AbilityType.HealthOneTimeRecovery => _container.Instantiate<HealthOneTimeRecoveryAbility>(),
                AbilityType.SoftCurrency => _container.Instantiate<SoftCurrencyAbility>(),
                AbilityType.MissileToRandomEnemy => _container.Instantiate<MissileToRandomEnemyAbility>(),
                _ => throw new ArgumentOutOfRangeException(nameof(abilityType), abilityType, null)
            };

            return result;
        }
    }
}