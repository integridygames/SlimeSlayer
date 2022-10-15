using System;
using Game.DataBase;
using UnityEngine;

namespace Game.Gameplay.Views.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public event Action<int, Vector3, EssenceType, EnemyView> OnEnemyDied;

        [SerializeField] private float _maxHealth;
        [SerializeField] private EssenceType _essenceType;
        [SerializeField] private int _essenceQuntity;

        private MeshFilter _meshFilter;

        public float CurrentHealth { get; private set; }
        public EssenceType EssenceType => _essenceType;
        public int EssenceQuntity => _essenceQuntity;

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
            OnEnemyDied?.Invoke(_essenceQuntity, transform.position, _essenceType, this);
            gameObject.SetActive(false);
        }
    }
}