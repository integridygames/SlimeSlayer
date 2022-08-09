using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private float _MaxHealth;

        private MeshFilter _meshFilter;

        private float _currentHP;
        public float HP => _currentHP;

        private void Awake()
        {
            _currentHP = _MaxHealth;
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
            _currentHP -= damage;
            if (_currentHP <= 0)
                Die();
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}