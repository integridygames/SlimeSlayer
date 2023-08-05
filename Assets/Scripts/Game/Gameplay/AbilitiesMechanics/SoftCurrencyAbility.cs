using Game.DataBase.GameResource;
using Game.Gameplay.Models.Character;

namespace Game.Gameplay.AbilitiesMechanics
{
    public class SoftCurrencyAbility : AbilityBase
    {
        private readonly GameResourceData _gameResourceData;

        public SoftCurrencyAbility(GameResourceData gameResourceData)
        {
            _gameResourceData = gameResourceData;
        }

        public override void OnStart()
        {
            _gameResourceData.AddResource(GameResourceType.Coin, 25);
        }
    }
}