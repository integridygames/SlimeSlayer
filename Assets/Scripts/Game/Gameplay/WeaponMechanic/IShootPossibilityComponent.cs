namespace Game.Gameplay.WeaponMechanic
{
    public interface IShootPossibilityComponent
    {
        bool CanShoot();
        void HandleShoot();
    }
}