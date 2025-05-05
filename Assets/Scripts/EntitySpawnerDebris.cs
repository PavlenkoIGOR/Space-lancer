using Space_lancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySpawnerDebris : MonoBehaviour
{
    [SerializeField] private Destructable[] _debrisPrefabs;

    [SerializeField] private CircleArea _spawnArea;

    [SerializeField] private int _numDebris;

    [SerializeField] private float _RandomSpeed;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _numDebris; i++)
        {
            SpawnDebris();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnDebris()
    {
        int index = Random.Range(0, _debrisPrefabs.Length);
        GameObject debris = Instantiate(_debrisPrefabs[index].gameObject);

        debris.transform.position = _spawnArea.GetRandomInsideZone();
        debris.GetComponent<Destructable>().eventOnDeath.AddListener(OnDebrisDead);

        Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

        if (rb != null && _RandomSpeed > 0)
        {
            rb.velocity = (Vector2) UnityEngine.Random.insideUnitSphere * _RandomSpeed;
        }
    }

    private void OnDebrisDead()
    {
        SpawnDebris();
    }
}
