namespace Game.Gameplay.WeaponMechanic
{
    public interface IShootComponent
    {
        bool CanShoot();
        void Shoot();
    }
}