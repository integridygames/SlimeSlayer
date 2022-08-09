using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        
        private void OnDrawGizmos()
        {
            if (_meshFilter == null)
            {
                _meshFilter = GetComponent<MeshFilter>();
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawMesh(_meshFilter.sharedMesh, transform.position);
        }
    }
}