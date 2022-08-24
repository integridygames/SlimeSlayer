using Game.Gameplay.Views.Bullets;
using System.Collections.Generic;
using TegridyCore.Base;
using UnityEngine;

namespace Game.Gameplay.Models.Bullets 
{
    public class BulletsPool : ViewBase
    {
        [SerializeField] [Range(0, 1000)] private int _bulletsPoolQuantity;
        [SerializeField] private bool _isLeft;

        public List<BulletView> Bullets { get; private set; } = new List<BulletView>();
        public bool IsLeft => _isLeft;

        public int CurrentBulletNumber { get; private set; }

        public void CreatePool(BulletView bulletTemplate) 
        {
            CurrentBulletNumber = 0;

            for (int i = 0; i < _bulletsPoolQuantity; i++) 
            {
                var bullet = Instantiate(bulletTemplate, transform.position, Quaternion.identity);
                bullet.gameObject.SetActive(false);
                bullet.GetComponent<Transform>().SetParent(transform);
                Bullets.Add(bullet);               
            }
        }

        public void ClearPool() 
        {
            Bullets.Clear();
        }

        public void ChangePoolQuantity(int quantity) 
        {
            _bulletsPoolQuantity = Mathf.Clamp(quantity, 0, quantity);
        }

        public BulletView TakeNextBullet() 
        {
            BulletView nextBullet = null;

            if (CurrentBulletNumber < _bulletsPoolQuantity) 
            {
                nextBullet = Bullets[CurrentBulletNumber];
                CurrentBulletNumber++;              
            }
            else 
            {
                CurrentBulletNumber = 0;
                nextBullet = Bullets[CurrentBulletNumber];
            }

            return nextBullet;
        }
    }
}