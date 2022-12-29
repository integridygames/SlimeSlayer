using Game.Gameplay.Models.Enemy;
using Game.Gameplay.Models.Teleport;
using TegridyCore.Base;

namespace Game.Gameplay.Systems.Teleport 
{
    public class TeleportActiveStateUpdateSystem : IUpdateSystem
    {
        private readonly ActiveEnemiesContainer _activeEnemiesContainer;
        private readonly TeleportInfo _teleportInfo;

        public TeleportActiveStateUpdateSystem(ActiveEnemiesContainer activeEnemiesContainer, TeleportInfo teleportInfo) 
        {
            _activeEnemiesContainer = activeEnemiesContainer;
            _teleportInfo = teleportInfo;
        }

        public void Update()
        {
            if(_teleportInfo.CurrentTeleportView.Value != null) 
            {
                TryToHide();
                TryToShow();               
            }
        }

        private bool ConditionForShowing() 
        {
            return _activeEnemiesContainer.ActiveEnemies.Count == 0
                && !_teleportInfo.CurrentTeleportView.Value.gameObject.activeInHierarchy;
        }

        private bool ConditionForHiding()
        {
            return _activeEnemiesContainer.ActiveEnemies.Count > 0 
                && _teleportInfo.CurrentTeleportView.Value.gameObject.activeInHierarchy;
        }

        private void TryToHide() 
        {
            if (ConditionForHiding())
            {
                _teleportInfo.CurrentTeleportView.Value.gameObject.SetActive(false);
            }
        }

        private void TryToShow() 
        {
            if (ConditionForShowing())
            {
                _teleportInfo.CurrentTeleportView.Value.gameObject.SetActive(true);
            }
        }
    }
}