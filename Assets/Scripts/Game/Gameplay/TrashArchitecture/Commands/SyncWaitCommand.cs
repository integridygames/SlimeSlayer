using System.Linq;

namespace Game.Gameplay.TrashArchitecture.Commands
{
    public class SyncWaitCommand : ISpawnerCommand
    {
        private readonly SpawnerRepository _spawnerRepository;
        public int QueueIndex { get; }

        public bool IsEnded { get; private set; }

        public SyncWaitCommand(int queueIndex, SpawnerRepository spawnerRepository)
        {
            _spawnerRepository = spawnerRepository;
            QueueIndex = queueIndex;
        }

        public void Execute()
        {
            if (AllCommandsInAllQueuesIsSyncCommands())
            {
                IsEnded = true;
            }
        }

        private bool AllCommandsInAllQueuesIsSyncCommands()
        {
            return _spawnerRepository.CurrentSpawnCommandByQueueIndex.Values.All(x =>  x.Count == 0 || x.Peek() is SyncWaitCommand);
        }
    }
}