using Game.Gameplay.Factories;
using Game.Gameplay.Views;
using Game.Gameplay.Views.CameraContainer;
using TegridyUtils;
using UnityEngine;

namespace Game.Gameplay.Services
{
    public class DamageFxService
    {
        private readonly CameraContainerView _cameraContainerView;
        private readonly CanvasView _canvasView;
        private readonly UiFxPoolFactory _uiFxPoolFactory;

        public DamageFxService(CameraContainerView cameraContainerView, CanvasView canvasView, UiFxPoolFactory uiFxPoolFactory)
        {
            _cameraContainerView = cameraContainerView;
            _canvasView = canvasView;
            _uiFxPoolFactory = uiFxPoolFactory;
        }

        public void DoDamageFx(int damage, Vector3 position, int spread)
        {
            var damageFx = _uiFxPoolFactory.GetElement(0);

            var damageFxPosition = MathUtils.ToScreenPositionWithOffset(position, _cameraContainerView.Camera,
                0, 0);

            var randomStartPos = Random.insideUnitCircle * spread * _canvasView.Canvas.scaleFactor;

            damageFxPosition.x += randomStartPos.x;
            damageFxPosition.y += randomStartPos.y;

            damageFx.transform.position = damageFxPosition;

            damageFx.StartFx((damage).ToString(),
                () => { _uiFxPoolFactory.RecycleElement(0, damageFx); });
        }
    }
}