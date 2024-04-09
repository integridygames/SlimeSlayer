using Game.Gameplay.TrashArchitecture;
using TegridyCore.FiniteStateMachine;

namespace Game.Gameplay.Transitions
{
    public class GameToEndScreenTransition : TransitionBase
    {
        private readonly SpawnerRepository _spawnerRepository;

        public GameToEndScreenTransition(StateBase stateFrom, StateBase stateTo, SpawnerRepository spawnerRepository) : base(stateFrom, stateTo)
        {
            _spawnerRepository = spawnerRepository;
        }

        public override void OnTransitionAdded()
        {
            _spawnerRepository.OnAllWaveCompleted += AllWaveCompletedHandler;
        }
        
        public override void OnTransitionRemoved()
        {
            _spawnerRepository.OnAllWaveCompleted -= AllWaveCompletedHandler;
        }

        private void AllWaveCompletedHandler()
        {
            DoTransition();
        }
    }
}