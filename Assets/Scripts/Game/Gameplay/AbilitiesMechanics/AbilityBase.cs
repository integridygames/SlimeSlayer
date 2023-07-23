namespace Game.Gameplay.AbilitiesMechanics
{
    /// <summary>
    /// Базовый класс способностей
    /// Некоторые способности разовые и удаляться сразу после применения. Некоторые экземляры будут оставаться на всё время в бою.
    /// </summary>
    public abstract class AbilityBase
    {
        /// <summary>
        /// Текущий уровень способности
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Готова ли для использования способность. Проверяется каждый кадр.
        /// </summary>
        /// <returns></returns>
        public virtual bool EnableToExecute()
        {
            return true;
        }

        /// <summary>
        /// Выполняется при первом выборе данной способности первого уровня (если уровни есть).
        /// </summary>
        public virtual void OnStart() {}

        /// <summary>
        /// Выполняется каждый кадр, пока способность существует и доступна для использования.
        /// </summary>
        public virtual void Execute() {}


        /// <summary>
        /// Нужен для отписки. Выполняется при удалении абилки (выходе с уровня).
        /// </summary>
        public virtual void OnEnd()
        {
        }
    }
}