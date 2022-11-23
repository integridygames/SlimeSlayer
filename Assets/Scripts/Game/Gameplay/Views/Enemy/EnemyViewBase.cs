using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public abstract class EnemyViewBase : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;

        public void SetEssenceMaterial(Material material)
        {
            _meshRenderer ??= GetComponent<MeshRenderer>();

            _meshRenderer.material = material;
        }
    }
}