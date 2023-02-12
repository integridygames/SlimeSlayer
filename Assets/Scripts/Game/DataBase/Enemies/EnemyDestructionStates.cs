using System;
using UnityEngine;

namespace Game.DataBase.Enemies
{
    [Serializable]
    public class EnemyDestructionStates
    {
        [SerializeField] private Mesh[] _meshes;

        public Mesh[] Meshes => _meshes;
    }
}