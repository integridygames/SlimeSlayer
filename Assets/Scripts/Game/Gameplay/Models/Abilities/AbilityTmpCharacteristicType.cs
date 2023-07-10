namespace Game.Gameplay.Models.Abilities
{
    public enum AbilityTmpCharacteristicType
    {
        /// <summary>
        /// Граната делится на несколько гранат
        /// </summary>
        MultipleGrenade,
        /// <summary>
        /// Противник взрывается после смерти нанося урон противникам вокруг
        /// </summary>
        EnemyDeathExplosion,
        /// <summary>
        /// Из персонажа периодически вылетает граната в сторону ближайшего противника
        /// </summary>
        PeriodicGrenadeToRandomEnemy,
        /// <summary>
        /// По следу персонажа идёт шлейф из огня, наносящий урон противникам
        /// </summary>
        FireTrail,
        /// <summary>
        /// Из персонажа периодически исходит импульс, расталкивающий врагов
        /// </summary>
        PushingImpulse,
        /// <summary>
        /// Ускорение скорострельности всех используемых орудий
        /// </summary>
        SpeedFire,
        /// <summary>
        /// Ускорение перезарядки всех используемых орудий
        /// </summary>
        SpeedReload,
        /// <summary>
        /// В зоне видимости на землю в случайные места падают взрывные снаряды, нанося значительный урон
        /// </summary>
        ManyRandomMissiles,
        /// <summary>
        /// Увеличение регенерации
        /// </summary>
        HealthRegeneration,
        /// <summary>
        /// Увеличение здоровья
        /// </summary>
        MoreHealth,
        /// <summary>
        /// Увеличение скорости передвижения
        /// </summary>
        MovementSpeed,
        /// <summary>
        /// Увеличение шанса, выпадения редкой валюты
        /// </summary>
        HardCurrencyChanceImprovement,
        /// <summary>
        /// Увеличение обоймы всех используемых орудий
        /// </summary>
        ChargeImprovement,
        /// <summary>
        /// Восстановление здоровья
        /// </summary>
        HealthOneTimeRecovery,
        /// <summary>
        /// Получение валюты
        /// </summary>
        SoftCurrency,
        /// <summary>
        /// Персонаж стреляет самонаводящейся ракетой. Ракета летит к ближайшему скопление врагов
        /// </summary>
        MissileToRandomEnemy
    }
}