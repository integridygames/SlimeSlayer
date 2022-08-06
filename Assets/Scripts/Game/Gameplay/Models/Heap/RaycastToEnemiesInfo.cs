namespace Game.Gameplay.Models.Heap 
{
    public class RaycastToEnemiesInfo
    {
        private const float RadiusConstant = 30;
        private const int LayerNumberConstant = 1 << 12;

        public float Radius => RadiusConstant;
        public int LayerNumber => LayerNumberConstant;
    }
}