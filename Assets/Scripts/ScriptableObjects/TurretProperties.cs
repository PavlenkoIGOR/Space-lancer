using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretMode
{
    Primary,
    Secondary
}

[CreateAssetMenu(menuName = "Turret properties")]
public sealed class TurretProperties : ScriptableObject
{
    [SerializeField] private TurretMode _turretMode;
    public TurretMode turretMode => _turretMode;

    [SerializeField] private Projectile _projectilePrefab;
    public Projectile projectilePrefab => _projectilePrefab;

    [SerializeField] private float _rateFoFire;
    public float rateFoFire => _rateFoFire;

    [SerializeField] private int _energyUsage;
    public int energyUsage => _energyUsage;

    [SerializeField] private float _ammoUsage; //type of ammo
    public float ammoUsage => _ammoUsage;

    [SerializeField] private AudioClip _launchSFX; //shoot sound
    public AudioClip launchSFX => _launchSFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
