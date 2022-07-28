using Game.Gameplay.Views.Level;
using JetBrains.Annotations;
using TegridyCore;
using TegridyCore.Base;

namespace Game.Gameplay.Models.Level
{
    [UsedImplicitly]
    public class LevelInfo : ViewBase
    {
        public RxField<LevelView> CurrentLevelView { get; set; } = new RxField<LevelView>();
    }
}