namespace TegridyCore.Base
{
    public abstract class ControllerBase<T> where T : ViewBase

    {
        protected readonly T ControlledEntity;

        protected ControllerBase(T controlledEntity)
        {
            ControlledEntity = controlledEntity;
        }
    }
}