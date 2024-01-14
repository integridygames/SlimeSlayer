using Game.Gameplay.Models.Character;
using TegridyCore;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class GameToDeathScreenTransition : TransitionBase
    {
        private readonly CharacterCharacteristicsRepository _characterCharacteristicsRepository;

        public GameToDeathScreenTransition(StateBase stateFrom, StateBase stateTo, CharacterCharacteristicsRepository characterCharacteristicsRepository) : base(stateFrom, stateTo)
        {
            _characterCharacteristicsRepository = characterCharacteristicsRepository;
        }

        public override void OnTransitionAdded()
        {
            _characterCharacteristicsRepository.CurrentHealth.OnUpdate += CurrentHealthOnUpdateHandler;
        }

        public override void OnTransitionRemoved()
        {
            _characterCharacteristicsRepository.CurrentHealth.OnUpdate -= CurrentHealthOnUpdateHandler;
        }

        private void CurrentHealthOnUpdateHandler(RxValue<float> value)
        {
            if (value.NewValue <= 0)
            {
                DoTransition();
            }
        }
    }
}