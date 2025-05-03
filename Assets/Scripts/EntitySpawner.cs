using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space_lancer
{
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] private Entity[] _entitiesPrefabs;
        [SerializeField] private CircleArea _area;
        [SerializeField] private SpawnMode _spawnMode;
        [SerializeField] private int _numSpawns;
        [SerializeField] private float _respawnTime;
        private float _timer;
    }
}
