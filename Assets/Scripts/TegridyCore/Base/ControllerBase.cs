namespace TegridyCore.Base
{
    public abstract class ControllerBase<T>

    {
        protected readonly T ControlledEntity;

        protected ControllerBase(T controlledEntity)
        {
            ControlledEntity = controlledEntity;
        }
    }
}