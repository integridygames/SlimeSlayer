using Game.Gameplay.Views.Level;
using JetBrains.Annotations;
using TegridyCore;

namespace Game.Gameplay.Models.Level
{
    [UsedImplicitly]
    public class LevelInfo
    {
        public RxField<LevelView> CurrentLevelView { get; set; } = new RxField<LevelView>();
    }
}