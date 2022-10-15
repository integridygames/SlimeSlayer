using TegridyCore;

namespace Game.Gameplay.WeaponMechanic
{
    public interface IReloadComponent
    {
        IReadonlyRxField<float> ReloadProgress { get; }
        RxField<int> CurrentCharge { get; }
        bool NeedReload();
        void Reload();
    }
}