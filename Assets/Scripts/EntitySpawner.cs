using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        void Start() 
        {
            if (_spawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }
            _timer = _respawnTime;
        }
        void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }

            if (_spawnMode == SpawnMode.Loop && _timer < 0)
            {
                SpawnEntities();
                _timer = _respawnTime;
            }

        }


        private void SpawnEntities()
        {
            for (int i = 0; i < _numSpawns; i++)
            {
                int index = Random.Range(0, _entitiesPrefabs.Length);
                GameObject ent = Instantiate(_entitiesPrefabs[index].gameObject);
                ent.transform.position = _area.GetRandomInsideZone();
            }
        }
    }
}
