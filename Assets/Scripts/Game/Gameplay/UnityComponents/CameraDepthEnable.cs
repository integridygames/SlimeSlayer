using UnityEngine;

namespace Game.Gameplay.UnityComponents
{
    [RequireComponent(typeof(Camera))]
    [ExecuteInEditMode]
    public class CameraDepthEnable : MonoBehaviour
    {
        private Camera _camera;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            _camera.depthTextureMode = DepthTextureMode.Depth;
        }
    }
}