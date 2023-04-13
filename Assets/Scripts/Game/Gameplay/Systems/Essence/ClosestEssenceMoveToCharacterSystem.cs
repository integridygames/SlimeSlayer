using Game.Gameplay.Models.Essence;
using Game.Gameplay.Views.Character;
using Game.Gameplay.Views.Essence;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Systems.Essence
{
    public class ClosestEssenceMoveToCharacterSystem : IUpdateSystem
    {
        private readonly ActiveEssencesContainer _activeEssencesContainer;
        private readonly CharacterView _characterView;

        public ClosestEssenceMoveToCharacterSystem(ActiveEssencesContainer activeEssencesContainer, CharacterView characterView)
        {
            _activeEssencesContainer = activeEssencesContainer;
            _characterView = characterView;
        }

        public void Update()
        {
            foreach (var activeEssence in _activeEssencesContainer.ActiveEssences)
            {
                if (activeEssence.IsMovingToCharacter == false)
                {
                    continue;
                }

                SmoothMoveToCharacter(activeEssence);
            }
        }

        private void SmoothMoveToCharacter(EssenceView essenceView)
        {
            essenceView.MovingProgress += Time.deltaTime;

            essenceView.transform.position = Vector3.Lerp(essenceView.transform.position, _characterView.transform.position, essenceView.MovingProgress);
        }
    }
}