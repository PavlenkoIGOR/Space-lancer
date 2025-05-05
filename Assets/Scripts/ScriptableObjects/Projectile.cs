using Space_lancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;


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
    [SerializeField]private CircleCollider2D _seekingCircleArea;
    private Vector3 _direction;
    private Destructable _target;
    #endregion

    [SerializeField] private ImpactEffect _impactEffect;
    private float _timer;
    // Start is called before the first frame update
    void Start()
    {
        #region seeking missle
        _seekingCircleArea = GetComponent<CircleCollider2D>();
        if (_seekingCircleArea != null)
        {            
            _seekingCircleArea.isTrigger = true;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        float stepLength = _velocity * Time.deltaTime;
        Vector2 step = Vector2.zero;
        if (_type == ProjectileType.SeekingMissle)
        {
            step = transform.up * stepLength;
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

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destructable enemyShip = collision.gameObject.transform.root.GetComponent<Destructable>();
        if (enemyShip != null && enemyShip.transform.name != "PlayerShipNode")
        {
            _target = enemyShip;
            Debug.Log("Target locked!");

        }
    }
}
