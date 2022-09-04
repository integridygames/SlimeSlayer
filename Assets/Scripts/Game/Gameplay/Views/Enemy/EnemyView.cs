using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;

        private MeshFilter _meshFilter;

        public float CurrentHealth { get; private set; }

        private void Awake()
        {
            CurrentHealth = _maxHealth;
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

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}