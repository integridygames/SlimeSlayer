using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

        public float MovementSpeed => _movementSpeed;
        public float RotationSpeed => _rotationSpeed;
        public Vector3 CurrentPatrolPoint { get; private set; }

        private MeshFilter _meshFilter;

        private void Start()
        {
            CurrentPatrolPoint = transform.position;
        }

        private void OnDrawGizmos()
        {
            if (_meshFilter == null)
            {
                _meshFilter = GetComponent<MeshFilter>();
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawMesh(_meshFilter.sharedMesh, transform.position);
        }

        public void SetPatrolPoint(Vector3 point) 
        {
            CurrentPatrolPoint = point;
        }
    }
}