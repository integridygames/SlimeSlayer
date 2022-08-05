using System.Collections.Generic;
using UnityEngine;
using Game.Gameplay.Views.Bullets;
using TegridyCore;

namespace Game.Gameplay.Models.Ammo 
{
    public class AmmoInfo
    {
        public List<AmmoView> AmmoArsenal = new List<AmmoView>();
        public RxField<AmmoView> CurrentAmmoView { get; set; } = new RxField<AmmoView>();

        public void AddAmmoType(AmmoView ammoView)
        {
            AmmoArsenal.Add(ammoView);
        }

        public void RemoveAmmoType(AmmoView ammoView)
        {
            AmmoArsenal.Remove(ammoView);
        }

        public void AddAmmo(string ammoID, int quantity)
        {
            foreach (var ammo in AmmoArsenal)
            {
                if (ammo.ID == ammoID)
                {
                    if (!ammo.IsUnlimited)
                        ammo.AddAmmo(quantity);
                    break;
                }
            }
        }

        public void RemoveAmmo(string ammoID, int quantity)
        {
            foreach (var ammo in AmmoArsenal)
            {
                if (ammo.ID == ammoID && !ammo.IsUnlimited)
                {
                    if (!ammo.IsUnlimited)
                        ammo.RemoveAmmo(quantity);
                    break;
                }
            }
        }
    }
}