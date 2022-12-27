using Game.Gameplay.Views.Character;
using System;
using TegridyCore.Base;

namespace Game.Gameplay.Views.Teleport 
{
    public class TeleportView : ViewBase
    {
        public Action<CharacterView> PlayerEnteredTeleport;

        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            if(other.TryGetComponent<CharacterView>(out CharacterView characterView))
            {
                PlayerEnteredTeleport?.Invoke(characterView);
            }
        }
    }
}