using Game.Gameplay.Views.Bullets;
using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Models.Bullets 
{
    public class BulletsPool : ViewBase
    {
        public List<BulletView> Bullets = new List<BulletView>();
    }
}