using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space_lancer
{
    public class PowerupWeapon : Powerup
    {
        [SerializeField] private TurretProperties _properties;
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AssignWeapon(_properties);
        }
    }
}
