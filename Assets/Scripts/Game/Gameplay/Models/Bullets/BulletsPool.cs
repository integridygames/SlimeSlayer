using Game.Gameplay.Views.Bullets;
using System.Collections.Generic;
using TegridyCore.Base;

namespace Game.Gameplay.Models.Bullets 
{
    public class BulletsPool : ViewBase
    {
        private List<BulletView> _bullets = new List<BulletView>();
        private List<BulletView> _bulletsForDelete = new List<BulletView>();

        public List<BulletView> Bullets => _bullets;
        public List<BulletView> BulletsForDelete => _bulletsForDelete;
    }
}