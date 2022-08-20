using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;

        private MeshFilter _meshFilter;

        private float _currentHp;
        public float HP => _currentHp;

        private void Awake()
        {
            _currentHp = _maxHealth;
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
            _currentHp -= damage;
            if (_currentHp <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}