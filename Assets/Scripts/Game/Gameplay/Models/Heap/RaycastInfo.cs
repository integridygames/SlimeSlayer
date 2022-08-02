namespace Game.Gameplay.Models.Heap 
{
    public class RaycastInfo
    {
        private const float _radius = 30;
        private const int _layerNumber = 1 << 12;

        public float Radius => _radius;
        public int LayerNumber => _layerNumber;
    }
}