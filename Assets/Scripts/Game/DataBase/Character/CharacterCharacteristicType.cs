namespace Game.DataBase.Character
{
    public enum CharacterCharacteristicType
    {
        /// <summary>
        /// Start and max health in battle
        /// </summary>
        MaxHealth = 1,
        /// <summary>
        /// Base attack damage for all weapon types
        /// </summary>
        BaseDamage = 2,
        /// <summary>
        /// Health repairing percentage per second
        /// </summary>
        Regeneration = 3,
        /// <summary>
        /// Additional health with fast regeneration
        /// </summary>
        Shield = 4,
        /// <summary>
        /// Health stealing from enemies from one projectile collision
        /// </summary>
        HealthSteal = 5,
        /// <summary>
        /// Character movement speed
        /// </summary>
        Speed = 6,
        /// <summary>
        /// Shield repairing per second
        /// </summary>
        ShieldRepairing = 7,
        /// <summary>
        /// Attack range
        /// </summary>
        AttackRange = 8,
    }
}