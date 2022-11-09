namespace Game.Gameplay.WeaponMechanics
{
    public interface IShootPossibilityComponent
    {
        bool CanShoot();
        void HandleShoot();
    }
}