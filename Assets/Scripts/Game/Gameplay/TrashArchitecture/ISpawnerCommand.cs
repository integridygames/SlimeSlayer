namespace Game.Gameplay.TrashArchitecture
{
    public interface ISpawnerCommand
    {
        public void Execute();
        
        public bool IsEnded { get; }

        public int QueueIndex { get; }
    }
}