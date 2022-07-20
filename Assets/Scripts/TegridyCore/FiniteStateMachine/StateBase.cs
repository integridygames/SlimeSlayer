namespace TegridyCore.FiniteStateMachine
{
    public abstract class StateBase
    {
        public bool IsActive { get; private set; }

        public void Activate()
        {
            IsActive = true;
            OnActivate();
        }

        protected virtual void OnActivate() {}
        
        public void Deactivate()
        {
            IsActive = false;
            OnDeactivate();
        }
        
        protected virtual void OnDeactivate() {}
    }
}