using Space_lancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private TurretMode _turretMode;
    public TurretMode turretMode => _turretMode;

    [SerializeField] private TurretProperties _turretProperties;

    private float _refireTimer;
    public bool _canFire => _refireTimer <= 0;

    private SpaceShip _ship;


    // Start is called before the first frame update
    void Start()
    {
        _ship = transform.root.GetComponent<SpaceShip>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_refireTimer > 0)
            _refireTimer -= Time.deltaTime;
    }

    public void Fire()
    {
        if (_turretProperties == null) return;

        if (_refireTimer > 0) return;

        Projectile projectile = Instantiate(_turretProperties.projectilePrefab).GetComponent<Projectile>();
        //чтобы не писать Instantiate(object, position, rotation), пишем так:
        //instead of Instantiate(object, position, rotation) write:
        projectile.transform.position = transform.position; //set the position of projectile
        projectile.transform.up = transform.up; // set the rotation (set UP-vectors in one direction). установка вращения (установка up векторов в одном направлении)

        _refireTimer = _turretProperties.rateFoFire;

        //sound of shooting here

        Debug.Log("is firing");
    }

    public void AssignLoadOut(TurretProperties propsSO)
    {
        if (_turretMode != propsSO.turretMode) return;

        _refireTimer = 0;
        _turretProperties = propsSO;
    }


}
