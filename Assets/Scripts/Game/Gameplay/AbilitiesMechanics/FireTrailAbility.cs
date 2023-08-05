using Game.DataBase.FX;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.FX;
using Game.Gameplay.WeaponMechanics;
using UnityEngine;

namespace Game.Gameplay.AbilitiesMechanics
{
    public class FireTrailAbility : AbilityBase
    {
        private readonly RecyclableParticlesPoolFactory _recyclableParticlesPoolFactory;
        private readonly CharacterView _characterView;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;
        private FireTrailView _fireTrail;

        public FireTrailAbility(RecyclableParticlesPoolFactory recyclableParticlesPoolFactory,
            CharacterView characterView, CharacterCharacteristicsRepository characterCharacteristicsRepository)
        {
            _recyclableParticlesPoolFactory = recyclableParticlesPoolFactory;
            _characterView = characterView;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public override void OnStart()
        {
            _fireTrail = (FireTrailView) _recyclableParticlesPoolFactory.GetElement(RecyclableParticleType.FireTrail);
            Transform transform;
            (transform = _fireTrail.transform).SetParent(_characterView.transform);
            transform.localPosition = Vector3.zero;

            _fireTrail.OnEnemyCollide += OnEnemyCollideHandler;
        }

        public override void OnEnd()
        {
            _fireTrail.OnEnemyCollide -= OnEnemyCollideHandler;

            _recyclableParticlesPoolFactory.RecycleElement(RecyclableParticleType.FireTrail, _fireTrail);
        }

        private void OnEnemyCollideHandler(CommonEnemyView commonEnemyView)
        {
            _characterCharacteristicsRepository.TryGetAbilityCharacteristic(AbilityCharacteristicType.FireTrailDamage,
                out float damage);

            commonEnemyView.InvokeHit(new HitInfo(damage, Vector3.zero, commonEnemyView.transform.position));
        }
    }
}