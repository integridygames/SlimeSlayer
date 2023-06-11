using Game.Gameplay.Models.GameResources;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.GameResources;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Essence
{
    public class ClosestResourceMoveToCharacterSystem : IUpdateSystem
    {
        private readonly ActiveEssencesContainer _activeEssencesContainer;
        private readonly ActiveCoinsContainer _activeCoinsContainer;
        private readonly CharacterView _characterView;

        public ClosestResourceMoveToCharacterSystem(ActiveEssencesContainer activeEssencesContainer,
            ActiveCoinsContainer activeCoinsContainer, CharacterView characterView)
        {
            _activeEssencesContainer = activeEssencesContainer;
            _activeCoinsContainer = activeCoinsContainer;
            _characterView = characterView;
        }

        public void Update()
        {
            foreach (var essence in _activeEssencesContainer.Values)
            {
                HandleResourceMoving(essence.transform, essence);
            }

            foreach (var coin in _activeCoinsContainer.Values)
            {
                HandleResourceMoving(coin.transform, coin);
            }
        }

        private void HandleResourceMoving(Transform transform, GameResourceViewBase gameResourceViewBase)
        {
            if (gameResourceViewBase.IsMovingToCharacter == false)
            {
                return;
            }

            SmoothMoveToCharacter(transform, gameResourceViewBase);
        }

        private void SmoothMoveToCharacter(Transform transform, GameResourceViewBase gameResourceViewBase)
        {
            gameResourceViewBase.MovingProgress += Time.deltaTime;

            transform.position = Vector3.Lerp(transform.position, _characterView.transform.position,
                gameResourceViewBase.MovingProgress);
        }
    }
}