using System;

namespace TegridyCore
{
    public interface IReadonlyRxField<T>
    {
        public event Action<RxValue<T>> OnUpdate;

        public T Value { get; }
    }
}