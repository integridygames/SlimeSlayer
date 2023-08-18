using TegridyCore;

namespace Game.Gameplay.WeaponMechanics
{
    public interface IReloadComponent
    {
        IReadonlyRxField<float> ReloadProgress { get; }
        RxField<int> CurrentCharge { get; }
        bool NeedReload();
        void Reload();
        void Reset();
    }
}