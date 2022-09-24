using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Essence;

namespace Game.Gameplay.Models.Character 
{
    public class CharacterEssence
    {
        public EssenceImageView EssenceImageView { get; private set; }
        public int Quantity { get; set; }

        public CharacterEssence(EssenceImageView essenceImageView, int quantity) 
        {
            EssenceImageView = essenceImageView;
            Quantity = quantity;
        }
    }
}