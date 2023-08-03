using Game.DataBase.FX;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Enemy;
using Game.Gameplay.Views.FX;
using UnityEngine;

namespace Game.Gameplay.AbilitiesMechanics
{
    public class PushingImpulseAbility : AbilityBase
    {
        private readonly RecyclableParticlesPoolFactory _recyclableParticlesPoolFactory;
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;
        private readonly CharacterView _characterView;
        private PushingImpulseView _pushingImpulseView;

        private float _timeToPush;
        private float _delay;

        public PushingImpulseAbility(RecyclableParticlesPoolFactory recyclableParticlesPoolFactory,
            CharacterCharacteristicsRepository characterCharacteristicsRepository, CharacterView characterView)
        {
            _recyclableParticlesPoolFactory = recyclableParticlesPoolFactory;
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
            _characterView = characterView;
        }

        public override void OnStart()
        {
            _pushingImpulseView =
                (PushingImpulseView) _recyclableParticlesPoolFactory.GetElement(RecyclableParticleType.PushingImpulse);

            Transform transform;
            (transform = _pushingImpulseView.transform).SetParent(_characterView.transform);
            transform.localPosition = Vector3.up * 0.3f;

            _pushingImpulseView.OnEnemyCollide += OnEnemyCollideHandler;

            _characterCharacteristicsRepository.TryGetAbilityCharacteristic(
                AbilityCharacteristicType.PushingImpulseDelay, out _delay);

            _timeToPush = _delay;
        }

        public override void Execute()
        {
            _timeToPush -= Time.deltaTime;
            if (_timeToPush <= 0)
            {
                _pushingImpulseView.Play();
                _timeToPush = _delay;
            }
        }

        public override void OnEnd()
        {
            _pushingImpulseView.OnEnemyCollide -= OnEnemyCollideHandler;

            _recyclableParticlesPoolFactory.RecycleElement(RecyclableParticleType.PushingImpulse, _pushingImpulseView);
        }

        private void OnEnemyCollideHandler(CommonEnemyView commonEnemyView)
        {
            Debug.Log("Push");

            _characterCharacteristicsRepository.TryGetAbilityCharacteristic(
                AbilityCharacteristicType.PushingImpulseForce, out var force);

            var direction = commonEnemyView.transform.position - _characterView.transform.position;
            direction.y = 0;
            commonEnemyView.Rigidbody.AddForce(direction.normalized * force, ForceMode.Impulse);
        }
    }
}