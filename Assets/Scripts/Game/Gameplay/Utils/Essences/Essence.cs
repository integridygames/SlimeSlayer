namespace Game.Gameplay.Utils.Essences 
{
    public class Essence
    {
        public EssenceType EssenceType { get; private set; }
        public int Quantity { get; set; }

        public Essence(EssenceType essenceType, int quantity) 
        {
            EssenceType = essenceType;
            Quantity = quantity;
        }
    }  
}