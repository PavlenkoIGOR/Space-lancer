using UnityEngine;

namespace Space_lancer
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GravityWellForuniqueLevel : MonoBehaviour
    {
        [SerializeField] private float _gravityForce;
        [SerializeField] private float _gravityRadius;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            
        }

        private void OnTriggerStay2D(Collider2D collision)
        {

            if (collision.attachedRigidbody == null) 
            {
                return;
            }

            Vector2 direction = transform.position - collision.transform.position;

            float dist = direction.magnitude;

            if (dist < _gravityRadius)
            {
                Vector2 force = direction.normalized * _gravityForce * (dist / _gravityRadius);
                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }
        }


    }
}
