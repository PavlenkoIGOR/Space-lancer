using Space_lancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    [SerializeField] private float _velocity;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _damage;
    [SerializeField] private ImpactEffect _impactEffect;
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float stepLength = _velocity * Time.deltaTime;
        Vector2 step = transform.up * stepLength;

        
        _timer += Time.deltaTime;
        if (_timer > _lifeTime)
        {
            Destroy(gameObject);
        }

        transform.position += new Vector3(step.x, step.y, 0);
    }
}
