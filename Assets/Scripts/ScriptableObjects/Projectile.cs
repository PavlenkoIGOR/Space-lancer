using Space_lancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    [SerializeField] private float _velocity;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _damage;
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);
        if (hit)
        {
            Destructable destructableObj = hit.collider.transform.root.GetComponent<Destructable>();
            if (destructableObj != null &&destructableObj != _parent) 
            {
                destructableObj.ApplyDmg(_damage);
            }
            OnProjectileLifeEnd(hit.collider, hit.point);
        }
        
        _timer += Time.deltaTime;
        if (_timer > _lifeTime)
        {
            Destroy(gameObject);
        }

        transform.position += new Vector3(step.x, step.y, 0);
    }

    void OnProjectileLifeEnd(Collider2D collider, Vector2 vector2)
    {
        Destroy(gameObject);
    }

    private Destructable _parent;public void SetParentShooter(Destructable parent)
    {
        _parent = parent;
    }


}
