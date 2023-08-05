using Game.DataBase.FX;
using Game.Gameplay.Factories;
using Game.Gameplay.Models.Abilities;
using Game.Gameplay.Models.Camera;
using Game.Gameplay.Models.Character;
using Game.Gameplay.Services;
using Game.Gameplay.Views.CameraContainer;
using Game.Gameplay.Views.FX;
using UnityEngine;

namespace Game.Gameplay.AbilitiesMechanics
{
    public class ManyRandomMissilesAbility : AbilityBase
    {
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;
        private readonly CameraRepository _cameraRepository;
        private readonly RecyclableParticlesPoolFactory _particlesPoolFactory;
        private readonly CameraContainerView _cameraContainerView;
        private readonly WeaponMechanicsService _weaponMechanicsService;
        private float _timeToDrop;
        private float _delay;

        public ManyRandomMissilesAbility(CharacterCharacteristicsRepository characterCharacteristicsRepository,
            CameraRepository cameraRepository,
            RecyclableParticlesPoolFactory particlesPoolFactory, CameraContainerView cameraContainerView,
            WeaponMechanicsService weaponMechanicsService)
        {
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
            _cameraRepository = cameraRepository;
            _particlesPoolFactory = particlesPoolFactory;
            _cameraContainerView = cameraContainerView;
            _weaponMechanicsService = weaponMechanicsService;
        }

        public override void OnStart()
        {
            _characterCharacteristicsRepository.TryGetAbilityCharacteristic(
                AbilityCharacteristicType.RandomMissilesDelay, out _delay);

            _timeToDrop = _delay;
        }

        public override void Execute()
        {
            _timeToDrop -= Time.deltaTime;
            if (_timeToDrop <= 0)
            {
                DropMissiles();
                _timeToDrop = _delay;
            }
        }

        private void DropMissiles()
        {
            _characterCharacteristicsRepository.TryGetAbilityCharacteristic(
                AbilityCharacteristicType.RandomMissilesCount, out int missilesCount);

            var distanceToCamera = Vector3.Distance(_cameraRepository.CameraTargetPosition,
                _cameraContainerView.Camera.transform.position);

            for (var i = 0; i < missilesCount; i++)
            {
                var xPositionOnScreen = Random.Range(0.1f, 0.9f);
                var yPositionOnScreen = Random.Range(0.1f, 0.9f);

                var screenToWorldPoint = _cameraContainerView.Camera.ViewportToWorldPoint(new Vector3(xPositionOnScreen,
                    yPositionOnScreen, distanceToCamera));

                var droppedMissileFx = _particlesPoolFactory.GetElement(RecyclableParticleType.DroppedMissile);
                var droppedMissileTransform = droppedMissileFx.transform;

                screenToWorldPoint.y = droppedMissileTransform.position.y;
                droppedMissileTransform.position = screenToWorldPoint;

                droppedMissileFx.OnMainParticleStopped += OnMainParticleStoppedHandler;
                droppedMissileFx.OnParticleCompletelyStopped += OnParticleCompletelyStoppedHandler;

                droppedMissileFx.Play();
            }
        }

        private void OnMainParticleStoppedHandler(RecyclableParticleView recyclableParticleView)
        {
            recyclableParticleView.OnMainParticleStopped -= OnMainParticleStoppedHandler;

            var particlePosition = recyclableParticleView.transform.position;
            particlePosition.y = 0.5f;

            _characterCharacteristicsRepository.TryGetAbilityCharacteristic(
                AbilityCharacteristicType.RandomMissilesDamage, out float damage);

            _weaponMechanicsService.DoExplosion(RecyclableParticleType.GrenadeExplosion, particlePosition, damage);
        }

        private void OnParticleCompletelyStoppedHandler(RecyclableParticleView recyclableParticleView)
        {
            recyclableParticleView.OnParticleCompletelyStopped -= OnParticleCompletelyStoppedHandler;
            _particlesPoolFactory.RecycleElement(RecyclableParticleType.DroppedMissile, recyclableParticleView);
        }
    }
}