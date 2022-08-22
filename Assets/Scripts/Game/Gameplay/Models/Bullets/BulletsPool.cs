using Game.Gameplay.Views.Bullets;
using System.Collections.Generic;
using TegridyCore.Base;

namespace Game.Gameplay.Models.Bullets 
{
    public class BulletsPool : ViewBase
    {     
        public List<BulletView> Bullets { get; private set; } = new List<BulletView>();
        public List<BulletView> BulletsForDelete { get; private set; } = new List<BulletView>();
    }
}