using Game.Gameplay.Views.Teleport;
using TegridyCore;

namespace Game.Gameplay.Models.Teleport 
{
    public class TeleportInfo
    {
        public RxField<TeleportView> CurrentTeleportView { get; set; } = new();
    }
}