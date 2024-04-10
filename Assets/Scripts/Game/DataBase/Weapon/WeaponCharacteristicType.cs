namespace Game.DataBase.Weapon
{
    public enum WeaponCharacteristicType
    {
        /// <summary>
        /// Damage addition percentage
        /// </summary>
        DamageModificator = 0,
        /// <summary>
        /// Bullets per second
        /// </summary>
        FireRate = 1,
        /// <summary>
        /// Bullets in charge
        /// </summary>
        Charge = 2,
        /// <summary>
        /// Reloading time in seconds
        /// </summary>
        ReloadSpeed = 3,
        /// <summary>
        /// Upgrade price in coins
        /// </summary>
        UpgradePrice = 4
    }
}