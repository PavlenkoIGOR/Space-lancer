using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space_lancer
{
    [RequireComponent(typeof(CircleCollider2D))]
    public abstract class Powerup : MonoBehaviour
    {


        private void OnTriggerEnter2D(Collider2D collision)
        {
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();

            if (ship != null && Player.instance.activeShip)
            {
                OnPickedUp(ship);

                Destroy(gameObject);
            }
        }

        protected abstract void OnPickedUp(SpaceShip ship);
    }
}
