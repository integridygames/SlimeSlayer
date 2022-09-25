namespace Game.Gameplay.WeaponMechanic
{
    public interface IWeapon
    {
        public void Shoot();

        public bool NeedToShoot();
    }
}