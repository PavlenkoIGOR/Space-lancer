using Space_lancer;
using UnityEngine;

public class SeekingArea : MonoBehaviour
{
    private Collider2D[] _colliders;
    public Collider2D[] colliders { get { return _colliders; } set { _colliders = value; } }

    [HideInInspector] public Destructable nearestDestructable;
    [SerializeField] float _seekingDistance = 3.0f;
    float closestDistance;

    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = _seekingDistance;
        closestDistance = _seekingDistance;
    }

    public void GetCollidersInSeekArea()
    {
        _colliders = Physics2D.OverlapCircleAll(transform.position, _seekingDistance);
        Destructable closestEnemyShip = null;
        foreach (var collider in _colliders)
        {
            Destructable enemyShip = collider.transform.root.GetComponent<Destructable>();
            if (enemyShip != null && !enemyShip.name.Contains("PlayerShip"))
            {
                float distance = Vector2.Distance(transform.position, enemyShip.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemyShip = enemyShip; // Closest enemy ship
                }
            }
        }

        if (closestEnemyShip != null)
        {
            nearestDestructable = closestEnemyShip;
        }
    }

}
