using Game.Gameplay.Utils.Essences;
using Game.Gameplay.Views.Essence;

namespace Game.Gameplay.Models.Character 
{
    public class CharacterEssence
    {
        public EssenceType EssenceType { get; private set; }
        public EssenceImageView EssenceImageView { get; private set; }
        public int Quantity { get; set; }

        public CharacterEssence(EssenceType essenceType, EssenceImageView essenceImageView, int quantity) 
        {
            EssenceType = essenceType;
            EssenceImageView = essenceImageView;
            Quantity = quantity;
        }
    }
}