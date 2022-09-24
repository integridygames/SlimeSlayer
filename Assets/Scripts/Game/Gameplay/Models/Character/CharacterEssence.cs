using Game.Gameplay.Views.Essence;

namespace Game.Gameplay.Models.Character
{
    public class CharacterEssence
    {
        private readonly EssenceImageView _essenceImageView;
        private int _quantity;

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                _essenceImageView.Quantity.text = _quantity.ToString();
            }
        }

        public CharacterEssence(EssenceImageView essenceImageView, int quantity)
        {
            _essenceImageView = essenceImageView;
            Quantity = quantity;
        }
    }
}