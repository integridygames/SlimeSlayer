using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed;

        private MeshFilter _meshFilter;

        public float MovementSpeed => _movementSpeed;
        public float RotationSpeed => _rotationSpeed;
        public Vector3 CurrentPatrolPoint { get; private set; }

        public float CurrentHealth { get; private set; }

        private void Awake()
        {
            CurrentHealth = _maxHealth;
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

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;
            
            if (CurrentHealth <= 0)
                Die();
        }

        public void SetPatrolPoint(Vector3 point)
        {
            CurrentPatrolPoint = point;
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}