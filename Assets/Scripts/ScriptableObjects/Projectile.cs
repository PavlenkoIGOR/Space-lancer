using Space_lancer;
using UnityEngine;


public class Projectile : Entity
{


    [SerializeField] private float _velocity;
    [SerializeField] private float _lifeTime;
    [SerializeField] private int _damage;

    #region seeking missle
    private enum ProjectileType
    {
        SeekingMissle,
        Simple
    }
    [SerializeField] private ProjectileType _type;
    [SerializeField] private SeekingArea _seekingArea;
    private Destructable _target;
    #endregion

    [SerializeField] private ImpactEffect _impactEffect;
    private float _timer;


    // Update is called once per frame
    void Update()
    {
        
        float stepLength = _velocity * Time.deltaTime;
        Vector2 step = Vector2.zero;
        if (_type == ProjectileType.SeekingMissle)
        {
            _seekingArea.GetCollidersInSeekArea();
            _target = _seekingArea.nearestDestructable;
            if (_target != null)
            {
                Vector2 directionToTarget = (_target.transform.root.position - transform.position).normalized;
                step = directionToTarget * stepLength;
            }
            else
            {
                step = transform.up * stepLength;
            }
        }
        if (_type == ProjectileType.Simple)
        {
            step = transform.up * stepLength;
        }


       

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);
        if (hit)
        {
            Destructable destructableObj = hit.collider.transform.root.GetComponent<Destructable>();
            if (destructableObj != null && destructableObj != _parent) 
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

    private Destructable _parent;
    public void SetParentShooter(Destructable parent)
    {
        _parent = parent;
    }
}
